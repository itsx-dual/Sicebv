using Cebv.app.data;
using Cebv.features.dashboard.presentation;
using Cebv.features.login.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.app.presentation;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableObject _currentPage;
    private LoginViewModel LoginViewModel { get; }
    private DashboardViewModel DashboardViewModel { get; }

    public MainWindowViewModel(LoginViewModel loginViewModel, DashboardViewModel dashboardViewModel)
    {
        LoginViewModel = loginViewModel;
        DashboardViewModel = dashboardViewModel;
        
        CurrentPage = LoginViewModel;

        BroadCast.OnMessageTransmitted += _onMessageReceived;
    }

    private void _onMessageReceived(string message)
    {
        if (message == "inicio exitoso")
        {
            CurrentPage = DashboardViewModel;
        }
    }
}