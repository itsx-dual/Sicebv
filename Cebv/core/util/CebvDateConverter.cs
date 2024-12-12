using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Cebv.core.util;

public class CebvDateConverter : IValueConverter
{
    private DateTime? _fecha;
    
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string fechaAprox && !string.IsNullOrEmpty(fechaAprox))
        {
            fechaAprox = DateConvert(fechaAprox);
            
            if (DateTime.TryParseExact(fechaAprox, "dd/MM/yyyy", null, DateTimeStyles.None, 
                    out var fechaConvertida)) return _fecha = fechaConvertida;
        }
        
        return null;
    }
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    { 
        throw new NotImplementedException();
    }

    private string DateConvert(string fechaAprox)
    {
        if (!Regex.IsMatch(fechaAprox, @"^((0[1-9]|[12][0-9]|3[01])|99)/((0[1-9]|1[0-2])|99)/\d{4}$")) 
            return fechaAprox = DateTime.Now.ToString("dd/MM/yyyy");
        
        int year = DateTime.Now.Year;
        int month = DateTime.MaxValue.Month;
        int day = DateTime.MaxValue.Day;

        if (fechaAprox.StartsWith("99"))
        {
            if (fechaAprox.Contains("/99/"))
            {
                if (fechaAprox.EndsWith("9999")) fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
                
                year = int.Parse(fechaAprox.Substring(6, 4));
                fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
            }
            if (fechaAprox.EndsWith("9999"))
            {
                month = int.Parse(fechaAprox.Substring(3, 2));
                day = DateTime.DaysInMonth(year, month);
                fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
            }
            
            year = int.Parse(fechaAprox.Substring(6, 4)); 
            month = int.Parse(fechaAprox.Substring(3, 2));
            day = DateTime.DaysInMonth(year, month);
            
            fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
        }
        if (fechaAprox.Contains("/99/"))
        {
            if (fechaAprox.EndsWith("9999"))
            {
                day = int.Parse(fechaAprox.Substring(0, 2));
                fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
            }
            
            year = int.Parse(fechaAprox.Substring(6, 4));
            day = int.Parse(fechaAprox.Substring(0, 2));
            fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");   
        }
        if (fechaAprox.EndsWith("9999"))
        {
            month = int.Parse(fechaAprox.Substring(3, 2));
            day = int.Parse(fechaAprox.Substring(0, 2));
            fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
        }
        
        return fechaAprox;
    }
}