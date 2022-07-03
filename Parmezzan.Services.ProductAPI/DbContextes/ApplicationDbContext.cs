using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.ProductAPI.Models;

namespace Parmezzan.Services.ProductAPI.DbContextes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
