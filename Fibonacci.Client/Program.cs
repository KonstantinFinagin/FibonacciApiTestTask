using Autofac;
using Fibonacci.Common.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using IContainer = Autofac.IContainer;

namespace Fibonacci.Client
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CompositionRoot().Resolve<Application>().Run();
        }

        private static IContainer CompositionRoot()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var builder = new ContainerBuilder();

            builder.RegisterModule<FibonacciClientModule>();

            builder.RegisterType<Application>().AsSelf();
            builder.RegisterInstance(Log.Logger).AsImplementedInterfaces();
            builder.RegisterInstance(configuration).AsImplementedInterfaces();

            return builder.Build();
        }
    }
}

