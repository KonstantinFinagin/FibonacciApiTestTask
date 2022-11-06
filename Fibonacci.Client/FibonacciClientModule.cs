using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using EasyNetQ.ConnectionString;
using Fibonacci.Calculator.Modules;
using Fibonacci.Client.Bll;
using Fibonacci.Common.Modules;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Client
{
    public class FibonacciClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DefaultModule>();

            builder.RegisterModule(new FibonacciClientBllModule());

            builder.RegisterAutoMapper(typeof(Bll.Processors.MessageProcessor).Assembly);

            builder.RegisterAssemblyTypes(typeof(Application).Assembly)
                .Where(t => t.Name.EndsWith("Service") ||
                            t.Name.EndsWith("Processor") ||
                            t.Name.EndsWith("Factory"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

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
        }
    }
}
