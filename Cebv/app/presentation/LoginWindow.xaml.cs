using Cebv.features.login.presentation;
using Wpf.Ui.Controls;

namespace Cebv.app.presentation;

public partial class LoginWindow : FluentWindow
{
    public LoginWindow(LoginPage loginPage)
    {
        Content = loginPage;
        InitializeComponent();
    }
}