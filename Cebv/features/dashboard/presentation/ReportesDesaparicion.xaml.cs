using System.Windows.Controls;
using System.Windows.Input;

namespace Cebv.features.dashboard.presentation;

public partial class ReportesDesaparicion : Page
{
    public ReportesDesaparicion()
    {
        InitializeComponent();
    }

    private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
    {
        ((ReportesDesaparicionViewModel)DataContext).ReporteClickCommand.Execute(null);
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ((ReportesDesaparicionViewModel)DataContext).DesaparecidoClickCommand.Execute(null);
    }
}