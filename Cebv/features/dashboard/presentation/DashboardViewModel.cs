using Cebv.features.dashboard.data;
using Cebv.features.dashboard.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.dashboard.presentation;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private Usuario _usuario;

    public DashboardViewModel()
    {
        _cargarUsuario();
    }

    private async void _cargarUsuario()
    {
        Usuario = await DashboardNetwork.GetUsuarioActualRequest();
    }
}