using System.Windows.Controls;

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
        Console.WriteLine($"{e.VerticalOffset} {e.ExtentHeight}");
        if (!(e.VerticalOffset > e.ExtentHeight * 0.8)) return;
        datacontext?.EndingScrollingCommand.Execute(null);
    }
}