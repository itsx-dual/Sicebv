using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static Cebv.core.util.enums.TipoExpediente;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class RelacionarExpedienteViewModel : ObservableObject
{
    public event EventHandler<Expediente>? GuardarExpediente;

    public HechosDesaparicion Item { get; }
    public String NombreDesaparecido { get; }

    public RelacionarExpedienteViewModel(string nombre, HechosDesaparicion item)
    {
        TiposExpedientes = [Directo, Indirecto];

        CargarCatalogos();
        Item = item;
        NombreDesaparecido = nombre;
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
        if (string.IsNullOrEmpty(TipoExp) || Parentesco.Id is null) return;
        Expediente.Reporte.Id = Item.ReporteId;
        Expediente.Tipo = TipoExp;
        Expediente.Parentesco = Parentesco;

        GuardarExpediente?.Invoke(this, Expediente);

        if (CloseAction != null)
            CloseAction();
    }

    public Action? CloseAction { get; set; }
}