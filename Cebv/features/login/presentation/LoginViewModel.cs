using System.Windows;
using Cebv.app.presentation;
using Cebv.features.login.data;
using Cebv.features.login.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.login.presentation;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string _username = "test@example.com";

    [ObservableProperty] private string _password = "password";

    partial void OnUsernameChanged(string value) =>
        Console.WriteLine(value);
    
    partial void OnPasswordChanged(string value) =>
        Console.WriteLine(value);

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

        if (result is TokenWrapped)
        {
            var currentWindow = Application.Current.MainWindow;
            DashboardWindow.Show();
            currentWindow?.Close();
        }
        else if (result is Error)
        {
            ErrorVisibility = Visibility.Visible;
            ErrorMessage = result.error;
            IniciandoSesion = false;
        }
    }
}