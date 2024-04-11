using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Cebv.app.data;
using Cebv.app.presentation;
using Cebv.features.dashboard.presentation;
using Cebv.features.login.data;
using Cebv.features.login.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.login.presentation;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string _username;

    [ObservableProperty] private string _password;

    //[ObservableProperty] private Visibility _visibilityErrorMessage;

    [ObservableProperty] private string _errorMessage;


    [RelayCommand]
    private async void IniciarSesion()
    {
        var result = await LoginNetwork.GetTokenRequest(Username, Password);

        if (result is TokenWrapped)
        {
            BroadCast.Message("inicio exitoso");
            var dashboard = new DashboardWindow();
            var currentWindow = Application.Current.MainWindow;
            dashboard.Show();
            currentWindow.Close();
        }
    }
}