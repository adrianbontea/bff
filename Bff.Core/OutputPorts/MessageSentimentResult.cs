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
    }
}
