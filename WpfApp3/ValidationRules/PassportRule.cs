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
    class PassportRule : ValidationRule
    {
        public PassportRule()
        {
        }


        public override ValidationResult Validate
           (object value, CultureInfo cultureInfo)
        {
            bool result = false;
            string passport =value.ToString();
            if (passport != null && passport != string.Empty && passport.Length == 8)
            {
                char[] passArray = passport.ToCharArray();
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
            if(result)
            return ValidationResult.ValidResult; 
            else
                return new ValidationResult(false,
                  string.Format("Invalid passport format..(First 2 chars should be alphabets and next 6 should be digits)"));
        }
    }
}
