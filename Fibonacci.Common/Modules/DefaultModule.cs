using Autofac;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace Fibonacci.Common.Modules
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SerilogLoggerFactory>().As<ILoggerFactory>();
        }
    }
}
