using System.Globalization;

namespace Cebv.core.util;

public class ConvertFormat
{
    private DateTime? _fecha;
    private string _fechaAprox = string.Empty;

    public ConvertFormat(string fechaAprox)
    {
        _fechaAprox = fechaAprox;
        ConvertDate();
    }

    /// <summary>
    /// Método para convertir la fecha aproximada a una fecha válida.
    /// - Si la fecha aproximada es válida, se convierte a fecha en formato DatePicker.
    /// </summary>
    private void ConvertDate()
    {
        if (DateTime.TryParseExact(_fechaAprox, "dd/MM/yyyy", null, DateTimeStyles.None, out var fechaConvertida))
        {
            _fecha = fechaConvertida;
        }
        
        IrregularCase();
        
        if (DateTime.TryParseExact(_fechaAprox, "dd/MM/yyyy", null, DateTimeStyles.None, out fechaConvertida))
        {
            _fecha = fechaConvertida;
        }
        else
        {
            _fecha = null;
        }
    }

    /// <summary>
    /// Método para convertir fechas 9999 a una fecha válida.
    ///     - Si la fecha aproximada es "99/99/9999", se convierte a "31/12/año actual".
    ///     - Si la fecha aproximada es "99/99/xxxx", se convierte a "31/12/xxxx".
    ///     - Si la fecha aproximada es "99/xx/xxxx", se convierte a "ultimo dia/xx/xxxx".
    ///     - Si la fecha aproximada es "xx/99/xxxx", se convierte a "xx/12/xxxx".
    ///     - Si la fecha aproximada es "xx/xx/9999", se convierte a "xx/xx/año actual".
    /// </summary>
    private void IrregularCase()
    {
        if (_fechaAprox == "99/99/9999")
        {
            _fechaAprox = new DateTime(DateTime.Now.Year, 12, 31).ToString("dd/MM/yyyy");
        }
        else if (_fechaAprox.StartsWith("99/99/"))
        {
            int year = int.Parse(_fechaAprox.Substring(6, 4));
            _fechaAprox = new DateTime(year, 12, 31).ToString("dd/MM/yyyy");
        }
        else if (_fechaAprox.StartsWith("99/"))
        {
            int year = int.Parse(_fechaAprox.Substring(6, 4));
            int month = int.Parse(_fechaAprox.Substring(3, 2));
            int day = DateTime.DaysInMonth(year, month);
            _fechaAprox = new DateTime(year, month, day).ToString("dd/MM/yyyy");
        }
        else if (_fechaAprox.Contains("/99/"))
        {
            int year = int.Parse(_fechaAprox.Substring(6, 4));
            int day = int.Parse(_fechaAprox.Substring(0, 2));
            _fechaAprox = new DateTime(year, 12, day).ToString("dd/MM/yyyy");
        }
        else if (_fechaAprox.EndsWith("9999"))
        {
            int month = int.Parse(_fechaAprox.Substring(3, 2));
            int day = int.Parse(_fechaAprox.Substring(0, 2));
            _fechaAprox = new DateTime(DateTime.Now.Year, month, day).ToString("dd/MM/yyyy");
        }
    }

    public DateTime? GetDate()
    {
        return _fecha;
    }
}