using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Domestic:Candidate
    {
        private bool isFeeDeduction;

        public bool IsFeeDeduction { get => isFeeDeduction; set => isFeeDeduction = value; }

        public override string SpecificWork()
        {
            return (isFeeDeduction) ? "Fee deduction applied..." : "Fee deduction not applied...";
        }

        public override string ToString()
        {
            return string.Format("Candidate: Domestic, {0} {1}", base.ToString(), (isFeeDeduction) ? ", Fee deduction applied.." : ", Fee deduction not applied..");
        }
    }
}
