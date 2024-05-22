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
    [ObservableProperty] private Visibility _errorVisibility;
    [ObservableProperty] private bool _iniciandoSesion;

    public LoginViewModel()
    {
        ErrorMessage = string.Empty;
        ErrorVisibility = Visibility.Collapsed;
        IniciandoSesion = false;
    }
    
    [RelayCommand]
    private async void IniciarSesion()
    {
        if (IniciandoSesion) return;
        
        IniciandoSesion = true;
        ErrorVisibility = Visibility.Collapsed;
        var result = await LoginNetwork.POST(Username, Password);
        
        if (result is TokenWrapped)
        {
            var dashboard = new DashboardWindow();
            var currentWindow = Application.Current.MainWindow;
            dashboard.Show();
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