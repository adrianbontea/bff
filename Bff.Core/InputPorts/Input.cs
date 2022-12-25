using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.InputPorts
{
    public class Input
    {
        public Input(string userId, string message, Stream faceImage = null)
        {
            UserId = userId;
            Message = message;
            FaceImage = faceImage;
        }

        public string UserId { get; }

        public string Message { get; }

        public Stream FaceImage { get; }
    }
}
