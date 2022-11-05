using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Client.Contracts;
using Fibonacci.Common.Constants;

namespace Fibonacci.Api.Bll.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IAdvancedBus _bus;
        private readonly Exchange _exchange;

        public NotificationService(IAdvancedBus bus)
        {
            _bus = bus;

            try
            {
                _exchange = _bus.ExchangeDeclare(ExchangeNameConstants.CrmExchange, ExchangeType.Fanout);
            }
            catch (Exception ex)
            {
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

            await _bus.PublishAsync(_exchange, string.Empty, true, m);
        }
    }
}
