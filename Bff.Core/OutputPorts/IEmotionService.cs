using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public interface IEmotionService
    {
        Task<EmotionsResult> GetEmotionsAsync(Stream faceImage);

        Task<MessageSentimentResult> GetSentimentsAsync(string message);
    }
}
