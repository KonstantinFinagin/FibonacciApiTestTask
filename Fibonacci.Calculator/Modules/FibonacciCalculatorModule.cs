using Autofac;
using Fibonacci.Calculator.Services;

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
