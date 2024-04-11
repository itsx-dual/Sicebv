using System.ComponentModel;
using System.Windows.Controls;
using Cebv.app.data;
using Cebv.features.dashboard.presentation;
using Cebv.features.login.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.app.presentation;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableObject _currentPage;
    private LoginViewModel _loginViewModel;

    public MainWindowViewModel()
    {
        _loginViewModel = new LoginViewModel();
        CurrentPage = _loginViewModel;

        BroadCast.OnMessageTransmitted += _onMessageReceived;
    }

    private void _onMessageReceived(string message)
    {
        if (message == "inicio exitoso")
        {
            CurrentPage = new DashboardViewModel();
        }
    }
    
}