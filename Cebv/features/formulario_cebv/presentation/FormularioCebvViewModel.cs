using Cebv.core.modules.reportante.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvViewModel : ObservableObject, 
    IRecipient<NombreCompletoMessage>,
    IRecipient<GuardarBorradorMessage>
{
    [ObservableProperty] private string _nombreCompleto = string.Empty;
    [ObservableProperty] private bool _puedeGuardar;
    public Type callerType = null;
    
    /**
     * Reportante
     */
    [ObservableProperty] private ReportanteRequest _reportante = new();

    public FormularioCebvViewModel()
    {
        WeakReferenceMessenger.Default.Register<NombreCompletoMessage>(this);
        WeakReferenceMessenger.Default.Register<GuardarBorradorMessage>(this);
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