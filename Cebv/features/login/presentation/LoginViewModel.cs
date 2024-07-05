using System.Windows;
using Cebv.app.presentation;
using Cebv.features.login.data;
using Cebv.features.login.domain;

namespace Cebv.features.login.presentation;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string _username = "test@example.com";

    [ObservableProperty] private string _password = "password";

    [ObservableProperty] private string _errorMessage = String.Empty;
    [ObservableProperty] private Visibility _errorVisibility = Visibility.Collapsed;
    [ObservableProperty] private bool _iniciandoSesion;

    private DashboardWindow DashboardWindow { get; }

    public LoginViewModel(DashboardWindow dashboardWindow)
    {
        DashboardWindow = dashboardWindow;
        ErrorMessage = string.Empty;
        ErrorVisibility = Visibility.Collapsed;
        IniciandoSesion = false;
    }

    [RelayCommand]
    private async Task IniciarSesion()
    {
        if (IniciandoSesion) return;

        IniciandoSesion = true;
        ErrorVisibility = Visibility.Collapsed;
        var result = await LoginNetwork.Post(Username, Password);

        switch (result)
        {
            case TokenWrapped:
            {
                var currentWindow = Application.Current.MainWindow;
                DashboardWindow.Show();
                currentWindow?.Close();
                break;
            }
            case Error:
                ErrorVisibility = Visibility.Visible;
                ErrorMessage = result.error;
                IniciandoSesion = false;
                break;
        }
    }
}