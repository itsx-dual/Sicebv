using Cebv.features.dashboard.presentation;
using Wpf.Ui.Controls;

namespace Cebv.app.presentation;

public partial class DashboardWindow : FluentWindow
{
    public DashboardWindow(DashboardPage dashboardPage)
    {
        Content = dashboardPage;
        InitializeComponent();
    }
}