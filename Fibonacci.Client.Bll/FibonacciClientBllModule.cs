using Autofac;
using EasyNetQ.ConnectionString;
using Fibonacci.Client.Bll.Services;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace Fibonacci.Client.Bll
{
    public class FibonacciClientBllModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MessageService).Assembly)
                .Where(t => t.Name.EndsWith("Factory") 
                            || t.Name.EndsWith("Service") 
                            || t.Name.EndsWith("Processor"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            /*
            builder.RegisterEasyNetQ(c =>
            {
                var conf = c.Resolve<IConfiguration>().GetSection("RabbitMq");
                var connectionStringParser = new ConnectionStringParser();
                string connectionString = $"host={conf["Host"]}:{conf["Port"]};virtualHost={conf["VirtualHost"]};username={conf["UserName"]};password={conf["Password"]};prefetchCount=1";
                return connectionStringParser.Parse(connectionString);
            });
            */
        }
    }
}
