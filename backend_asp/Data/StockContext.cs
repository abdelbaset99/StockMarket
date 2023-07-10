using Microsoft.EntityFrameworkCore;
using backend_asp.Models;
using MySql.EntityFrameworkCore.Extensions;


namespace backend_asp.Data
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options)
            : base(options)
        {
        }
        public DbSet<Stock>? Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=StockMarket;Uid=root;Pwd=root;", new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }
    }
}
