using Autofac;
using EasyNetQ.ConnectionString;
using Fibonacci.Api.Bll.Mapping;
using Fibonacci.Api.Bll.Validation;
using Fibonacci.Calculator.Modules;
using Fibonacci.Common.Validation;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Api
{
    public class FibonacciApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<FibonacciCalculatorModule>();
            builder.RegisterModule(new ValidatorsModule(typeof(CalculateNextFibonacciRequestValidator).Assembly));

            builder.RegisterEasyNetQ(c =>
            {
                var conf = c.Resolve<IConfiguration>().GetSection("RabbitMq");
                var connectionStringParser = new ConnectionStringParser();
                string connectionString = $"host={conf["Host"]}:{conf["Port"]};" +
                                          $"virtualHost={conf["VirtualHost"]};" +
                                          $"username={conf["UserName"]};" +
                                          $"password={conf["Password"]};" +
                                          $"prefetchCount=1000";

                return connectionStringParser.Parse(connectionString);
            });

            builder.RegisterAssemblyTypes(typeof(FibonacciMappingProfile).Assembly)
                .Where(t => t.Name.EndsWith("Service") 
                         || t.Name.EndsWith("Factory"))
                    .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
