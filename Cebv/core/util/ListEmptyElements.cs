using System.Collections.ObjectModel;
using System.Reflection;

namespace Cebv.core.util;

public class ListEmptyElements
{
    public static List<string> EnlistarElementosVacios(object objeto)
    {
        PropertyInfo[] properties = objeto.GetType().GetProperties();
        var emptyElements = new List<string>();

        foreach (var property in properties)
        {
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() 
                == typeof(ObservableCollection<>)) continue;
            
            var value = property.GetValue(objeto);
            if (value is null || (value is string str && string.IsNullOrEmpty(str)))
            {
                emptyElements.Add(property.Name);
            }
        }

        return emptyElements;
    }
}