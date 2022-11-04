using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Client.Contracts;
using Fibonacci.Common.Constants;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Api.Bll.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IAdvancedBus _bus;
        private readonly Exchange _exchange;

        public NotificationService(IConfiguration configuration)
        {
            var rabbitMq = configuration.GetSection("RabbitMq").GetChildren().ToList();

            var host = rabbitMq.First(t => t.Key == "host").Value;
            var virtualHost = rabbitMq.First(t => t.Key == "virtualhost").Value;
            var username = rabbitMq.First(t => t.Key == "host").Value;
            var password = rabbitMq.First(t => t.Key == "host").Value;

            var connectionString = $"host={host};virtualhost={virtualHost};username={username};password={password}";

            _bus = RabbitHutch.CreateBus(connectionString).Advanced;
            _exchange = _bus.ExchangeDeclare(ExchangeNameConstants.CrmExchange, ExchangeType.Fanout);
        }

        public async Task NotifyNextFibonacciCalculated(CalculateNextFibonacciResponse nextFibonacciResponse)
        {
            var message = new NextFibonacciCalculatedResultMessage
            {
                Previous = nextFibonacciResponse.Previous,
                Result = nextFibonacciResponse.Result,
                GeneratedOn = DateTime.UtcNow,
                SessionId = nextFibonacciResponse.SessionId,
                TaskId = nextFibonacciResponse.TaskId
            };

            var m = new Message<NextFibonacciCalculatedResultMessage>(message);
            await _bus.PublishAsync(_exchange, string.Empty, true, m);
        }
    }
}
