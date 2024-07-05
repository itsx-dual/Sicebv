using System.Windows.Controls;
using PasswordBox = Wpf.Ui.Controls.PasswordBox;

namespace Cebv.features.login.presentation;

public partial class LoginPage : Page
{
    public LoginViewModel ViewModel { get; }

    public LoginPage(LoginViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    private void PasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        // Este malabar no rompe MVVM porque se está enviando los datos de la propiedad del DataContext por este medio.
        if (DataContext is null) return;
        
        ViewModel.Password = ((PasswordBox)sender).Password;
    }
}