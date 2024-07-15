using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.desaparicion_forzada.data;
using Cebv.features.formulario_cebv.desaparicion_forzada.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

public partial class PerpetradorViewModel : ObservableObject
{
    public PerpetradorViewModel()
    {
        CargarCatalogos();
    }

    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private Catalogo? _sexo;

    [ObservableProperty] private string _perpetradorDescripcion = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _estatusPerpetradores = new();
    [ObservableProperty] private Catalogo? _estatusPerpetrador;

    /**
     * Logica para añadir o eliminar perpetradores
     */
    [ObservableProperty] private ObservableCollection<Perpetrador> _perpetradores = new();

    private void ClearForm()
    {
        Nombre = String.Empty;
        PerpetradorDescripcion = String.Empty;
        Sexo = null;
        EstatusPerpetrador = null;
    }

    [RelayCommand]
    private void AddPerpetrador()
    {
        if (string.IsNullOrEmpty(Nombre)) return;

        var perpetrador = new Perpetrador
        {
            Nombre = Nombre,
            Descripcion = PerpetradorDescripcion,
            Sexo = Sexo,
            EstatusPerpetrador = EstatusPerpetrador
        };

        Perpetradores.Add(perpetrador);

        ClearForm();
    }

    [RelayCommand]
    private void RemovePerpetrador(Perpetrador perpetrador)
    {
        Perpetradores.Remove(perpetrador);
    }


    private async void CargarCatalogos()
    {
        Sexos = await CebvNetwork.GetCatalogo("sexos");
        EstatusPerpetradores = await CebvNetwork.GetCatalogo("estatus-perpetradores");
    }
}