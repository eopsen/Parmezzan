using Parmezzan.MessageBus;

namespace Parmezzan.Services.ShoppingCartAPI.RabbitMQSender
{
    public class RabbitMQCartMessageSender : IRabbitMQCartMessageSender
    {
        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            throw new NotImplementedException();
        }
    }
}
