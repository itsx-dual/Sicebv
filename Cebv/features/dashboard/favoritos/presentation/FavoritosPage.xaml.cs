using System.Windows.Controls;
using System.Windows.Input;

namespace Cebv.features.dashboard.favoritos.presentation;

public partial class FavoritosPage : Page
{
    public FavoritosPage()
    {
        InitializeComponent();
    }
    
    private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        var datacontext = DataContext as FavoritosViewModel;
        if (!(e.VerticalOffset > e.ExtentHeight * 0.8)) return;
        datacontext?.EndingScrollingCommand.Execute(null);
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ((FavoritosViewModel)DataContext).ReporteClickCommand.Execute(null);
    }
}