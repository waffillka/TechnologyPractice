using AutoMapper;
using Contracts;
using EmailService;
using Entities.DataTransferObjects;
using Entities.EmailServiceModels;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyPractice.ActionFilters;

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
        public async Task<IActionResult> GetContactsFormOrganization(Guid organizationId, [FromQuery] ContactParameters parameters)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(organizationId, parameters, false);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {organizationId} doesn't exist in the database.");
                return NotFound();
            }

            var contacts = await _repository.Contacts.GetContactsAsync(organizationId, parameters, false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(parameters.MetaData));
            var contactsDto = _mapper.Map<IEnumerable<ContactDto>>(contacts);

            return Ok(contactsDto);
        }

        [HttpGet("{contactId}")]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> GetContactFormOrganizationById(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            var contact = HttpContext.Items["contact"] as Contact;
            var contactDto = _mapper.Map<ContactDto>(contact);

            return Ok(contactDto);
        }

        [HttpPost("sendEmail/{contactId}")]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> SendEmail(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            var contact = HttpContext.Items["contact"] as Contact;

            IEmailService email = new EmailService.EmailService();

            await email.SendAsync(contact.Email, $"{contact.FirstName} {contact.LastName}", "Test", $"Your Id is {contact.Id}");

            contact.CountLetters = contact.CountLetters++;
            await _repository.SaveAsync();

            return Ok("message sent");
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PostCreateContact(Guid organizationId, [FromBody] ContactCreationDto contact, [FromQuery] ContactParameters parameters)
        {
            var contactEntity = _mapper.Map<Contact>(contact);

            _repository.Contacts.CreateContact(organizationId, contactEntity);
            await _repository.SaveAsync();

            var contactToReturn = _mapper.Map<ContactDto>(contactEntity);

            return CreatedAtRoute("OrganizationById", new { id = contactToReturn.Id }, contactToReturn);
        }

        [HttpPut("{contactId}")]
        public async Task<IActionResult> PutUpdateContact(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            return NotFound();
        }

        [HttpDelete("{contactId}")]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> DeleteContact(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            var contact = HttpContext.Items["contact"] as Contact;

            _repository.Contacts.DeleteContact(contact);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
