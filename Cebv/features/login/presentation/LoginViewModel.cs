using System.Windows;
using Cebv.app.presentation;
using Cebv.features.login.data;
using Cebv.features.login.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.login.presentation;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string _username = "jon@cebv.com";
    [ObservableProperty] private string _password = "password";

    [ObservableProperty] private string _errorMessage = String.Empty;
    [ObservableProperty] private Visibility _errorVisibility = Visibility.Collapsed;
    [ObservableProperty] private bool _iniciandoSesion;

    public LoginViewModel()
    {
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
                var dashboard = new DashboardWindow();
                var currentWindow = Application.Current.MainWindow;
                dashboard.Show();
                currentWindow?.Close();
                break;
            
            case Error:
                ErrorVisibility = Visibility.Visible;
                ErrorMessage = result.error;
                IniciandoSesion = false;
                break;
        }
    }
}