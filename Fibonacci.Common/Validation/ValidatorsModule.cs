using System.Reflection;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace Fibonacci.Common.Validation
{
    public class ValidatorsModule : Module
    {
        private readonly Assembly _validatorsAssembly;

        public ValidatorsModule(Assembly validatorsAssembly)
        {
            _validatorsAssembly = validatorsAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AutofacValidatorsFactory>()
                .As<IValidatorsFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(_validatorsAssembly)
                .Where(t => t.IsAssignableTo<IValidator>())
                .AsClosedTypesOf(typeof(AbstractValidator<>))
                .InstancePerLifetimeScope();
        }
    }
}
