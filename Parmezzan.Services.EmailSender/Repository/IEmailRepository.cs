using Parmezzan.Services.EmailSender.Messages;

namespace Parmezzan.Services.EmailSender.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
