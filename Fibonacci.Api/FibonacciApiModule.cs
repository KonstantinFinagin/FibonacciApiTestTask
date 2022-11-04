using Autofac;
using Fibonacci.Api.Bll.Mapping;
using Fibonacci.Api.Bll.Validation;
using Fibonacci.Api.Contracts;
using Fibonacci.Common.Validation;

namespace Fibonacci.Api
{
    public class FibonacciApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new ValidatorsModule(typeof(CalculateNextFibonacciRequestValidator).Assembly));

            builder.RegisterAssemblyTypes(typeof(FibonacciMappingProfile).Assembly)
                .Where(t => t.Name.EndsWith("Service") 
                         || t.Name.EndsWith("Factory"))
                    .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
