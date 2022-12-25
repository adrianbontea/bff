using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bff.Core.OutputPorts
{
    public class EmotionsResult
    {
        private readonly List<string> _negatives = new List<string> { nameof(AngerScore), nameof(DisgustScore), nameof(FearScore), nameof(SadnessScore) };
        private readonly List<string> _positives = new List<string> { nameof(HappinessScore), nameof(SurpriseScore) };

        public decimal AngerScore { get; set; }

        public decimal ContemptScore { get; set; }

        public decimal DisgustScore { get; set; }

        public decimal FearScore { get; set; }

        public decimal HappinessScore { get; set; }

        public decimal NeutralScore { get; set; }

        public decimal SadnessScore { get; set; }

        public decimal SurpriseScore { get; set; }

        public bool IsNegative => _negatives.Contains(GetMaxScore().Name); 

        public bool IsPositive => _positives.Contains(GetMaxScore().Name);

        public (string Name, decimal Value) GetMaxScore()
        {
            var pairs = GetType().GetProperties()
                                 .Where(p => p.PropertyType == typeof(decimal))
                                 .Select(p => new
                                 {
                                     Name = p.Name,
                                     Value = (decimal)p.GetValue(this)
                                 });

            string maxName = string.Empty;
            decimal maxValue = 0;

            foreach (var item in pairs)
            {
                if (item.Value > maxValue)
                {
                    maxName = item.Name;
                    maxValue = item.Value;
                }
            }

            return (maxName, maxValue);
        }
    }
}
