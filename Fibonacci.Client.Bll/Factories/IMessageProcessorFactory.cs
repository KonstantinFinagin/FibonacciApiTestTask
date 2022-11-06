using Fibonacci.Client.Bll.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.Client.Bll.Factories
{
    public interface IMessageProcessorFactory
    {
        IMessageProcessor<T> GetMessageProcessor<T>();
    }
}
