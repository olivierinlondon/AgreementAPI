using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AgreementAPI.Models
{
    public class AgreementSimulator
    {
        double _NewInterestRate = 0;
        double _OldInterestRate = 0;
        Agreement _CurrentAgreement;
        Agreement _SimAgreement;

        public Agreement CurrentAgreement
        {
            get { return _CurrentAgreement; }
        }

        public double NewInterestRate
        {
            get { return _NewInterestRate; }
        }

        public double OldInterestRate
        {
            get { return _OldInterestRate; }
        }

        public double DifferenceInRate
        {
            get { return OldInterestRate - NewInterestRate; }
        }


        public AgreementSimulator(Agreement currentAgreement, string newRateCode)
        {
            _SimAgreement = currentAgreement.ShallowCopy();
            _CurrentAgreement = currentAgreement;
            _SimAgreement.BaseCodeRate = newRateCode;
            Parallel.Invoke(
            () => _NewInterestRate = _SimAgreement.CalculateInterestRate(),
            () => _OldInterestRate = _CurrentAgreement.CalculateInterestRate()
            );

        }
    }
}