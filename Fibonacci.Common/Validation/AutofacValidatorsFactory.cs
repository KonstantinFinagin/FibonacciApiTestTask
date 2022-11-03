using Autofac;
using FluentValidation;

namespace Fibonacci.Common.Validation
{
    public class AutofacValidatorsFactory : IValidatorsFactory
    {
        private readonly IComponentContext _context;

        public AutofacValidatorsFactory(IComponentContext context)
        {
            _context = context;
        }

        public IValidator<T> For<T>()
        {
            return _context.Resolve<AbstractValidator<T>>();
        }
    }
}
