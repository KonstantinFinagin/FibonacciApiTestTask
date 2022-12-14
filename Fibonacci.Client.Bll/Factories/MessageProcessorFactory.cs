using Autofac;
using Fibonacci.Client.Bll.Processors;

namespace Fibonacci.Client.Bll.Factories
{
    public class MessageProcessorFactory : IMessageProcessorFactory
    {
        private readonly ILifetimeScope _rootScope;

        public MessageProcessorFactory(ILifetimeScope rootScope)
        {
            _rootScope = rootScope;
        }

        public IMessageProcessor<T> GetMessageProcessor<T>()
        {
            return _rootScope.Resolve<IMessageProcessor<T>>();
        }
    }
}
