using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.OrderAPI.Models;

namespace Parmezzan.Services.OrderAPI.DbContextes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
