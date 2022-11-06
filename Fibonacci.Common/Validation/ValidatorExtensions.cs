using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.TestHelper;

namespace Fibonacci.Common.Validation
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Performs validation asynchronously using validation context and then throws an exception if validation fails.
        /// </summary>
        public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, ValidationContext<T> context, CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(context, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public static async Task<TestValidationResult<T>> ValidateContextAsync<T>(
            this IValidator<T> validator, 
            ValidationContext<T> context) where T : class
        {
            var validationResult = await validator.ValidateAsync(context);
            return new TestValidationResult<T>(validationResult);
        }
    }
}
