using System.Security.Cryptography.X509Certificates;
using Fibonacci.Api.Bll;
using Fibonacci.Api.Bll.Notification;
using Fibonacci.Api.Bll.Validation;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Common.Validation;
using FluentValidation;
using Moq;
using Serilog;
using Xunit;

namespace Fibonacci.Api.Tests
{
    public class FibonacciServiceTest
    {
        private FibonacciService _service;
        private Mock<IValidatorsFactory> _validatorsFacotyMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<ILogger> _loggerMock;

        public FibonacciServiceTest()
        {
            _validatorsFacotyMock = new Mock<IValidatorsFactory>();
            _notificationServiceMock = new Mock<INotificationService>(); 
            _loggerMock = new Mock<ILogger>();

            _validatorsFacotyMock.Setup(m => m.For<CalculateNextFibonacciRequest>()).Returns(new CalculateNextFibonacciRequestValidator());

            _service = new FibonacciService(
                _validatorsFacotyMock.Object, 
                _notificationServiceMock.Object);
        } 

        [Theory]
        [InlineData(0, null, 1, null)]
        [InlineData(1, null, null, "Previous value should not be null when calculating next value for 1")]
        [InlineData(1, 0, 1, null)]
        [InlineData(1, 1, 2, null)]
        [InlineData(2, null, 3, null)]
        [InlineData(3, null, 5, null)]
        [InlineData(4, null, null, "Please pass in a valid Fibonacci number")]
        [InlineData(8, null, 13, null)]
        async Task CalculationUnitTest(int value, int? prevValue, int? result, string exceptionMessage)
        {
            var request = new CalculateNextFibonacciRequest()
            {
                Value = value,
                PreviousValue = prevValue,
                SessionId = 1,
                TaskId = 1,
            };

            if (exceptionMessage == null)
            {
                var nextNumberResponse = await _service.CalculateNextFibonacciNumber(request);
                Assert.Equal(result, (long?) nextNumberResponse.Result);
                Assert.Equal(1, nextNumberResponse.SessionId);
                Assert.Equal(1, nextNumberResponse.TaskId);
                Assert.Equal(value, nextNumberResponse.Previous);
            }

            else
            {
                var exception = await Assert.ThrowsAsync<ValidationException>(() => _service.CalculateNextFibonacciNumber(request));
                Assert.Contains(exceptionMessage, exception.Message);
            }
        }
    }
}