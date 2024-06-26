using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cebv.core.util;

public class TextBoxHelper
{
    /// <summary>
    /// Eventos que se dispara cuando el texto de un TextBox cambia
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void UpperCaseText(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        DatePicker datePicker = (sender as DatePicker)!;
        
        if (datePicker?.Tag?.ToString() == "Exclude") return;

        
        // Verificar si el TextBox tiene el Tag "Exclude"
        if (textBox?.Tag?.ToString() == "Exclude")
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

    public static void PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;
        DatePicker datePicker = (sender as DatePicker)!;
        
        if (datePicker?.Tag?.ToString() == "Exclude") return;
        
        // Verificar si el TextBox tiene el Tag "Exclude"
        if (textBox?.Tag?.ToString() == "Exclude")
        {
            return;
        }
        
        if (textBox?.Tag?.ToString() == "Number")
        {
            // Patrón para permitir solo números
            string pattern = @"[^0-9]";

            if (Regex.IsMatch(e.Text, pattern))
            {
                e.Handled = true; // Marcar el evento como manejado para cancelar la entrada de texto
            }
        }else if (textBox?.Tag?.ToString() == "Letter")
        {
            // Patrón para permitir solo letras
            string pattern = @"[^A-ZÑ.]";

            if (Regex.IsMatch(e.Text.ToUpper(), pattern))
            {
                e.Handled = true;
            }

        }else if (textBox?.Tag?.ToString() == "Units")
        {
            // Patrón para permitir solo numeros y caracteres de unidad de medida
            string pattern = @"[^0-9.:]";

            if (Regex.IsMatch(e.Text, pattern))
            {
                e.Handled = true;
            }
        }else
        {
            // Patrón para permitir solo letras y la Ñ
            string pattern = @"[^A-ZÑ0-9]";
            
            if (Regex.IsMatch(e.Text.ToUpper(), pattern))
            {
                e.Handled = true;
            }
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
        DatePicker datePicker = (sender as DatePicker)!;
        
        if (datePicker?.Tag?.ToString() == "Exclude") return;
        
        // Verificar si el TextBox tiene el Tag "Exclude"
        if (textBox?.Tag?.ToString() == "Exclude")
        {
            return;
        }

        // Eliminar espacios finales e iniciales
        string trimmedText = textBox.Text.Trim();

        // Reemplazar múltiples espacios consecutivos con un solo espacio
        string singleSpaceText = Regex.Replace(trimmedText, @"\s+", " ");

        textBox.Text = singleSpaceText;
    }
}