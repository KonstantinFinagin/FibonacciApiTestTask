using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.Api.Contracts
{
    public class CalculateResponse
    {
        public Guid TaskId { get; set; }

        public long Result { get; set; }

        public long? Previous { get; set; }
    }
}
