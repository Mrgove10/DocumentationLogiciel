using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DocumentationLogicielle.App.Rules
{
    public class NotNullValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Please enter a value");
        }
    }
}
