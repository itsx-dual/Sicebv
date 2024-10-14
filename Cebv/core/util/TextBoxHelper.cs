using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Cebv.core.util.snackbar;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using TextBox = System.Windows.Controls.TextBox;

namespace Cebv.core.util;

public class TextBoxHelper
{
    private static ISnackbarService _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
    /// <summary>
    /// Método auxiliar para verificar si el TextBox está dentro de un DatePicker.
    /// </summary>
    /// <param name="depObj"></param>
    /// <returns></returns>   
    private static bool IsDatePicker(DependencyObject depObj)
    {
        while (depObj != null)
        {
            if (depObj is DatePicker)
            {
                return true;
            }
            depObj = VisualTreeHelper.GetParent(depObj);
        }
        return false;
    }

    /// <summary>
    /// Maneja el evento SelectedDateChanged para restringir la selección de fechas futuras.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void DatePickerSelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        DatePicker datePicker = sender as DatePicker;
        // Verificar si el DatePicker tiene el Tag "Exclude"
        if (datePicker.Tag?.ToString() == "Exclude")
        {
            return;
        }

        if (datePicker != null)
        {
            datePicker.DisplayDateEnd = DateTime.Now;
            datePicker.SelectedDateChanged -= DatePickerSelectedDateChanged;

            if (datePicker.SelectedDate.HasValue && datePicker.SelectedDate.Value > DateTime.Now)
            {
                datePicker.SelectedDate = DateTime.Now;
            }
        }
    }

    /// <summary>
    /// Eventos que se dispara cuando el texto de un TextBox cambia
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void UpperCaseText(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        
        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude" || textBox.Tag?.ToString() == "Mail")
        {
            return;
        }
        
        // Convertir el texto a mayúsculas
        if (textBox != null)
        {
            int caretIndex = textBox.CaretIndex;
            textBox.Text = textBox.Text.ToUpper();
            textBox.CaretIndex = caretIndex;
        }
        
        // Guardar la posición actual del cursor
        int cursorPosition = textBox.SelectionStart;

        // Restaurar la posición del cursor
        textBox.SelectionStart = cursorPosition;
        textBox.SelectionLength = 0;
    }

    /// <summary>
    /// Evento que se dispara cuando se presiona una tecla en un TextBox
    /// y permite solo ciertos caracteres según el Tag del TextBox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        string pattern;

        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude")
        {
            return;
        }

        switch (textBox?.Tag?.ToString())
        {
            case "Number":
                // Patrón para permitir solo números
                pattern = @"[^0-9]";
          
                // No permitir números negativos
                if (textBox.Text.Contains("-") || e.Text == "-")
                {
                    e.Handled = true;
                    return;
                }
                break;
            case "Letter":
                // Patrón para permitir solo letras
                pattern = @"[^A-ZÑ.]";
                break;
            case "Units":
                // Patrón para permitir solo numeros y caracteres de unidad de medida
                pattern = @"[^0-9.,]";
                // Permitir solo un punto decimal o coma
                if (e.Text == "." || e.Text == ",")
                {
                    if (textBox.Text.Contains(".") || textBox.Text.Contains(","))
                    {
                        e.Handled = true;
                    }
                    return;
                }
                // No permitir números negativos
                if (textBox.Text.Contains("-") || e.Text == "-")
                {
                    e.Handled = true;
                    return;
                }
                break;
            case "Time":
                //Solo números y caracteres de tiempo
                pattern = @"[^\d{2}:\d{2}$]";
                textBox.MaxLength = 5;
                break;
            case "Date":
                //Solo números y caracteres de fecha
                pattern = @"[^\d{2}/\d{2}/\d{4}$]";
                textBox.MaxLength = 10;
                break;
            case "Mail":
                // Patrón para permitir letras, números y caracteres especiales de correo electrónico
                pattern = @"[^a-zA-ZñÑ0-9@._-]";
                break;
            default:
                // Patrón para permitir letras y la Ñ
                pattern = @"[^A-ZÑ0-9,]";
                break;
        }

        if (Regex.IsMatch(e.Text.ToUpper(), pattern))
        {
            e.Handled = true;
        }
    }
    
    /// <summary>
    /// Evento que se dispara cuando el texto de un TextBox cambia
    /// y permite completar automáticamente el texto según el Tag del TextBox.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void AutoCompleted(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        
        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude")
        {
            return;
        }
        
        if (textBox?.Tag?.ToString() == "Date")
        {
            if ((textBox.Text.Length == 2 || textBox.Text.Length == 5) && !textBox.Text.EndsWith("/"))
            {
                textBox.Text += "/";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }else if (textBox?.Tag?.ToString() == "Time")
        {
            if (textBox.Text.Length == 2 && !textBox.Text.EndsWith(":"))
            {
                textBox.Text += ":";
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
    
    public static void ValidateCoherentText(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;

        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude")
        {
            return;
        }
        
        string inputText = textBox.Text.ToLower();

        // Criterio 1: Validar que la longitud de cada palabra no sea extremadamente corta o larga
        string[] words = inputText.Split(' ');
        foreach (var word in words)
        {
            if (word.Length < 1 || word.Length > 15)
            {
                _snackbarService.Show(
                    "Texto incoherente",
                    $"La palabra \"{word}\" parece ser inválida por su longitud.",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));

                textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
                return;
            }
        }

        // Criterio 2: Validar la frecuencia de letras repetidas en una palabra
        foreach (var word in words)
        {
            var letterGroups = word.GroupBy(c => c).Where(g => g.Count() > 3); // Letras repetidas más de 3 veces
            if (letterGroups.Any())
            {
                _snackbarService.Show(
                    "Texto incoherente",
                    $"La palabra \"{word}\" tiene letras repetidas de forma inusual.",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));

                textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
                return;
            }
        }
        
        textBox.BorderBrush = SystemColors.ControlDarkBrush;
    }
    
    public static void ValidateCoherentName(object sender, RoutedEventArgs e) 
    { 
        TextBox textBox = (sender as TextBox)!;

        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude") 
        { 
            return; 
        }
        
        string inputText = textBox.Text.ToLower();
        
        // Criterio 1: Validar longitud de cada nombre (parte del nombre)
        string[] names = inputText.Split(new char[] { ' ', '-', '\'' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var name in names)
        {
            // Longitud típica de un nombre propio
            if (name.Length < 2 || name.Length > 20)
            {
                _snackbarService.Show(
                    "Nombre incoherente",
                    $"El nombre \"{name}\" parece inválido por su longitud.",
                    ControlAppearance.Dark,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));

                textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
                return;
            }
        }
        
        // Criterio 2: Validar frecuencia de letras repetidas en un nombre
        foreach (var name in names)
        {
            var letterGroups = name.GroupBy(c => c).Where(g => g.Count() > 2); // Letras repetidas más de 2 veces
            if (letterGroups.Any())
            {
                _snackbarService.Show(
                    "Nombre incoherente",
                    $"El nombre \"{name}\" tiene letras repetidas de forma inusual.",
                    ControlAppearance.Dark,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));

                textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
                return;
            }
        }
        
        // Criterio 3: Validar solo caracteres permitidos (letras, espacios, guiones, apóstrofes)
        if (!Regex.IsMatch(inputText, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ '-]+$"))
        {
            _snackbarService.Show(
                "Nombre inválido",
                "El nombre contiene caracteres no permitidos. Solo se permiten letras, guiones y apóstrofes.",
                ControlAppearance.Dark,
                new SymbolIcon(SymbolRegular.Warning20),
                new TimeSpan(0, 0, 5));

            textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
            return;
        }
        textBox.BorderBrush = SystemColors.ControlDarkBrush; 
    }
    
    public static void ValidateUserName(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;

        string userNamePattern = @"^[a-zA-Z0-9](?!.*[_.]{2})[a-zA-Z0-9._]+[a-zA-Z0-9]$";

        if (!Regex.IsMatch(textBox.Text, userNamePattern) || textBox.Text.Length < 3 || textBox.Text.Length > 30)
        {
            _snackbarService.Show(
                "Nombre de usuario no válido",
                "El nombre de usuario debe tener entre 3 y 30 caracteres, y solo puede incluir letras, números, guiones bajos, y puntos. No puede comenzar ni terminar con un punto o guion bajo.",
                ControlAppearance.Dark,
                new SymbolIcon(SymbolRegular.Warning20),
                new TimeSpan(0, 0, 5));

            e.Handled = true;
            textBox.BorderBrush = new SolidColorBrush(Colors.Yellow);
        }
        else
        {
            textBox.BorderBrush = SystemColors.ControlDarkBrush;
        }
    }
    
    /// <summary>
    /// Evento que se dispara cuando un TextBox pierde el foco,
    /// elimina los espacios finales e iniciales y
    /// reemplaza múltiples espacios consecutivos con un solo espacio.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void TrimmedText(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        
        // Verificar si el TextBox tiene el Tag "Exclude" o si está dentro de un DatePicker
        if (IsDatePicker(textBox) || textBox.Tag?.ToString() == "Exclude")
        {
            return;
        }

        if (textBox?.Tag?.ToString() == "Time")
        {
            if (!Regex.IsMatch(textBox.Text, @"^([0-1][0-9]|2[0-3]):([0-5][0-9])$"))
            {
                _snackbarService.Show(
                    "Formato no valido",
                    "Por favor ingrese formato valido: \"HH:MM\" \nEjemplo: \"23:59\"",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));
                
                e.Handled = true;
                textBox.BorderBrush = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                textBox.BorderBrush = SystemColors.ControlDarkBrush;
            }
        }else if (textBox?.Tag?.ToString() == "Date")
        {
            if (!Regex.IsMatch(textBox.Text, @"^((0[1-9]|[12][0-9]|3[01])|99)/((0[1-9]|1[0-2])|99)/\d{4}$"))
            {
                _snackbarService.Show(
                    "Formato no valido",
                    "Por favor ingrese formato valido: \"DD/MM/AAAA\" \nEjemplo: \"31/12/2021\"",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));
                
                e.Handled = true;
                textBox.BorderBrush = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                textBox.BorderBrush = SystemColors.ControlDarkBrush;
            }
        }else if (textBox?.Tag?.ToString() == "Mail")
        {
            if (!Regex.IsMatch(textBox.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                _snackbarService.Show(
                    "Formato no valido",
                    "Por favor ingrese un correo electrónico valido",
                    ControlAppearance.Caution,
                    new SymbolIcon(SymbolRegular.Warning20),
                    new TimeSpan(0, 0, 5));
                
                e.Handled = true;
                textBox.BorderBrush = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                textBox.BorderBrush = SystemColors.ControlDarkBrush;
            }
        }
        
        ValidateCoherentText(sender, e);
        ValidateCoherentName(sender, e);

        // Eliminar espacios finales e iniciales
        string trimmedText = textBox.Text.Trim();

        // Reemplazar múltiples espacios consecutivos con un solo espacio
        string singleSpaceText = Regex.Replace(trimmedText, @"\s+", " ");

        textBox.Text = singleSpaceText;
    }
}