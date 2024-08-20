using System.Windows.Controls;
using System.Windows.Input;

namespace Cebv.features.dashboard.reportes_desaparicion.presentation;

public partial class ReportesDesaparicionPage : Page
{
    public ReportesDesaparicionPage()
    {
        InitializeComponent();
    }
    
    private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        var datacontext = DataContext as ReportesDesaparicionViewModel;
        if (!(e.VerticalOffset > e.ExtentHeight * 0.8)) return;
        datacontext?.EndingScrollingCommand.Execute(null);
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ((ReportesDesaparicionViewModel)DataContext).ReporteClickCommand.Execute(null);
    }
}