using Fibonacci.Api.Contracts;
using Fibonacci.Api.Contracts.Responses;
using Fibonacci.Common.Exceptions;

namespace Fibonacci.Api.Bll.Notification
{
    public interface INotificationService
    {
        Task NotifyNextFibonacciCalculated(CalculateNextFibonacciResponse nextFibonacciResponse);

        Task NotifyCalculationEnded(int taskId, DomainException ex);
    }
}
