using Microsoft.EntityFrameworkCore;
using backend_asp.Models;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace backend_asp.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options) {}
        public DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=StockMarket;Uid=root;Pwd=root;", new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }
    }
}
