using Azure.AI.TextAnalytics;
using Bff.Core.OutputPorts;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Options;

namespace Bff.Service.Adapters
{
    public class AzureEmotionService : IEmotionService
    {
        private readonly IFaceClient _faceClient;
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public AzureEmotionService(IFaceClient faceClient, TextAnalyticsClient textAnalyticsClient)
        {
            _faceClient = faceClient;
            _textAnalyticsClient = textAnalyticsClient;
        }

        public async Task<EmotionsResult> GetEmotionsAsync(Stream faceImage)
        {
            var faces = await _faceClient.Face.DetectWithStreamAsync(faceImage, returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Emotion }, recognitionModel: RecognitionModel.Recognition04);
            if(faces.Any())
            {
                var face = faces.First();
                return new EmotionsResult
                {
                    AngerScore = face.FaceAttributes.Emotion.Anger,
                    ContemptScore = face.FaceAttributes.Emotion.Contempt,
                    DisgustScore = face.FaceAttributes.Emotion.Disgust,
                    FearScore = face.FaceAttributes.Emotion.Fear,
                    HappinessScore = face.FaceAttributes.Emotion.Happiness,
                    NeutralScore = face.FaceAttributes.Emotion.Neutral,
                    SadnessScore = face.FaceAttributes.Emotion.Sadness,
                    SurpriseScore = face.FaceAttributes.Emotion.Surprise
                };
            }

            return new EmotionsResult { NeutralScore = 1 };
        }

        public async Task<MessageSentimentResult> GetSentimentsAsync(string message)
        {
            var messageSentimentResponse = await _textAnalyticsClient.AnalyzeSentimentAsync(message, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });
            
            if (messageSentimentResponse.Value != null)
            {
                var messageSentiment = messageSentimentResponse.Value;
                var messageSentimentResult = new MessageSentimentResult 
                { 
                    Text = message, 
                    Sentiment =  (Sentiment)messageSentiment.Sentiment, 
                    PositiveScore = messageSentiment.ConfidenceScores.Positive,
                    NeutralScore = messageSentiment.ConfidenceScores.Neutral,
                    NegativeScore = messageSentiment.ConfidenceScores.Negative
                };

                foreach(var sentence in messageSentiment.Sentences)
                {
                    var sentenceSentimentResult = new SentenceSentimentResult
                    {
                        Text = sentence.Text,
                        Sentiment = (Sentiment)sentence.Sentiment,
                        PositiveScore = sentence.ConfidenceScores.Positive,
                        NeutralScore = sentence.ConfidenceScores.Neutral,
                        NegativeScore = sentence.ConfidenceScores.Negative
                    };

                    foreach (var opinion in sentence.Opinions)
                    {
                        var opinionResult = new SentimentResult
                        {
                            Text = opinion.Target.Text,
                            Sentiment = (Sentiment)opinion.Target.Sentiment,
                            PositiveScore = opinion.Target.ConfidenceScores.Positive,
                            NeutralScore = opinion.Target.ConfidenceScores.Neutral,
                            NegativeScore = opinion.Target.ConfidenceScores.Negative
                        };

                        sentenceSentimentResult.Opinions.Add(opinionResult);
                    }

                    messageSentimentResult.Sentences.Add(sentenceSentimentResult);
                }

                return messageSentimentResult;
            }
            
            return new MessageSentimentResult { Text = message, Sentiment = Sentiment.Neutral, NeutralScore = 1 };
        }
    }
}
