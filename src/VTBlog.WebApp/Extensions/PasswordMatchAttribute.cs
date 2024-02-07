using System.ComponentModel.DataAnnotations;

namespace VTBlog.WebApp.Extensions
{
    public class PasswordMatchAttribute(string otherProperty) : ValidationAttribute("{0} and {1} must match")
    {
        private readonly string _otherProperty = otherProperty;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(otherProperty)
                .GetValue(validationContext.ObjectInstance, null);
            if (value == null || value.ToString() == otherPropertyValue?.ToString())
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
