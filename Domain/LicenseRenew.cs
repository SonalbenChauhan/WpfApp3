using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LicenseRenew:Candidate
    {
        private bool isLicenseExpired;

        public bool IsLicenseExpired { get => isLicenseExpired; set => isLicenseExpired = value; }

        public override string SpecificWork()
        {
            return (isLicenseExpired) ? "License is expired..." : "License is not expired...";
        }

        public override string ToString()
        {
            return string.Format("Candidate: License Renewal, {0} {1}", base.ToString(), (isLicenseExpired) ? ", License is expired.." : ", License is not expired..");
        }
    }
}
