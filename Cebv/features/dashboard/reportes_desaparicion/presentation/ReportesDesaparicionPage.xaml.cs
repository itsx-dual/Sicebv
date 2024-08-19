using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ListView = Wpf.Ui.Controls.ListView;

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
        if (!(e.VerticalOffset > e.ExtentHeight * 0.5)) return;
        datacontext?.EndingScrollingCommand.Execute(null);
    }
}