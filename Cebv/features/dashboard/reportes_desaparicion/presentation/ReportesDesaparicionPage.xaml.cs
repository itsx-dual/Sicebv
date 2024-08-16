using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Cebv.features.dashboard.reportes_desaparicion.presentation;

public partial class ReportesDesaparicionPage : Page
{
    public ReportesDesaparicionPage()
    {
        InitializeComponent();
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (sender is null) return;
        var scroller = sender as ScrollViewer;
        var datacontext = DataContext as ReportesDesaparicionViewModel;

        if (!(scroller.VerticalOffset > scroller.ScrollableHeight * 0.8)) return;
        datacontext?.EndingScrollingCommand.Execute(null);
    }
}