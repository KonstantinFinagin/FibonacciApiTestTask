using Fibonacci.Api.Contracts;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Calculator;
using FluentValidation;

namespace Fibonacci.Api.Bll.Validation
{
    public class CalculateNextFibonacciRequestValidator : AbstractValidator<CalculateNextFibonacciRequest>
    {
        public CalculateNextFibonacciRequestValidator()
        {
            When(a => a.Value == 1, () =>
            {
                RuleFor(a => a.PreviousValue).NotNull().WithMessage("Previous value should not be null when calculating next value for 1");
            });

            RuleFor(a => a.Value).Must(FibonacciCalculator.IsFibonacci).WithMessage("Please pass in a valid Fibonacci number");
        }
    }
}
