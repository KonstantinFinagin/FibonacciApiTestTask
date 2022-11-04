using Fibonacci.Client.Bll.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Fibonacci.Client.Tasks
{
    public class ListenerService : IListenerService
    {
        private readonly ILogger _logger = Log.ForContext<ListenerService>();

        public ListenerService(IMessageService messageService, IConfiguration configuration)
        {

        }

        public async Task StartAsync(int nParallelRequests)
        {

        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
