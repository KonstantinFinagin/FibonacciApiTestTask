using Fibonacci.Api.Contracts.ExposedApis;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Client.Bll.Services;
using Refit;
using Serilog;

namespace Fibonacci.Client.Tasks
{
    public class ListenerService : IListenerService
    {
        private readonly IAdvancedBus _bus;
        private readonly ILogger _logger = Log.ForContext<ListenerService>();

        public ListenerService(IMessageService messageService, IAdvancedBus bus )
        {
            _bus = bus;
        }

        public async Task StartAsync(int nParallelRequests)
        {
            var response = await RestService.For<IFibonacciApi>(rpcHost).CalculateNextFibonacciRpc(new CalculateNextFibonacciRequest()
            {
                SessionId = 1, 
                TaskId = 1,
                Value = 8,
                PreviousValue = null
            });

            // save result to use in the following call

            Console.WriteLine(response.Result);
            Console.ReadLine();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
