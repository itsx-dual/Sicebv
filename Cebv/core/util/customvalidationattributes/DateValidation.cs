using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Cebv.core.util.customvalidationattributes;

public sealed class DateValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? fecha = value?.ToString();
        if (!Regex.IsMatch(fecha, @"^((0[1-9]|[12][0-9]|3[01])|99)/((0[1-9]|1[0-2])|99)/\d{4}$")) 
           return new ValidationResult("El valor no es una fecha");
        
        return ValidationResult.Success;
    }
}