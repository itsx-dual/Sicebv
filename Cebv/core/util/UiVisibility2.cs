using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Cebv.core.util;

public class UiVisibility2 : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is UiState.Edit ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}