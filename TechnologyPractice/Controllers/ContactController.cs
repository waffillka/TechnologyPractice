using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyPractice.ActionFilters;

namespace TechnologyPractice.Controllers
{
    [Route("api/organizations/{organizationId}/contacts")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
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

        [HttpGet, Authorize]
        public async Task<IActionResult> GetContactsFormOrganization(Guid organizationId, [FromQuery] ContactParameters parameters)
        {
            var organization = await _repository.Organizations.GetOrganizationAsync(organizationId, new OrganizationParameters(), false);

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

        [HttpGet("{contactId}"), Authorize]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public IActionResult GetContactFormOrganizationById(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            var contact = HttpContext.Items["contact"] as Contact;
            var contactDto = _mapper.Map<ContactDto>(contact);

            return Ok(contactDto);
        }

        [HttpPost("sendEmail/{contactId}"), Authorize(Roles = "Administrator")]
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

        [HttpPost, Authorize(Roles = "Administrator")]
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

        [HttpPost("collection"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrganizationExistsAttribute))]
        public async Task<IActionResult> PostCreateCollectionContacts(Guid organizationId, [FromBody] IEnumerable<ContactCreationDto> contacts, [FromQuery] ContactParameters parameters)
        {
            var contactEntity = _mapper.Map<IEnumerable<Contact>>(contacts);

            _repository.Contacts.CreateCollectionContacts(organizationId, contactEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{contactId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> PutUpdateContact(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters, [FromBody] ContactCreationDto contact)
        {
            var contactBd = HttpContext.Items["contact"] as Contact;
            _mapper.Map(contact, contactBd);
            await _repository.SaveAsync();

            return NotFound();
        }

        [HttpDelete("{contactId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> DeleteContact(Guid organizationId, Guid contactId, [FromQuery] ContactParameters parameters)
        {
            var contact = HttpContext.Items["contact"] as Contact;

            _repository.Contacts.DeleteContact(contact);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{contactId}"), Authorize(Roles = "Administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateContactExistsAttribute))]
        public async Task<IActionResult> PatchUpdateContact(Guid organizationId, Guid contactId, [FromQuery] OrganizationParameters parameters, [FromBody] JsonPatchDocument<ContactUpdateDto> patchDoc)
        {
            var contactDb = HttpContext.Items["contact"] as Contact;

            var contactToPatch = _mapper.Map<ContactUpdateDto>(contactDb);
            patchDoc.ApplyTo(contactToPatch);

            _mapper.Map(contactToPatch, contactDb);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
