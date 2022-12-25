using Bff.Core.InputPorts;
using Bff.Core.OutputPorts;

namespace Bff.Core
{
    public class BffService : IBffService
    {
        private readonly IEmotionService _emotionsService;
        private readonly IConversationService _conversationService;

        public BffService(IEmotionService emotionsService, IConversationService conversationService)
        {
            _emotionsService = emotionsService;
            _conversationService = conversationService;
        }

        public async Task<Output> InteractAsync(Input input)
        {
            if(input.FaceImage != null)
            {
                var emotionsResult = await _emotionsService.GetEmotionsAsync(input.FaceImage);

                if(emotionsResult.IsNegative)
                {
                    return new Output($"Hmmm I see some negative feelings on your face...mostly {emotionsResult.GetMaxScore().Name}...Do you want to talk more about that?");
                }

                if (emotionsResult.IsPositive)
                {
                    return new Output($"I am really happy to see that {emotionsResult.GetMaxScore().Name.Replace("Score", string.Empty).ToLower()} on your face!");
                }
            }

            var messageSentimentResult = await _emotionsService.GetSentimentsAsync(input.Message);

            if (messageSentimentResult.Sentiment == Sentiment.Negative || messageSentimentResult.Sentiment == Sentiment.Mixed)
            {
                var negativeOpinions = messageSentimentResult.Sentences.SelectMany(s => s.Opinions.Where(o => o.Sentiment == Sentiment.Negative).Select(o => o.Text)).ToList();
                var negativeOpinionsMessage = string.Join(", ", negativeOpinions.ToArray(), 0, negativeOpinions.Count - 1) + ", and " + negativeOpinions.LastOrDefault();

                return new Output($"Hmmm looks like you are not very happy about {negativeOpinionsMessage}...Do you want to talk more about that?", true);
            }

            return new Output(await _conversationService.ChatAsync(input.Message));
        }
    }
}
