using Serilog;

namespace Fibonacci.Common.Logging
{
    public class SerilogLoggerFactory : ILoggerFactory
    {
        public ILogger GetLoggerForContext<TContext>()
        {
            return Log.ForContext<TContext>();
        }
    }
}