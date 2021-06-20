using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice.ActionFilters
{
    public class ValidateContactExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateContactExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var organizationId = (Guid)context.ActionArguments["organizationId"];
            var parameters = (ContactParameters)context.ActionArguments["parameters"];

            var organization = await _repository.Organizations.GetOrganizationAsync(organizationId, parameters, trackChanges);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {organizationId} doesn't exist in the database.");
                context.Result = new NotFoundResult(); return;
            }

            var id = (Guid)context.ActionArguments["contactId"];
            var contact = await _repository.Contacts.GetContactByIdAsync(organizationId, id, parameters, trackChanges);

            if (contact == null)
            {
                _logger.LogInfo($"Contact with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("contact", contact);
                await next();
            }
        }
    }
}
