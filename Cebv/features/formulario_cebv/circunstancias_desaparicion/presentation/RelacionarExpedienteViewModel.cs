using System.Collections.ObjectModel;
using Cebv.core.modules.reportante.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Persona = Cebv.core.modules.persona.data.Persona;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public class Expediente
{
    public Persona Persona { get; set; } = new();
    public Catalogo Parentesco { get; set; } = new();
}

public partial class RelacionarExpedienteViewModel : ObservableObject
{
    public event EventHandler<Expediente>? GuardarExpediente;

    public Persona Persona { get; set; }

    public RelacionarExpedienteViewModel(Persona persona)
    {
        CargarCatalogos();
        Persona = persona;
    }
    
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();
    
    [ObservableProperty] private Expediente _expediente = new();
    
    
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetCatalogo("parentescos");
    }
    
    [RelayCommand]
    private void OnGuardarPrenda()
    {
        Expediente.Persona = Persona;
        Expediente.Parentesco = Parentesco;
        
        GuardarExpediente?.Invoke(this, Expediente);

        if (CloseAction != null)
            CloseAction();
    }

    public Action? CloseAction { get; set; }
}