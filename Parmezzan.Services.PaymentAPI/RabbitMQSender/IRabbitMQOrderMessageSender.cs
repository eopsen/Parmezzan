using Parmezzan.MessageBus;

namespace Parmezzan.Services.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQPaymentMessageSender
    {
        void SendMessage(BaseMessage baseMessage, String queueName);
    }
}
