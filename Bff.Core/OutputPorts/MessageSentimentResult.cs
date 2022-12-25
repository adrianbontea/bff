using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public class MessageSentimentResult: SentimentResult
    {
        public MessageSentimentResult()
        {
            Sentences = new List<SentenceSentimentResult>();
        }

        public IList<SentenceSentimentResult> Sentences { get; }

        public string GetNegativeOpinionsDescription()
        {
            var negativeOpinions = Sentences.SelectMany(s => s.Opinions.Where(o => o.Sentiment == Sentiment.Negative).Select(o => o.Text)).ToList();
            return string.Join(", ", negativeOpinions.ToArray(), 0, negativeOpinions.Count - 1) + ", and " + negativeOpinions.LastOrDefault();
        }
    }
}
