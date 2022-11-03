using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.Api.Contracts
{
    public class CalculateNextFibonacciRequest
    {
        public int SessionId { get; set; }

        public Guid TaskId { get; set; }

        public long Value { get; set; }

        public long? PreviousValue { get; set; }
    }
}
