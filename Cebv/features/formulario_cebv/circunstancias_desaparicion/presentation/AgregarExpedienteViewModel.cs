using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.persona.data;
using Cebv.core.modules.reportante.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public class Expediente
{
    public Persona Persona { get; set; } = new();
    public Catalogo Parentesco { get; set; } = new();
}

public partial class AgregarExpedienteViewModel : ObservableObject
{
    public event EventHandler<Expediente>? GuardarExpediente;

    public Persona Persona { get; set; }

    public AgregarExpedienteViewModel(Persona persona)
    {
        CargarCatalogos();
        Persona = persona;
    }
    
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();
    
    [ObservableProperty] private Expediente _expediente = new();
    
    
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetParentescos();
    }
    
    [RelayCommand]
    private void GuardarPrenda()
    {
        
        Expediente.Persona = Persona;
        Expediente.Parentesco = Parentesco;
        
        GuardarExpediente?.Invoke(this, Expediente);

        if (CloseAction != null)
            CloseAction();
    }

    public Action? CloseAction { get; set; }
}