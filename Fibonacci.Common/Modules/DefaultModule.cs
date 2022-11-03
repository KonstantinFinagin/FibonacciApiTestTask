using Autofac;
using Fibonacci.Common.Communication;
using Fibonacci.Common.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace Fibonacci.Common.Modules
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceBaseUrlProvider>().As<IServiceBaseUrlProvider>().InstancePerLifetimeScope();
            builder.RegisterType<RefitServiceHttpClientFactory>().As<IServiceHttpClientFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SerilogLoggerFactory>().As<ILoggerFactory>();
            builder.RegisterConfiguration<CommunicationOptions>("Rpc");
        }
    }
}
