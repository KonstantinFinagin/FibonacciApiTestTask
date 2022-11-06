using Autofac;
using EasyNetQ.ConnectionString;
using Fibonacci.Calculator.Modules;
using Fibonacci.Client.Bll.Services;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace Fibonacci.Client.Bll
{
    public class FibonacciClientBllModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<FibonacciCalculatorModule>();
            builder.RegisterAssemblyTypes(typeof(MessageService).Assembly)
                .Where(t => t.Name.EndsWith("Factory") 
                            || t.Name.EndsWith("Service") 
                            || t.Name.EndsWith("Processor"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
