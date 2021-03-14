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
    class EmailRule : ValidationRule
    {
        public EmailRule()
        {
        }


        public override ValidationResult Validate
           (object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match((string)value);
            if (!match.Success) {
                return new ValidationResult(false,
                    string.Format("Invali email format"));
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
