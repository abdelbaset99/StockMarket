using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_asp.Data;
using backend_asp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using backend_asp.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using backend_asp.Services;
using System;
using System.IO;


namespace backend_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(
            // ILogger<UserController> logger,
            UserService userService
        )
        {
            _userService = userService;
        }

       
        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequest userSignUpRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignUpAsync(userSignUpRequest);
                if (result == null)
                {
                    return BadRequest(this.ModelState);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("signin")]
        // [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserSignInRequest userSignInRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignInAsync(userSignInRequest);
                if (result == null)
                {
                    return BadRequest("Invalid username or password.");
                }
                return Ok(result);
            }
            return BadRequest("Invalid request");
        }
    }
}
