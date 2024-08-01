using System.Globalization;
using System.Windows.Data;

namespace Cebv.core.util;

public class BooleanNegation : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolean = (bool) value;
        return !boolean;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var boolean = (bool) value;
        return !boolean;
    }
}