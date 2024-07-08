using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.hipotesis.presentation;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciaDesaparicionViewModel : ObservableObject
{
    private IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;

    /**
     * Constructor de la clase.
     */
    public CircunstanciaDesaparicionViewModel()
    {
        Reporte = _reporteService.GetReporte();

        // Si no existe los hechos de desaparición se asume que es la primera vez capturando
        // y se crean unos nuevos completamente en blanco para que no haya error de nullabilidad.
        Reporte.HechosDesaparicion ??= new();

        SyncHipotesis();

        if (!string.IsNullOrEmpty(Reporte.HechosDesaparicion.FechaDesaparicionCebv) ||
            !string.IsNullOrEmpty(Reporte.HechosDesaparicion.FechaPercatoCebv))
            FechaAproximada = true;
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private string _horaDesaparicion = String.Empty;

    [ObservableProperty] private string _horaPercato = String.Empty;

    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _amenazaCambioComportamientoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _amenazaCambioComportamiento = false;
    [ObservableProperty] private string _amenazaDescripcion = String.Empty;

    partial void OnAmenazaCambioComportamientoOpcionChanged(string value)
    {
        AmenazaCambioComportamiento = OpcionesCebv.MappingToBool(value);
        Reporte.HechosDesaparicion!.CambioComportamiento = AmenazaCambioComportamiento;
    }

    // Hipotesis
    [ObservableProperty] private HipotesisViewModel _hipotesis = new();

    private void SyncHipotesis()
    {
        Reporte.Hipotesis ??= new();

        if (Reporte.Hipotesis is not null && Reporte.Hipotesis.Count > 0) return;

        Reporte.Hipotesis!.Add(new Hipotesis { Etapa = EtapaHipotesis.Inicial.ToString() });
        Reporte.Hipotesis!.Add(new Hipotesis { Etapa = EtapaHipotesis.Final.ToString() });
    }


    // Desaparicion asociada
    [ObservableProperty] private string _desaparecioAcompanadoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _desaparecioAcompanado = false;

    [ObservableProperty] private int _numeroPersonasMismoEvento = 1;


    /**
     * Expedientes directos e indirectos
     */
    [ObservableProperty] private ExpedienteViewModel _expedienteDirecto = new();

    [ObservableProperty] private ExpedienteViewModel _expedienteIndirecto = new();

    /**
     * Lógica de guardado
     */
    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}