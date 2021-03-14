using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3
{
    class AppoitnmentRule : ValidationRule
    {
        int minVal;
        int maxVal;
        public int MinVal { get => minVal; set => minVal = value; }
        public int MaxVal { get => maxVal; set => maxVal = value; }

        public override ValidationResult Validate(object appointmentRanngeObj, CultureInfo cultureInfo)
        {
            int appointmentRange = 0;
            if (!int.TryParse(appointmentRanngeObj.ToString(), out appointmentRange))
            {
                return new ValidationResult(false, "Wrong input");
            }

            if (appointmentRange < MinVal || appointmentRange > maxVal)
            {
                return new ValidationResult(false, "Appointment range should be between 1 to 12");
            }

            return ValidationResult.ValidResult;
        }
    }
}
