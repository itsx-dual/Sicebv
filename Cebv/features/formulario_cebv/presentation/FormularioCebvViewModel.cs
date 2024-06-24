using System.ComponentModel;
using Cebv.core.domain;
using Cebv.core.modules.reportante.data;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvViewModel : ObservableObject, 
    IRecipient<NombreCompletoMessage>,
    IRecipient<GuardarBorradorMessage>
{
    [ObservableProperty] private string _nombreCompleto = string.Empty;
    [ObservableProperty] private bool _puedeGuardar;
    
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private ISnackbarService _snackbarService = App.Current.Services.GetService<ISnackbarService>()!;
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
    
    [RelayCommand]
    public async void OnGenerarFolio()
    {
        var id = _reporteService.GetReporteId();
        var client = CebvClientHandler.SharedClient;
        using var response = await client.GetAsync($"/api/reportes/asignar_folio/{id}");

        if (response.IsSuccessStatusCode)
        {
            _snackbarService.Show(
                "Folio/s asignado correctamente.", 
                "",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.Alert32),
                new TimeSpan(0,0, 5));
        }
        else
        {
            _snackbarService.Show(
                "Folio no asignado.", 
                "Persona desaparecida o no localizada no tiene un folio asignado",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.ErrorCircle24),
                new TimeSpan(0,0, 5));
        }
    }
}