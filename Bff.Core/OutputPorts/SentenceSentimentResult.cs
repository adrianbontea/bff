using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public class SentenceSentimentResult: SentimentResult
    {
        public SentenceSentimentResult()
        {
            Opinions = new List<SentimentResult>();
        }

        public IList<SentimentResult> Opinions { get; }
    }
}
