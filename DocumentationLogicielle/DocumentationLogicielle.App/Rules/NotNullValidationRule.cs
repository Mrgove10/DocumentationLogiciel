using System.Globalization;
using System.Windows.Controls;

namespace DocumentationLogicielle.App.Rules
{
    /// <summary>
    /// Checks if a value is null or empty
    /// if a value is empty, a value is null then ir returns false
    /// else the value returned is false
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
