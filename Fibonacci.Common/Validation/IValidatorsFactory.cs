using FluentValidation;

namespace Fibonacci.Common.Validation
{
    public interface IValidatorsFactory
    { 
        IValidator<T> For<T>();
    }
}
