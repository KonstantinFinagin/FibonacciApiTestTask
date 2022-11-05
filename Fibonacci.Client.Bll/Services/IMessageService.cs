using EasyNetQ;

namespace Fibonacci.Client.Bll.Services
{
    public interface IMessageService
    {
        List<string> QueueNames { get; }

        Task ProcessMessageAsync(
            ReadOnlyMemory<byte> bytes, 
            MessageProperties messageProperties,
            MessageReceivedInfo messageReceivedInfo);
    }
}
