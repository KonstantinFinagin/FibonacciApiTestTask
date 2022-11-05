using Fibonacci.Client.Bll.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Fibonacci.Client.Tasks
{
    public class ListenerService : IListenerService
    {
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ListenerService(IMessageService messageService, IConfiguration configuration, ILogger logger)
        {
            _messageService = messageService;
            _configuration = configuration;
            _logger = logger;
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
