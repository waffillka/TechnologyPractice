using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnologyPractice.ActionFilters;

namespace TechnologyPractice.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger; 
        private readonly IMapper _mapper; 
        private readonly UserManager<User> _userManager; 
        
        public AuthenticationController(ILoggerManager logger, IMapper mapper, UserManager<User> userManager) 
        { 
            _logger = logger; 
            _mapper = mapper; 
            _userManager = userManager; 
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var user = _mapper.Map<User>(userForRegistrationDto);

            var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
            if (!result.Succeeded) 
            { 
                foreach (var error in result.Errors) 
                { 
                    ModelState.TryAddModelError(error.Code, error.Description); 
                }
                
                return BadRequest(ModelState); 
            }
            await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles); 
            return StatusCode(201);
        }
    }
}
