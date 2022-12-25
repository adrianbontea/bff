using Bff.Core.OutputPorts;

namespace Bff.Service.Adapters
{
    public class AzureEmotionService : IEmotionService
    {
        public async Task<EmotionsResult> GetEmotionsAsync(Stream faceImage)
        {
            await Task.Delay(100);
            return new EmotionsResult { NeutralScore = 0.9m };
        }

        public async Task<MessageSentimentResult> GetSentimentsAsync(string message)
        {
            await Task.Delay(100);
            return new MessageSentimentResult { Text = message, Sentiment = Sentiment.Neutral, NeutralScore = 0.9m };
        }
    }
}
