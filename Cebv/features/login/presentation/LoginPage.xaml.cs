using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PasswordBox = Wpf.Ui.Controls.PasswordBox;

namespace Cebv.features.login.presentation;

public partial class LoginPage : Page
{
    public LoginPage() => InitializeComponent();
    
    private void PasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox) return;
        if (DataContext is not LoginViewModel viewModel) return;
        viewModel.Password = passwordBox.Password;
    }

    private void PasswordBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not PasswordBox passwordBox) return;
        if (DataContext is not LoginViewModel viewModel) return;
        if (e.Key == Key.Enter && viewModel.IniciarSesionCommand.CanExecute(null)) viewModel.IniciarSesionCommand.Execute(null);
    }
}