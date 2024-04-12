using System.ComponentModel;
using System.Windows.Controls;
using Cebv.app.data;
using Cebv.core.services;
using Cebv.features.dashboard.presentation;
using Cebv.features.login.presentation;
using Cebv.features.reportante.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.app.presentation;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableObject _currentPage;
    private LoginViewModel _loginViewModel;
    private ReportanteFeedViewModel _reportanteFeedViewModel;
    public event PropertyChangedEventHandler PropertyChanged;

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