using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class RelacionarExpedienteViewModel : ObservableObject
{
    public event EventHandler<Expediente>? GuardarExpediente;

    private HechosDesaparicion Item { get; }
    private int ReporteId { get; }

    public RelacionarExpedienteViewModel(int reporteId, HechosDesaparicion item)
    {
        TiposExpedientes =
        [
            TipoExpediente.Directo.ToString(),
            TipoExpediente.Indirecto.ToString()
        ];

        CargarCatalogos();
        ReporteId = reporteId;
        Item = item;
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();

    [ObservableProperty] private ObservableCollection<string> _tiposExpedientes;
    [ObservableProperty] private string _tipoExp = String.Empty;
    
    [ObservableProperty] private Expediente _expediente = new();


    private async void CargarCatalogos()
    {
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
    }

    [RelayCommand]
    private void OnGuardarExpediente()
    {
        Expediente.ReporteUno!.Id = ReporteId;
        Expediente.ReporteDos!.Id = Item.ReporteId;
        Expediente.Tipo = TipoExp;
        Expediente.Parentesco = Parentesco;

        GuardarExpediente?.Invoke(this, Expediente);

        if (CloseAction != null)
            CloseAction();
    }

    public Action? CloseAction { get; set; }
}