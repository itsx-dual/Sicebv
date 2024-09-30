using System.Windows;
using System.Windows.Controls;

namespace Cebv.core.util;

internal class ComboBoxFuzzySearch
{
    public void Search(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox) return;
        if (!comboBox.Items.Cast<dynamic>().Any()) return;
        
        var esValido = comboBox.Items.Cast<dynamic>().Any(x => x.ToString() == comboBox.Text);

        if (esValido) comboBox.SelectedItem = comboBox.Items.Cast<dynamic>().First(x => x.ToString() == comboBox.Text);
        else comboBox.SelectedItem = comboBox.Items.Cast<dynamic>().FirstOrDefault(x => x.ToString().Contains(comboBox.Text, StringComparison.OrdinalIgnoreCase)) ??
                                     comboBox.Items.Cast<dynamic>().FirstOrDefault(x => x.ToString().Contains("no especifica", StringComparison.OrdinalIgnoreCase)) ??
                                     comboBox.Items.Cast<dynamic>().First();
    }
}
