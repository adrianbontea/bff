using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public class SentimentResult
    {
        public string Text { get; set; }

        public Sentiment Sentiment { get; set; }

        public decimal PositiveScore { get; set; }

        public decimal NegativeScore { get; set; }

        public decimal NeutralScore { get; set; }
    }
}
