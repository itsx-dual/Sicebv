using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Cebv.core.util;

public class TextBoxHelper
{
    /// <summary>
    /// Eventos que se dispara cuando el texto de un TextBox cambia
    /// para filtrar caracteres acentuados y convertir el texto a mayúsculas.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void UpperCaseText(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (sender as TextBox)!;

        // Guardar la posición actual del cursor
        int cursorPosition = textBox.SelectionStart;

        // Filtrar caracteres acentuados
        textBox.Text = RemoveAccents(textBox.Text);

        // Restaurar la posición del cursor
        textBox.SelectionStart = cursorPosition;
        textBox.SelectionLength = 0;
    }

    /// <summary>
    /// Elimina los acentos de un texto   OJO: EXCLUYENDO LA Ñ.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string RemoveAccents(string text)
    {
        // Patrón de reemplazo para caracteres acentuados
        string pattern = "[áéíóúÁÉÍÓÚäëïöüÄËÏÖÜ]";
        
        string result = Regex.Replace(text, pattern, m => 
        {
            switch (m.Value)
            {
                case "á": return "a";
                case "é": return "e";
                case "í": return "i";
                case "ó": return "o";
                case "ú": return "u";
                case "Á": return "A";
                case "É": return "E";
                case "Í": return "I";
                case "Ó": return "O";
                case "Ú": return "U";
                case "ä": return "a";
                case "ë": return "e";
                case "ï": return "i";
                case "ö": return "o";
                case "ü": return "u";
                case "Ä": return "A";
                case "Ë": return "E";
                case "Ï": return "I";
                case "Ö": return "O";
                case "Ü": return "U";
                default: return m.Value;
            }
        });

        return result;
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

        // Eliminar espacios finales e iniciales
        string trimmedText = textBox.Text.Trim();

        // Reemplazar múltiples espacios consecutivos con un solo espacio
        string singleSpaceText = Regex.Replace(trimmedText, @"\s+", " ");

        textBox.Text = singleSpaceText;
    }
}