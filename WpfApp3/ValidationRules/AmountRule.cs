using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3
{
    class AmountRule : ValidationRule
    {
        int minVal;
        int maxVal;
        public int MinVal { get => minVal; set => minVal = value; }
        public int MaxVal { get => maxVal; set => maxVal = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int appointmentRange = 0;
            if (!int.TryParse(value.ToString(), out appointmentRange))
            {
                return new ValidationResult(false, "Wrong input");
            }

            if (appointmentRange < MinVal || appointmentRange > maxVal)
            {
                return new ValidationResult(false, "Amount range should be between 0 to 300");
            }

            return ValidationResult.ValidResult;
        }
    }
}
