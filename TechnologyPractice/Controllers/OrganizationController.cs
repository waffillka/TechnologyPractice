using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyPractice.ActionFilters;

namespace TechnologyPractice.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;

        public OrganizationController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations([FromQuery] OrganizationParameters parameters)
        {
            var organizations = await _repository.Organizations.GetAllOrganizationsAsync(false, parameters);
            var organizationsDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);

            return Ok(organizationsDto);
        }

        [HttpGet("{id}", Name = "OrganizationId")]
        public async Task<IActionResult> GetOrganization(Guid id, OrganizationParameters parameters)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(id, parameters, false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Ok(organizationDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PostCreateOrganization([FromBody] OrganizationCreationDto organization)
        {
            var organizationEntity = _mapper.Map<Organization>(organization);

            _repository.Organizations.CreateOrganization(organizationEntity);
            await _repository.SaveAsync();

            var organizationToReturn = _mapper.Map<OrganizationDto>(organizationEntity);

            return CreatedAtRoute("OrganizationById", new { id = organizationToReturn.Id }, organizationToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> DeleteOrganizationById(Guid id)
        {
            var organization = HttpContext.Items["organization"] as Organization;
            _repository.Organizations.DeleteOrganization(organization);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
