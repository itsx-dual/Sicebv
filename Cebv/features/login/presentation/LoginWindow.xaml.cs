using System.Windows;
using Wpf.Ui.Controls;

namespace Cebv.features.login.presentation;

public partial class LoginWindow : FluentWindow
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void PassRawPassword(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        {
            ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
        }
    }
}