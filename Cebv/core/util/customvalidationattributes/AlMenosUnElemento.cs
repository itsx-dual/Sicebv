using System.ComponentModel.DataAnnotations;

namespace Cebv.core.util.customvalidationattributes;

public sealed class AlMenosUnElemento : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<dynamic> list) return new ValidationResult("El valor no es una lista");
        return list.Any() ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}