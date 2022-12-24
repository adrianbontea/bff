using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.InputPorts
{
    public class Output
    {
        public Output(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
