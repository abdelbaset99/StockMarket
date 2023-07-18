using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend_asp.Models;
using backend_asp.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend_asp.Repositories
{
    public interface IUserRepo
    {
        UserContext _userContext { get; set; }
        Task<User?> FindByNameAsync(string username);
        Task<Boolean> AddUserAsync(User user);

    }
}