using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.EmailSender.Models;

namespace Parmezzan.Services.EmailSender.DbContextes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
