namespace Fibonacci.Common.Communication
{
    public interface IServiceHttpClientFactory
    {
        T CreateClient<T>(EServiceType serviceType);
    }
}
