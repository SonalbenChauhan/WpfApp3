using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3
{
    class CreditcardRule : ValidationRule
    {
        public CreditcardRule()
        {
        }


        public override ValidationResult Validate
           (object value, CultureInfo cultureInfo)
        {
            bool result = false;
            string creditCard = value.ToString();
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
            if (result)
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false,
                  string.Format("Invalid credit card format..(16 digits should be there)"));
        }
    }
}
