using Fibonacci.Client.Bll.Processors;

namespace Fibonacci.Client.Bll.Factories
{
    public interface IMessageProcessorFactory
    {
        IMessageProcessor<T> GetMessageProcessor<T>();
    }
}
