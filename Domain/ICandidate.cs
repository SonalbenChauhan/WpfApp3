using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICandidate:IComparable<ICandidate>
    {
        string CandidateType { get; set; }
        string CandidateName { get; set; }
        string CandidateAddress { get; set; }
        string Email { get; set; }
        string CreditCard { get; set; }
        string Passport { get; set; }
        decimal AmountPaid { get; set; }
        string ViewCreditCard { get; }
        string ViewPassport { get; }
        string ViewAmount { get; }
        CandidateDelegate MyDelegate { get; set; }

        string ArrangeStationaries();
        string RecordCreditCard();
        string SpecificWork();
        bool CheckEmail();
        bool CheckPassport();
    }
}
