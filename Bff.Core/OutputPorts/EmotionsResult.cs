using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public class EmotionsResult
    {
        public decimal AngerScore { get; set; }

        public decimal ContemptScore { get; set; }

        public decimal DisgustScore { get; set; }

        public decimal FearScore { get; set; }

        public decimal HappinessScore { get; set; }

        public decimal NeutralScore { get; set; }

        public decimal SadnessScore { get; set; }

        public decimal SurpriseScore { get; set; }
    }
}
