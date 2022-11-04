using Autofac;
using Fibonacci.Client.Bll;

namespace Fibonacci.Client
{
    public class FibonacciClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new FibonacciClientBllModule());

            builder.RegisterAssemblyTypes(typeof(Application).Assembly)
                .Where(t => t.Name.EndsWith("Service") ||
                            t.Name.EndsWith("Processor") ||
                            t.Name.EndsWith("Factory"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
