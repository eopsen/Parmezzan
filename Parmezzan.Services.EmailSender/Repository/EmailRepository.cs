using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.EmailSender.DbContextes;
using Parmezzan.Services.EmailSender.Messages;
using Parmezzan.Services.EmailSender.Models;

namespace Parmezzan.Services.EmailSender.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext;

        public EmailRepository(DbContextOptions<ApplicationDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            //TODO do magic and send emails

            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Zamówienie - {message.OrderId}, payment status - {message.Status}"
            };

            await using var _db = new ApplicationDbContext(_dbContext);
            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
