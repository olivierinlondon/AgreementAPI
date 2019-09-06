using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace AgreementAPI.Models
{

    public class Agreement
    {
        public static readonly IList<String> Metrics = new ReadOnlyCollection<string>
        (new List<String> { "VILIBOR1m", "VILIBOR3m", "VILIBOR6m", "VILIBOR1y" });

        public Agreement()
        {
            Margin = 0;
        }
        public Agreement(double p_Margin)
        {
            Margin = p_Margin; 
        }

        public long Id { get; set; }
        public Customer CustomerDetails { get; set; }
        public long Amount { get; set; }
        public string BaseCodeRate { get; set; }
        public readonly double Margin;
        public int Duration { get; set; }

        public double CalculateInterestRate()
        {
            return 2.1;
        }

    }
}