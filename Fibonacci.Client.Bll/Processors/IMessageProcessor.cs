namespace Fibonacci.Client.Bll.Processors
{
    public interface IMessageProcessor<in T>
    {
        Task ProcessMessageAsync(T message);
    }
}
