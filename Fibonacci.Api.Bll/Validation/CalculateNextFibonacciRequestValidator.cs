using Fibonacci.Api.Contracts;
using Fibonacci.Calculator;
using FluentValidation;

namespace Fibonacci.Api.Bll.Validation
{
    public class CalculateNextFibonacciRequestValidator : AbstractValidator<CalculateNextFibonacciRequest>
    {
        public CalculateNextFibonacciRequestValidator()
        {
            RuleFor(a => a.Value).Must(FibonacciCalculator.IsFibonacci).WithMessage("Please pass in a valid Fibonacci number");

            When(a => a.Value == 1, () =>
            {
                RuleFor(a => a.PreviousValue).NotNull();
            });
        }
    }
}
