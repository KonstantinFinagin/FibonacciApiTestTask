<<<<<<< HEAD
﻿using Fibonacci.Client.Bll.Services;
using Microsoft.Extensions.Configuration;
=======
﻿using Fibonacci.Api.Contracts.ExposedApis;
using Fibonacci.Api.Contracts.Requests;
using Fibonacci.Client.Bll.Services;
using Refit;
>>>>>>> 68be9a5c854e6a9c8c4d015e174c4506935d7a90
using Serilog;

namespace Fibonacci.Client.Tasks
{
    public class ListenerService : IListenerService
    {
<<<<<<< HEAD
        private readonly ILogger _logger = Log.ForContext<ListenerService>();

        public ListenerService(IMessageService messageService, IConfiguration configuration)
        {

=======
        private readonly IAdvancedBus _bus;
        private readonly ILogger _logger = Log.ForContext<ListenerService>();

        public ListenerService(IMessageService messageService, IAdvancedBus bus )
        {
            _bus = bus;
>>>>>>> 68be9a5c854e6a9c8c4d015e174c4506935d7a90
        }

        public async Task StartAsync(int nParallelRequests)
        {
<<<<<<< HEAD

=======
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
>>>>>>> 68be9a5c854e6a9c8c4d015e174c4506935d7a90
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
