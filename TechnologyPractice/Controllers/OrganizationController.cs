using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice.Controllers
{
    [Route("api/organization")]
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
        public async Task<IActionResult> GetOrganizations()
        {
            var organizations = await _repository.Organizations.GetAllOrganizationsAsync(false);
            var organizationsDto = _mapper.Map<IEnumerable<OrganizationDto>>(organizations);

            return Ok(organizationsDto);
        }

        [HttpGet("{id}", Name = "OrganizationId")]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(id, false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Ok(organizationDto);
        }
    }
}
