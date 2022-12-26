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

        public double AngerScore { get; set; }

        public double ContemptScore { get; set; }

        public double DisgustScore { get; set; }

        public double FearScore { get; set; }

        public double HappinessScore { get; set; }

        public double NeutralScore { get; set; }

        public double SadnessScore { get; set; }

        public double SurpriseScore { get; set; }

        public bool IsNegative => _negatives.Contains(GetMaxScore().Name); 

        public bool IsPositive => _positives.Contains(GetMaxScore().Name);

        public (string Name, double Value) GetMaxScore()
        {
            var pairs = GetType().GetProperties()
                                 .Where(p => p.PropertyType == typeof(double))
                                 .Select(p => new
                                 {
                                     Name = p.Name,
                                     Value = (double)p.GetValue(this)
                                 });

            string maxName = string.Empty;
            double maxValue = 0;

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
