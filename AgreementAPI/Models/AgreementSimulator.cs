using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgreementAPI.Models
{
    public class AgreementSimulator
    {
        public AgreementSimulator(Agreement currentAgreement, string newRateCode)
        {
            Agreement simAgreement = currentAgreement.ShallowCopy();
            simAgreement.BaseCodeRate = newRateCode;
            double newrate = simAgreement.CalculateInterestRate();
            double oldrate = currentAgreement.CalculateInterestRate();

        }
    }
}