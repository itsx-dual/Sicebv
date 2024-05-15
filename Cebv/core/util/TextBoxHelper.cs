using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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

        // Convertir el texto a mayúsculas
        textBox.Text = textBox.Text.ToUpper();

        // Restaurar la posición del cursor
        textBox.SelectionStart = cursorPosition;
        textBox.SelectionLength = 0;
    }

    /// <summary>
    /// Elimina los acentos de un texto.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string RemoveAccents(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        return regex.Replace(normalized, string.Empty);
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