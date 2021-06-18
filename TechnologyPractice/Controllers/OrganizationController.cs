using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetOrganization(Guid id, [FromQuery] OrganizationParameters parameters)
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
        public async Task<IActionResult> PostCreateOrganization([FromBody] OrganizationCreationDto organization, [FromQuery] OrganizationParameters parameters)
        {
            var organizationEntity = _mapper.Map<Organization>(organization);

            _repository.Organizations.CreateOrganization(organizationEntity);
            await _repository.SaveAsync();

            var organizationToReturn = _mapper.Map<OrganizationDto>(organizationEntity);

            return CreatedAtRoute("OrganizationById", new { id = organizationToReturn.Id }, organizationToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> DeleteOrganizationById(Guid id, [FromQuery] OrganizationParameters parameters)
        {
            var organization = HttpContext.Items["organization"] as Organization;
            _repository.Organizations.DeleteOrganization(organization);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PutUpdateOrganization(Guid id, [FromQuery] OrganizationParameters parameters, [FromBody] OrganizationCreationDto organization)
        {
            var organizationDb = HttpContext.Items["organization"] as Organization;
            _mapper.Map(organization, organizationDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PatchUpdateOrganization(Guid id, [FromQuery] OrganizationParameters parameters, [FromBody] JsonPatchDocument<OrganizationUpdateDto> patchDoc)
        {
            var organizationDb = HttpContext.Items["organization"] as Organization;

            var organizationToPatch = _mapper.Map<OrganizationUpdateDto>(organizationDb);
            patchDoc.ApplyTo(organizationToPatch);

            _mapper.Map(organizationToPatch, organizationDb);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
