using System.Collections.Generic;

namespace Fibonacci.Common.Constants
{
    public static class QueueNameConstants
    {
        public static List<string> AllQueues = new()
        {
            FibonacciUpdatesQueue
        };

        public static string FibonacciUpdatesQueue => "Fibonacci.Updates";
    }
}
