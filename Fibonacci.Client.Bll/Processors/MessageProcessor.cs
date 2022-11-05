using AutoMapper;
using Fibonacci.Client.Contracts;
using Serilog;

namespace Fibonacci.Client.Bll.Processors
{
    public class MessageProcessor : IMessageProcessor<NextFibonacciCalculatedResultMessage>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MessageProcessor(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ProcessMessageAsync(NextFibonacciCalculatedResultMessage message)
        {
            // when a message is recieved from the queue - do the roundtrip

            _logger.Debug($"{message.Result}: {message.GeneratedOn}");
            
        }
    }
}
