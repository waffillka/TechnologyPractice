using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyPractice.ActionFilters;
using TechnologyPractice.ModelBinders;

namespace TechnologyPractice.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
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


        [HttpGet(Name = "GetOrganizations"), Authorize]
        public async Task<IActionResult> GetALLOrganizations([FromQuery] OrganizationParameters parameters)
        {
            var organizations = await _repository.Organizations.GetAllOrganizationsAsync(false, parameters);
            var organizationsDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);

            return Ok(organizationsDto);
        }

        [HttpGet("{organizationId}", Name = "organizationId"), Authorize]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> GetOrganization(Guid organizationId, [FromQuery] OrganizationParameters parameters)
        {
            var organization = HttpContext.Items["organization"] as Organization;
            var organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Ok(organizationDto);
        }

        [HttpGet("collection/{organizationIds}"), Authorize]
        [ServiceFilter(typeof(ValidateCollectionOrganizationsExistsAttribute))]
        public async Task<IActionResult> GetOrganizations([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> organizationIds, [FromQuery] OrganizationParameters parameters)
        {
            var organizations = HttpContext.Items["organizations"] as Organization;
            var organizationsDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);

            return Ok(organizationsDto);
        }

        [HttpPost, Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PostCreateOrganization([FromBody] OrganizationCreationDto organization, [FromQuery] OrganizationParameters parameters)
        {
            var organizationEntity = _mapper.Map<Organization>(organization);

            _repository.Organizations.CreateOrganization(organizationEntity);
            await _repository.SaveAsync();

            var organizationToReturn = _mapper.Map<OrganizationDto>(organizationEntity);

            return CreatedAtRoute("OrganizationById", new { id = organizationToReturn.Id }, organizationToReturn);
        }

        [HttpPost("collection"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PostCreateCollectionOrganizations([FromBody] IEnumerable<OrganizationCreationDto> organizations, [FromQuery] OrganizationParameters parameters)
        {
            var organizationsEntity = _mapper.Map<IEnumerable<Organization>>(organizations);
            _repository.Organizations.CreateCollectionOrganizations(organizationsEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{organizationId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> DeleteOrganizationById(Guid organizationId, [FromQuery] OrganizationParameters parameters)
        {
            var organization = HttpContext.Items["organization"] as Organization;
            _repository.Organizations.DeleteOrganization(organization);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{organizationId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PutUpdateOrganization(Guid organizationId, [FromQuery] OrganizationParameters parameters, [FromBody] OrganizationCreationDto organization)
        {
            var organizationDb = HttpContext.Items["organization"] as Organization;
            _mapper.Map(organization, organizationDb);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{organizationId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PatchUpdateOrganization(Guid organizationId, [FromQuery] OrganizationParameters parameters, [FromBody] JsonPatchDocument<OrganizationUpdateDto> patchDoc)
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
