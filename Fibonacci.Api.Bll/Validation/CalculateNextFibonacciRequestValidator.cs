using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Calculator.Services;
using FluentValidation;

namespace Fibonacci.Api.Bll.Validation
{
    public class CalculateNextFibonacciRequestValidator : AbstractValidator<CalculateNextFibonacciRequest>
    {
        private readonly IFibonacciCalculatorService _calculator;

        public CalculateNextFibonacciRequestValidator(IFibonacciCalculatorService calculator)
        {
            _calculator = calculator;

            When(a => a.Value == "1", () =>
            {
                RuleFor(a => a.PreviousValue).NotNull().NotEmpty().WithMessage("PreviousValue value should not be null when calculating next value for 1");
            });

            if (_calculator != null)
            {
                RuleFor(a => a.Value).Must(_calculator.IsFibonacci).WithMessage("Please pass in a valid Fibonacci number");
            }
        }
    }
}
