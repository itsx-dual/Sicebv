using System.Windows;
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
            Console.WriteLine("Inicia de sesion exitoso");
        }
        else
        {
            Console.WriteLine("Error al iniciar sesión");
        }
    }
}