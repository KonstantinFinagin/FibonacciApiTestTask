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
        private FibonacciApiService _apiService;
        private Mock<IValidatorsFactory> _validatorsFactoryMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<ILogger> _loggerMock;

        private Mock<IFibonacciCalculatorService> _fibonacciCalculatorService;

        public FibonacciServiceTest()
        {
            _validatorsFactoryMock = new Mock<IValidatorsFactory>();
            _notificationServiceMock = new Mock<INotificationService>(); 
            _loggerMock = new Mock<ILogger>();

            _fibonacciCalculatorService = new Mock<IFibonacciCalculatorService>();

            _validatorsFactoryMock.Setup(m => m.For<CalculateNextFibonacciRequest>())
                .Returns(new CalculateNextFibonacciRequestValidator(new FibonacciCalculatorService())).Verifiable();

            _apiService = new FibonacciApiService(
                _validatorsFactoryMock.Object, 
                _notificationServiceMock.Object,
                _fibonacciCalculatorService.Object);
        } 

        [Fact]
        async Task CalculationCallsCalculatorService()
        {
            var request = new CalculateNextFibonacciRequest()
            {
                Value = "5",
                TaskId = 1,
            };

            await _apiService.CalculateNextFibonacciNumber(request);

            _validatorsFactoryMock.Verify(v => v.For<CalculateNextFibonacciRequest>(), Times.Once);
            _fibonacciCalculatorService.Verify(v => v.CalculateNextFibonacci("5", null), Times.Once);
        }
    }
}