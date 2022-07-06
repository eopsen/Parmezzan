using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.ShoppingCartAPI.Models;

namespace Parmezzan.Services.ShoppingCartAPI.DbContextes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
