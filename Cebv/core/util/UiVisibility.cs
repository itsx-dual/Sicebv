using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cebv.core.util;

public class UiVisibility : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is UiState.Normal ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}