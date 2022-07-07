using Newtonsoft.Json;
using Parmezzan.PaymentProcessor;
using Parmezzan.Services.PaymentAPI.Messages;
using Parmezzan.Services.PaymentAPI.RabbitMQSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Parmezzan.Services.PaymentAPI.Messaging
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IProcessPayment _processPayment;
        private readonly IRabbitMQPaymentMessageSender _rabbitMQPaymentMessageSender;

        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQPaymentMessageSender rabbitMQPaymentMessageSender)
        {
            _processPayment = processPayment;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "orderpaymentprocesstopic", false, false, false, arguments: null);
            _rabbitMQPaymentMessageSender = rabbitMQPaymentMessageSender;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                PaymentRequestMessage paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(content);
                HandleMessage(paymentRequestMessage).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("orderpaymentprocesstopic", false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(PaymentRequestMessage paymentRequestMessage)
        {
            var updatePaymentResultMessage = new UpdatePaymentResultMessage()
            {
                Status = _processPayment.PaymentProcess(),
                OrderId = paymentRequestMessage.OrderId,
                Email = paymentRequestMessage.Email
            };

            _rabbitMQPaymentMessageSender.SendMessage(updatePaymentResultMessage, string.Empty);

        }
    }
}
