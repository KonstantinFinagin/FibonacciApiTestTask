using Autofac;
using Fibonacci.Calculator.Services;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Calculator.Modules
{
    public class FibonacciCalculatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FibonacciCalculatorService>().As<IFibonacciCalculatorService>().SingleInstance();
        }
    }
}
