using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechnologyPractice.ActionFilters
{
    public class ValidateCollectionOrganizationsExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateCollectionOrganizationsExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var organizationId = (IEnumerable<Guid>)context.ActionArguments["organizationIds"];
            var parameters = (OrganizationParameters)context.ActionArguments["parameters"];

            var organizations = await _repository.Organizations.GetOrganizationsByIdsAsync(organizationId, parameters, trackChanges);

            if (organizations == null)
            {
                _logger.LogInfo($"Organization with id: {organizationId} doesn't exist in the database.");
                context.Result = new NotFoundResult(); return;
            }
            else
            {
                context.HttpContext.Items.Add("organizations", organizations);
                await next();
            }
        }
    }
}
