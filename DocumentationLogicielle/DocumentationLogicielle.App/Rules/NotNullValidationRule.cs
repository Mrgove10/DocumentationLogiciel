using System.Globalization;
using System.Windows.Controls;

namespace DocumentationLogicielle.App.Rules
{
    /// <summary>
    /// Validation rule to check empty value
    /// </summary>
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
