using EasyNetQ;

namespace Fibonacci.Client.Bll.Services
{
    public interface IMessageService
    {
        List<string> QueueNames { get; }

        Task ProcessMessageAsync(byte[] bytes, MessageProperties messageProperties, MessageReceivedInfo messageReceivedInfo);
    }
}
