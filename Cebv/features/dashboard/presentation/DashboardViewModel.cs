using Wpf.Ui.Controls;
using Cebv.features.dashboard.data;
using Cebv.features.dashboard.domain;
using System.Collections.ObjectModel;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.presentation;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private Usuario _usuario;

    
    [ObservableProperty]
    private ICollection<object> _menuItems = new ObservableCollection<object>
    {
        new NavigationViewItem("Inicio", SymbolRegular.Home24, typeof(Inicio)),
        new NavigationViewItem("Reportes de desaparición", SymbolRegular.News24, null)
        {
            MenuItemsSource = new object[]
            {
                new NavigationViewItem("Consultar", SymbolRegular.Library24, typeof(ReportesDesaparicion)),
                new NavigationViewItem("Capturar reporte", SymbolRegular.Pen24, typeof(FormularioCebvPage)),
            }
        },
    };
    
    public DashboardViewModel()
    {
        _cargarUsuario();
    }

    private async void _cargarUsuario()
    {
        Usuario = await DashboardNetwork.GetUsuarioActualRequest();
    }
}