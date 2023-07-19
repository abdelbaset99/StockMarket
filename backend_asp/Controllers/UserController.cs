using Microsoft.AspNetCore.Mvc;
using backend_asp.Models;
// using Microsoft.Extensions.Logging;
using backend_asp.Services;
// using Microsoft.Extensions.Localization;

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
