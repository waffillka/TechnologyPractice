using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnologyPractice.ActionFilters
{
    public class ValidateOrganizationExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateOrganizationExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (Guid)context.ActionArguments["id"];
            var organization = await _repository.Organizations.GetOrganizationAsync(id, trackChanges);

            if (organization == null)
            {
                _logger.LogInfo($"Organization with id: {id} doesn't exist in the database."); 
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("organization", organization);
                await next();
            }
        }
    }
}
