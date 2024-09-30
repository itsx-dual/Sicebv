using System.Windows.Controls;
using System.Windows.Input;
using Cebv.features.dashboard.filtro_busqueda.Presentation;
using Cebv.features.dashboard.presentation;

namespace Cebv.features.dashboard.filtro_busqueda;

public partial class FiltroBusquedaView : Page
{
    public FiltroBusquedaView()
    {
        InitializeComponent();
    }
    
    private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
    {
        ((FiltroBusquedaViewModel)DataContext).ReporteClickCommand.Execute(null);
    }

    private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        ((FiltroBusquedaViewModel)DataContext).DesaparecidoClickCommand.Execute(null);
    }
}