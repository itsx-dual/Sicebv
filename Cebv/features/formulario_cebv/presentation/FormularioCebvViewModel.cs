using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.formulario_cebv.presentation.data;
using Cebv.features.reportante.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvViewModel : ObservableObject, IRecipient<AddReporteMessage>,
    IRecipient<NombreCompletoMessage>, IRecipient<GuardarBorradorMessage>
{
    private readonly IFormularioService _formularioService;
    [ObservableProperty] private string _nombreCompleto = string.Empty;
    [ObservableProperty] private bool _puedeGuardar;
    
    /**
     * Reportante
     */
    [ObservableProperty] private ReportanteRequest _reportante = new();

    public FormularioCebvViewModel(IFormularioService formularioService)
    {
        _formularioService = formularioService;
        WeakReferenceMessenger.Default.Register<AddReporteMessage>(this);
        WeakReferenceMessenger.Default.Register<NombreCompletoMessage>(this);
        WeakReferenceMessenger.Default.Register<GuardarBorradorMessage>(this);
    }

    public void Receive(AddReporteMessage message)
    {
        _formularioService.Reporte = message.Value;
    }

    public void Receive(NombreCompletoMessage message)
    {
        NombreCompleto = message.Value;
    }

    public void Receive(GuardarBorradorMessage message)
    {
        PuedeGuardar = message.Value;
    }
}