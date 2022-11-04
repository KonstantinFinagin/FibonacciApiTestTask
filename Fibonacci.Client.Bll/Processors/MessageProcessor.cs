using AutoMapper;
using Fibonacci.Client.Contracts;

namespace Fibonacci.Client.Bll.Processors
{
    public class MessageProcessor : IMessageProcessor<NextFibonacciCalculatedResultMessage>
    {
        private readonly IMapper _mapper;

        public MessageProcessor(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task ProcessMessageAsync(NextFibonacciCalculatedResultMessage message)
        {
            // when a message is recieved from the queue - do the roundtrip

        }
    }
}
