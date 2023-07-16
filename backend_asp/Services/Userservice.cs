using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_asp.Models;
using backend_asp.Data;
using backend_asp.Configurations;

namespace backend_asp.Services
{
    public class UserService
    {
        // private readonly JwtConfig _jwtConfig;
        private readonly UserContext _userContext;
        public UserService(
            // JwtConfig jwtConfig,
            UserContext userContext
        )
        {
            // _jwtConfig = jwtConfig;
            _userContext = userContext;
        }

    //    public async Task<User>? FindByNameAsync(string username)
    //    {
    //     //    username = username.ToLower();
    //        return await _userContext.Users.FindAsync(username);
    //    }


    }
}