using Serilog;

namespace Fibonacci.Common.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLoggerForContext<TContext>();
    }
}
