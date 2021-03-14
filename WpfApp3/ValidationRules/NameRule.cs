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
    public class NameRule : ValidationRule
    {
        public NameRule()
        {
        }


        public override ValidationResult Validate
           (object value, CultureInfo cultureInfo)
        {
            Regex r = new Regex(@"[~`!@#$%^&*()-+=|\{}':;.,<>/?]");
            if (r.IsMatch((string)value))
            {
                return new ValidationResult(false,
                    string.Format("Special Characters are not allowed"));
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch((string)value, "^[a-zA-Z ]"))
            {
                return new ValidationResult(false,
                  string.Format("Only Alphabets are allowed"));
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
