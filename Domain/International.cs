using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class International:Candidate
    {
        private bool isWorkPermit;

        public bool IsWorkPermit { get => isWorkPermit; set => isWorkPermit = value; }

        public override string SpecificWork()
        {
            return (IsWorkPermit) ? "Work permit is valid..." : "Work permit is not valid...";
        }

        public override string ToString()
        {
            return string.Format("Candidate: International, {0} {1}", base.ToString(), (isWorkPermit) ? ", Work permit is valid.." : ", Work permit is not valid..");
        }
    }
}
