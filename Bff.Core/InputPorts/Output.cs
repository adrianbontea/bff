using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.InputPorts
{
    public class Output
    {
        public Output(string message, bool shouldCaptureFaceImage = false)
        {
            Message = message;
            ShouldCaptureFaceImage = shouldCaptureFaceImage;
        }

        public string Message { get; }

        public bool ShouldCaptureFaceImage { get; }
    }
}
