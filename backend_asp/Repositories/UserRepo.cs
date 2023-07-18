using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend_asp.Models;
using backend_asp.Data;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace backend_asp.Repositories
{

    public class UserRepo : IUserRepo
    {
        public UserContext _userContext { get; set; }
        public UserRepo(UserContext userContext)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<Boolean> AddUserAsync(User user)
        {
            var result = await _userContext.Users.AddAsync(user);
            if (result != null)
            {
                await _userContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User?> FindByNameAsync(string username)
        {
            return await _userContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}