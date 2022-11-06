using AutoMapper;
using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Client.Contracts;
using Fibonacci.Common.Constants;
using Fibonacci.Common.Exceptions;

namespace Fibonacci.Api.Bll.Notification
{
    /// <summary>
    ///     Notify consumers via service bus
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly IAdvancedBus _bus;
        private readonly IMapper _mapper;
        private readonly Exchange _exchange;

        public NotificationService(IAdvancedBus bus, IMapper mapper)
        {
            _bus = bus;
            _mapper = mapper;

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
            var message = _mapper.Map<NextFibonacciCalculationResultMessage>(nextFibonacciResponse);
            var m = new Message<NextFibonacciCalculationResultMessage>(message);
            await _bus.PublishAsync(_exchange, string.Empty, true, m);
        }

        public async Task NotifyCalculationEnded(int taskId, DomainException ex)
        {
            var message = new NextFibonacciCalculationResultMessage()
            {
                TaskId = taskId,
                ExceptionMessage = ex.Message,
                CalculationStopped = true
            };

            var m = new Message<NextFibonacciCalculationResultMessage>(message);
            await _bus.PublishAsync(_exchange, string.Empty, true, m);
        }
    }
}
