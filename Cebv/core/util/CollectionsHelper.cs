using System.Collections.ObjectModel;

namespace Cebv.core.util;

public static class CollectionsHelper
{
    /// <summary>
    /// Asegura que un objeto exista en una colección. Si no existe, lo crea y lo agrega a la colección.
    /// Usa <paramref name="obj"/> para modificar, el objeto y la colección original se pasan por referencia.
    /// Usa un diccionario para asignar valores a las propiedades del objeto.
    /// </summary>
    /// <typeparam name="T">El tipo del objeto.</typeparam>
    /// <param name="obj">El objeto que se asegura que exista.</param>
    /// <param name="collection">La colección a la que se agrega el objeto si no existe.</param>
    /// <param name="parameters">Un diccionario de nombres de propiedades y valores para asignar al objeto.</param>
    public static void EnsureObjectExists<T>(
        ref T? obj,
        ObservableCollection<T> collection,
        Dictionary<string, object> parameters) where T : class, new()
    {
        if (obj is not null) return;

        obj = new T();

        foreach (var (key, value) in parameters)
        {
            var prop = obj.GetType().GetProperty(key);
            prop?.SetValue(obj, value);
        }

        collection.Add(obj);
    }
}