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
// using BCrypt.Net;


namespace backend_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration Configuration;
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
        // private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;


        public UserController(
            ILogger<UserController> logger,
            UserContext context,
            IConfiguration configuration,
            // UserManager<User> userManager,
            IOptionsMonitor<JwtConfig> jwtConfig
        )
        {
            _context = context;
            Configuration = configuration;
            // _userManager = userManager;
            _jwtConfig = jwtConfig.CurrentValue;
        }

        // // GET: api/User
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        // {
        //     if (_context.Users == null)
        //     {
        //         return NotFound();
        //     }
        //     return await _context.Users.ToListAsync();
        // }

        // GET: api/User/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<User>> GetUser(string id)
        // {
        //     if (_context.Users == null)
        //     {
        //         return NotFound();
        //     }
        //     var user = await _context.Users.FindAsync(id);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return user;
        // }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUser(string id, User user)
        // {
        //     if (id != user.UserName)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(user).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpRequest userSignUpRequest)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _context.Users.FindAsync(userSignUpRequest.UserName);
                if (userExists != null)
                {
                    return BadRequest("User already exists!");
                }
                var newUser = new User(){Email = userSignUpRequest.Email, UserName = userSignUpRequest.UserName};
                var hashedPassword = passwordHasher.HashPassword(newUser, userSignUpRequest.Password);
                newUser.Password = hashedPassword;
                var result = await _context.Users.AddAsync(newUser);

                if (result != null)
                {
                    await _context.SaveChangesAsync();
                    // return Ok(result.ToString());
                    return Ok(new UserSignUpResponse()
                    {
                        Success = true,
                        Token = GenerateJwtToken(newUser)
                    });
                }
                else
                {
                    return BadRequest("Invalid request");
                }
            }
            return BadRequest("Invalid request");
            // if (_context.Users == null)
            // {
            //     return Problem("Entity set 'UserContext.Users'  is null.");
            // }
            // _context.Users.Add(user);
            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateException)
            // {
            //     if (UserExists(user.UserName))
            //     {
            //         return Conflict();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

            // return CreatedAtAction("GetUser", new { id = user.UserName }, user);
        }

        // DELETE: api/User/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUser(string id)
        // {
        //     if (_context.Users == null)
        //     {
        //         return NotFound();
        //     }
        //     var user = await _context.Users.FindAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Users.Remove(user);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.UserName == id)).GetValueOrDefault();
        }


        private string GenerateJwtToken(User user){

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string secret = _jwtConfig.Secret;
            // string secret = Environment.GetEnvironmentVariable("JWT_SECRET");
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Secret cannot be null or empty.", nameof(secret));
            }

            // string secret = "gj5f7h8d9k3l2j4h6g5f4d3s2a1s2d3f4g5h6j7k8l9";
            var jwtTokenHadler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            // var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);


            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(new [] {
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.Audience),
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512
                )
            };

            var token = jwtTokenHadler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHadler.WriteToken(token);
            return jwtToken;
            
                
        }

        // private string CreateToken(ClaimsIdentity claims)
        // {
        //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
        //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //     var token = new JwtSecurityToken(
        //         issuer: Configuration["Jwt:Issuer"],
        //         audience: Configuration["Jwt:Audience"],
        //         claims: claims,
        //         expires: DateTime.Now.AddMinutes(30),
        //         signingCredentials: creds);

        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }

        [HttpPost("signin")]
        // [Authorize]
        // [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserSignInRequest userSignInRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userSignInRequest.UserName);
                if (user == null)
                {
                    return NotFound("User not found!");
                }
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, userSignInRequest.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    return Ok(new UserSignInResponse()
                    {
                        Success = true,
                        Token = GenerateJwtToken(user)
                    });
                }
                else
                {
                    return Unauthorized("Invalid credentials!");
                }
            }
            return BadRequest("Invalid request");
        }
    //     {
    //         if (_context.Users == null)
    //         {
    //             return NotFound();
    //         }
    //         var user = await _context.Users.FindAsync(u.UserName);
    //         if (user == null)
    //         {
    //             return NotFound();
    //         }
    //         if (user.Password != u.Password)
    //         {
    //             return Unauthorized();
    //         }
    //         var claims = new List<Claim>
    //         {
    //             new Claim(ClaimTypes.Name, user.UserName)
    //         };
    //         var token = CreateToken(claims);
    //         return Ok(new { token });
    //     }
    }
}
