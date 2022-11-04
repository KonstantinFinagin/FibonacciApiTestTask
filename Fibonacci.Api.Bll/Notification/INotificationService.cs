using Fibonacci.Api.Contracts;
using Fibonacci.Api.Contracts.Responses;

namespace Fibonacci.Api.Bll.Notification
{
    public interface INotificationService
    {
        Task NotifyNextFibonacciCalculated(CalculateNextFibonacciResponse nextFibonacciResponse);
    }
}
