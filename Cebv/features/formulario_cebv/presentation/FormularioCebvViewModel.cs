using System.ComponentModel;
using Cebv.core.modules.reportante.data;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvViewModel : ObservableObject, 
    IRecipient<NombreCompletoMessage>,
    IRecipient<GuardarBorradorMessage>
{
    [ObservableProperty] private string _nombreCompleto = string.Empty;
    [ObservableProperty] private bool _puedeGuardar;
    
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    [ObservableProperty] private Reporte _reporte;
    public Type callerType = null;
    
    /**
     * Reportante
     */
    [ObservableProperty] private ReportanteRequest _reportante = new();

    public FormularioCebvViewModel()
    {
        WeakReferenceMessenger.Default.Register<NombreCompletoMessage>(this);
        WeakReferenceMessenger.Default.Register<GuardarBorradorMessage>(this);
        Reporte = _reporteService.GetReporteActual();
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