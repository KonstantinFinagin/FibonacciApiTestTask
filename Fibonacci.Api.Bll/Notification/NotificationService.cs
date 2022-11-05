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

            var host = rabbitMq.First(t => t.Key == "Host").Value;
            var virtualHost = rabbitMq.First(t => t.Key == "VirtualHost").Value;
            var username = rabbitMq.First(t => t.Key == "UserName").Value;
            var password = rabbitMq.First(t => t.Key == "Password").Value;

            var connectionString = $"host={host};virtualhost={virtualHost};username={username};password={password}";

            try
            {
                _bus = RabbitHutch.CreateBus(connectionString).Advanced;
                _exchange = _bus.ExchangeDeclare(ExchangeNameConstants.CrmExchange, ExchangeType.Fanout);
            }
            catch (Exception ex)
            {
                // TODO temporary exception, if needed - introduce exception handling mechanism
                throw new ApplicationException("Error initializing NotificationService. Before running the solution please create the following in RabbitMq: " +
                                               "Fibonacci.Exchange (fan-out) with a Fibonacci.Queue under the default guest/guest", ex);
            }
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

            // TODO implement bus re-init / retry strategy if communication fails depending on exception types
            await _bus.PublishAsync(_exchange, string.Empty, true, m);
        }
    }
}
