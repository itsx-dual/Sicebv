using System.Collections.ObjectModel;

namespace Cebv.core.util;

public static class ExtensionHelper
{
    public static bool Update<T>(this ObservableCollection<T> collection, T old, T newObj)
    {
        var found = collection.ToList().FindIndex(x => x != null && x.Equals(old));
        if (found < 0) return false;
        collection[found] = newObj;
        return true;
    }
}