using System.Windows;
using System.Windows.Controls;
using PasswordBox = Wpf.Ui.Controls.PasswordBox;

namespace Cebv.features.login.presentation;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }
    
    private void PasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        // Este malabar no rompe MVVM ya que se esta enviando los datos de la propiedad del DataContext por este medio.
        if (this.DataContext != null)
        {
            ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
        }
    }
}