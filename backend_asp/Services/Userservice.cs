using backend_asp.Models;
using backend_asp.Configurations;
using backend_asp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
namespace backend_asp.Services
{
    public class UserService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUserRepo _userRepo = default!;
        private readonly IPasswordHasher<User> _passwordHasher;
        // new PasswordHasher<User>();


        public UserService(
            IOptionsMonitor<JwtConfig> jwtConfig,
            IUserRepo userRepo,
            IPasswordHasher<User> passwordHasher
        )
        {
            _jwtConfig = jwtConfig.CurrentValue;
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserSignUpResponse?> SignUpAsync(UserSignUpRequest userSignUpRequest)
        {
            if (userSignUpRequest == null)
            {
                throw new ArgumentNullException(nameof(userSignUpRequest));
            }

            var existingUser = await _userRepo.FindByNameAsync(userSignUpRequest.UserName);
            if (existingUser != null)
            {
                return new UserSignUpResponse
                {
                    Token = null,
                    Success = false,
                    Errors = new List<string> { "UserAlreadyExists" }
                };
            }

            var newUser = new User() { Email = userSignUpRequest.Email, UserName = userSignUpRequest.UserName };
            var hashedPassword = _passwordHasher.HashPassword(newUser, userSignUpRequest.Password);
            newUser.Password = hashedPassword;
            var result = await _userRepo.AddUserAsync(newUser);

            if (result)
            {
                return new UserSignUpResponse
                {
                    Token = GenerateJwtToken(newUser),
                    Success = true,
                    Errors = new List<string>()
                };
            }
            else
            {
                return new UserSignUpResponse
                {
                    Token = null,
                    Success = false,
                    Errors = new List<string> { "FailedToCreateUser" }
                };
            }
        }

        public async Task<UserSignInResponse?> SignInAsync(UserSignInRequest userSignInRequest)
        {
            if (userSignInRequest == null)
            {
                throw new ArgumentNullException(nameof(userSignInRequest));
            }

            var user = await _userRepo.FindByNameAsync(userSignInRequest.UserName);
            if (user == null)
            {
                return new UserSignInResponse
                {
                    Token = null,
                    Success = false,
                    Errors = new List<string> { "UserDoesNotExist" }
                };
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userSignInRequest.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return new UserSignInResponse
                {
                    Token = GenerateJwtToken(user),
                    Success = true,
                    Errors = new List<string>()
                };
            }
            else
            {
                return new UserSignInResponse
                {
                    Token = null,
                    Success = false,
                    Errors = new List<string> { "InvalidPassword" }
                };
            }
        }

        private string GenerateJwtToken(User user)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string secret = _jwtConfig.Secret;
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Secret cannot be null or empty.", nameof(secret));
            }

            var jwtTokenHadler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.Issuer),
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

    }
}