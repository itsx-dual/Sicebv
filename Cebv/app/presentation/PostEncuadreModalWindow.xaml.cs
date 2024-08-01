using System.Windows;

namespace Cebv.app.presentation;

public partial class PostEncuadreModalWindow
{
    public PostEncuadreModalWindow()
    {
        InitializeComponent();
    }

    private void SubmitButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;  // Return true on Submit
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false; // Return false on Cancelbn
        Close();
    }

}