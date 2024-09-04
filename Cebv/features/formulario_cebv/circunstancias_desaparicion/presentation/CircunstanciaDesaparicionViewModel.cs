using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.hipotesis.presentation;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

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
        LoadAsync();
    }

    private async void LoadAsync()
    {
        TiposDomicilio = await CebvNetwork.GetCatalogo("tipos-domicilio");
        Reporte = _reporteService.GetReporte();

        Reporte.HechosDesaparicion ??= new();

        SyncHipotesis();

        if (!string.IsNullOrEmpty(Reporte.HechosDesaparicion!.FechaDesaparicionCebv) ||
            !string.IsNullOrEmpty(Reporte.HechosDesaparicion.FechaPercatoCebv))
            FechaAproximada = true;
        
        AmenazaCambioComportamiento = Reporte.HechosDesaparicion!.AmenazaCambioComportamiento;

        AmenazaCambioComportamientoOpcion = AmenazaCambioComportamiento switch
        {
            true => OpcionesCebv.Si,
            false => OpcionesCebv.No,
            _ => OpcionesCebv.NoEspecifica
        };

        DesaparecioAcompanado = Reporte.HechosDesaparicion!.DesaparecioAcompanado;

        DesaparecioAcompanadoOpcion = DesaparecioAcompanado switch
        {
            true => OpcionesCebv.Si,
            false => OpcionesCebv.No,
            _ => OpcionesCebv.NoEspecifica
        };
        
        FoliosPrevios();
    }

    private async void FoliosPrevios()
    {
        if (Reporte.Desaparecidos.Count == 0) return;
        
        var persona = Reporte.Desaparecidos[0].Persona;

        if (persona.Id is null) return;

        Folios = await CircunstanciaDesaparicionNetwork.GetFoliosPrevios(persona.Id);
    }

    [ObservableProperty] private ObservableCollection<Folio> _folios = new();

    /**
     * Variables de la clase
     */
    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string? _amenazaCambioComportamientoOpcion;
    [ObservableProperty] private bool? _amenazaCambioComportamiento = false;
    [ObservableProperty] private string _amenazaDescripcion = String.Empty;

    partial void OnAmenazaCambioComportamientoOpcionChanged(string value)
    {
        AmenazaCambioComportamiento = OpcionesCebv.MappingToBool(value);
        Reporte.HechosDesaparicion!.AmenazaCambioComportamiento = AmenazaCambioComportamiento;
    }

    // Hipotesis
    [ObservableProperty] private HipotesisViewModel _hipotesis = new();
    [ObservableProperty] private Catalogo? _sitio;
    [ObservableProperty] private Catalogo? _area;
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposDomicilio = new();

    partial void OnSitioChanged(Catalogo? value)
    {
        Reporte.Hipotesis![0].Sitio = value;
        Reporte.Hipotesis![1].Sitio = value;
    }

    partial void OnAreaChanged(Catalogo? value)
    {
        Reporte.Hipotesis![0].Area = value;
        Reporte.Hipotesis![1].Area = value;
    }


    private void SyncHipotesis()
    {
        Reporte.Hipotesis ??= new();

        if (Reporte.Hipotesis is not null && Reporte.Hipotesis.Count > 0)
        {
            Area = Reporte.Hipotesis![0].Area;
            Sitio = Reporte.Hipotesis![0].Sitio;
            return;
        }

        Reporte.Hipotesis!.Add(new Hipotesis { Etapa = EtapaHipotesis.Inicial.ToString() });
        Reporte.Hipotesis!.Add(new Hipotesis { Etapa = EtapaHipotesis.Inicial.ToString() });
    }


    // Desaparicion asociada
    [ObservableProperty] private string _desaparecioAcompanadoOpcion;
    [ObservableProperty] private bool? _desaparecioAcompanado = false;

    partial void OnDesaparecioAcompanadoOpcionChanged(string value)
    {
        DesaparecioAcompanado = OpcionesCebv.MappingToBool(value);
        Reporte.HechosDesaparicion!.DesaparecioAcompanado = DesaparecioAcompanado;
    }

    /**
     * Expedientes directos e indirectos
     */
    [ObservableProperty] private string? _nombre;
    [ObservableProperty] private string? _primerApellido;
    [ObservableProperty] private string? _segundoApellido;
    
    [ObservableProperty] private ObservableCollection<Persona> _personas = new();

    /**
     * Logica para la relación de los expedientes.
     */
    [RelayCommand]
    private async Task BuscarPersona()
    {
        Personas = await CircunstanciaDesaparicionNetwork.SearchPersona(
            Nombre,
            PrimerApellido,
            SegundoApellido
        );
    }
    
    [RelayCommand]
    private void AddExpediente(Persona persona)
    {
        if (Reporte.Expedientes!.Any(p => p.Persona!.Id == persona.Id)) return;

        var viewModel = new RelacionarExpedienteViewModel(persona);

        // Suscribirse al evento de guardado
        viewModel.GuardarExpediente += OnExpedienteGuardado;

        // Abrir la ventana de edición de la prenda
        var dialog = new AgregarExpediente { DataContext = viewModel };

        // Configurar la acción de cierre para la ventana de edición
        if (dialog.DataContext is RelacionarExpedienteViewModel vm)
        {
            vm.CloseAction = () => dialog.Close();
        }

        dialog.ShowDialog();
    }

    private void OnExpedienteGuardado(object? sender, Expediente expediente)
    {
        if (sender is not RelacionarExpedienteViewModel) return;

        Reporte.Expedientes!.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpediente(Expediente expediente)
    {
        Reporte.Expedientes!.Remove(expediente);
    }

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