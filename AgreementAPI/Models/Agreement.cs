using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AgreementAPI.Models
{

    public class Agreement
    {
        private static readonly IList<String> ValidCodeRates = new ReadOnlyCollection<string>
        (new List<String> { "VILIBOR1m", "VILIBOR3m", "VILIBOR6m", "VILIBOR1y" });

        private string _BaseCodeRate;

        public long Id { get; set; }
        public Customer CustomerDetails { get; set; }
        public long Amount { get; set; }
        public string BaseCodeRate {
            get
            {
                return _BaseCodeRate;
            }
            set
            {
                if (ValidCodeRates.Contains(value))
                {
                    _BaseCodeRate = value;
                }
                else
                {
                    throw (new ArgumentOutOfRangeException("BaseCodeRate", value, "Rate must be one of the following:VILIBOR1m,VILIBOR3m,VILIBOR6m,VILIBOR1y"));
                }
            }
                
         }
        public int Duration { get; set; }
        public double Margin { get; set; }

        public static bool ValidBaseCodeRate(string baseCodeRate)
        {
            return ValidCodeRates.Contains(baseCodeRate);
        }

        public Agreement ShallowCopy()
        {
            return (Agreement)this.MemberwiseClone();
        }

        public double CalculateInterestRate()
        {
            double currentRate = VilLibRate.GetCurrentRate(BaseCodeRate);
            return currentRate+Margin;
        }

    }
}