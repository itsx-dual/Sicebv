using System.Reflection;

namespace Cebv.core.util;

public class PropertyValidator
{
    public static bool ValidarPropiedades(object viewModel)
    {
        var properties = viewModel.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => 
            !p.PropertyType.IsGenericType || p.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>));
        
        foreach (var property in properties)
        {
            var value = property.GetValue(viewModel);
            
            if (value is null || (value is string str && string.IsNullOrEmpty(str)))
            {
                return false;
            }
        }
        return true;
    }
}