using Fibonacci.Api.Bll;
using Fibonacci.Api.Bll.Notification;
using Fibonacci.Api.Bll.Validation;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Calculator.Services;
using Fibonacci.Common.Validation;
using Moq;
using Serilog;
using Xunit;

namespace Fibonacci.Api.Tests
{
    public class FibonacciServiceTest
    {
        private FibonacciApiApiService _apiApiService;
        private Mock<IValidatorsFactory> _validatorsFacotyMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<ILogger> _loggerMock;

        private Mock<IFibonacciCalculatorService> _fibonacciCalculatorService;

        public FibonacciServiceTest()
        {
            _validatorsFacotyMock = new Mock<IValidatorsFactory>();
            _notificationServiceMock = new Mock<INotificationService>(); 
            _loggerMock = new Mock<ILogger>();

            _fibonacciCalculatorService = new Mock<IFibonacciCalculatorService>();

            _validatorsFacotyMock.Setup(m => m.For<CalculateNextFibonacciRequest>())
                .Returns(new CalculateNextFibonacciRequestValidator(new FibonacciCalculatorService()));

            _apiApiService = new FibonacciApiApiService(
                _validatorsFacotyMock.Object, 
                _notificationServiceMock.Object,
                _fibonacciCalculatorService.Object);
        } 

        [Fact]
        async Task CalculationCallsCalculatorService()
        {
            var request = new CalculateNextFibonacciRequest()
            {
                Value = "5",
                PreviousValue = "3",
                TaskId = 1,
            };

            await _apiApiService.CalculateNextFibonacciNumber(request);
        }
    }
}