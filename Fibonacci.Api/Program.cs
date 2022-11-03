using Fibonacci.Common;
using Microsoft.Extensions.Hosting;

namespace Fibonacci.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = new WebHostRunner<Startup>("FibonacciApi");
            service.HostBuilder.Build().Run();
        }

        /// <summary>
        /// Required for migrations in EF Core
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => new WebHostRunner<Startup>("FibonacciApi").HostBuilder;
    }
}
