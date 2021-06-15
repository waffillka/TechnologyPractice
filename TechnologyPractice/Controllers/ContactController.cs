using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice.Controllers
{
    [Route("api/organizations/{organizationId}/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private IMapper _mapper;

        public ContactController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactsFormOrganization(Guid organizationId)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(organizationId, false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {organizationId} doesn't exist in the database.");
                return NotFound();
            }

            var contacts = await _repository.Contacts.GetContactsAsync(organizationId, false);
            var contactsDto = _mapper.Map<IEnumerable<ContactDto>>(contacts);

            return Ok(contactsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactFormOrganizationById(Guid organizationId, Guid id)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(organizationId, false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {organizationId} doesn't exist in the database.");
                return NotFound();
            }

            var contact = await _repository.Contacts.GetContactByIdAsync(organizationId, id, false);
            if (contact == null)
            {
                _logger.LogInfo($"Contact with id: {organizationId} doesn't exist in the database.");
                return NotFound();
            }

            var contactDto = _mapper.Map<ContactDto>(contact);

            return Ok(contactDto);
        }
    }
}
