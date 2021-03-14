using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Domain
{
    public enum CandidateType
    {
        Domestic = 1,
        International,
        LicenseRenew
    }
    public delegate string CandidateDelegate();
    public abstract class Candidate:ICandidate
    {
        private string candidateType;
        private string candidateName;
        private string candidateAddress;
        private string email;
        private string creditCard;
        private string passport;
        private decimal amountPaid;
        private CandidateDelegate myDelegate;

        public string CandidateType { get => candidateType; set => candidateType = value; }
        public string CandidateName { get => candidateName; set => candidateName = value; }
        public string CandidateAddress { get => candidateAddress; set => candidateAddress = value; }
        public string Email { get => email; set => email = value; }
        public string CreditCard { get => creditCard; set => creditCard = value; }
        public string Passport { get => passport; set => passport = value; }
        public decimal AmountPaid { get => amountPaid; set => amountPaid = value; }
        public string ViewCreditCard { get => ConcealedCreditCard(); }
        public string ViewPassport { get => ConcealedPassport(); }
        public string ViewAmount { get => amountPaid.ToString("c"); }
        public string ViewTasks { get => myDelegate(); }

        [XmlIgnore()]
        public CandidateDelegate MyDelegate { get => myDelegate; set => myDelegate = value; }

        public bool CheckPassport()
        {
            bool result = false;
            if (Passport != null && Passport != string.Empty && passport.Length==8)
            {
                char[] passArray = Passport.ToCharArray();
                if (char.IsLetter(passArray[0]) && char.IsLetter(passArray[1]))
                {
                    for (int i = 2; i < passArray.Length; i++)
                    {
                        if (char.IsDigit(passArray[i]))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public Candidate()
        {
            myDelegate = ArrangeStationaries;
            myDelegate += RecordCreditCard;
            myDelegate += SpecificWork;
        }

        public bool CheckEmail()
        {
            try
            {
                if (email != null && email != string.Empty)
                {
                    MailAddress m = new MailAddress(email);
                }

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private string ConcealedPassport()
        {
            char[] passArray = Passport.ToCharArray();
            for (int i = 2; i < 5; i++)
            {
                passArray[i] = 'X';
            }
            return new string(passArray);
        }

        private string ConcealedCreditCard()
        {
            char[] creditCardArray = creditCard.ToCharArray();
            char[] newCreditCardArray = new char[19];

            for (int i = 0; i < 19; i++)
            {
                if (i >= 0 && i < 4)
                {
                    newCreditCardArray[i] = creditCardArray[i];
                }
                else if (i > 14 && i < 19)
                {
                    newCreditCardArray[i] = creditCardArray[i - 3];
                }

                else if (i == 4 || i == 9 || i == 14)
                {
                    newCreditCardArray[i] = ' ';
                }
                else
                {
                    newCreditCardArray[i] = 'X';
                }
            }
            return new string(newCreditCardArray);
        }

        public bool CheckCreditCard()
        {
            bool result = false;
            if (creditCard != null && creditCard != string.Empty && creditCard.Length == 16)
            {
                char[] creditCardArray = creditCard.ToCharArray();

                result = true;
                for (int i = 0; i < creditCardArray.Length; i++)
                {
                    if (!char.IsDigit(creditCardArray[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public string ArrangeStationaries()
        {
            return "Arranged stationaries...";
        }

        public string RecordCreditCard()
        {
            return "Recorded credit card number...";
        }

        public override string ToString()
        {
            return string.Format("Candidate Name : {0}, Candidate Address: {1}, Credit Card Number: {2}, Amount paid : {3}", candidateName, candidateAddress, ViewCreditCard, amountPaid);
        }

        public abstract string SpecificWork();

        public int CompareTo(ICandidate other)
        {
            return amountPaid.CompareTo(other.AmountPaid);
        }
    }
}
