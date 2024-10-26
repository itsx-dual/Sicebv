using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.hipotesis.presentation;
using static Cebv.core.util.enums.EtapaHipotesis;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.dashboard.presentation;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.util.CollectionsHelper;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciaDesaparicionViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    private static IDashboardNavigationService _dashboardNavigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();
    [ObservableProperty] private Hipotesis _p = new();
    [ObservableProperty] private Hipotesis? _hipotesisPrimaria;
    [ObservableProperty] private Hipotesis? _hipotesisSecundaria;

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    [ObservableProperty] private ObservableCollection<Folio> _folios = new();

    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private Estado? _estadoSelected;
    [ObservableProperty] private Municipio? _municipioSelected;

    [ObservableProperty] private HipotesisViewModel _hipotesis = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposDomicilio = new();
    
    [ObservableProperty] private Expediente _expedienteSelected = new();


    /**
     * Constructor de la clase.
     */
    public CircunstanciaDesaparicionViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        HipotesisPrimaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialPrimaria);
        HipotesisSecundaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialSecundaria);

        EnsureObjectExists(ref _hipotesisPrimaria, Reporte.Hipotesis, P.ParametrosInicialPrimaria);
        EnsureObjectExists(ref _hipotesisSecundaria, Reporte.Hipotesis, _p.ParametrosInicialSecundaria);

        Reporte.HechosDesaparicion ??= new();
    }

    private async void InitAsync()
    {
        TiposDomicilio = await CebvNetwork.GetRoute<Catalogo>("tipos-domicilio");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");

        var reportante = _reporteService.GetReporte().Reportantes.FirstOrDefault();

        var est =
            reportante?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio?.Estado;

        var mpio =
            reportante?.Persona.Direcciones.FirstOrDefault()?.Asentamiento?.Municipio;

        if (est is not null)
        {
            Municipios = await CebvNetwork.GetByFilter<Municipio>("municpios", "estado_id", est.Id);
            EstadoSelected = est;
        }

        if (mpio is not null)
        {
            Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", mpio.Id);
            MunicipioSelected = mpio;
        }

        FoliosPrevios();
    }

    private async void FoliosPrevios()
    {
        if (Desaparecido.Persona.Id is null) return;

        Folios = await CebvNetwork.GetById<Folio>("personas", $"{Desaparecido.Persona.Id}/folios");
    }

    /**
     * Expedientes directos e indirectos
     */
    [ObservableProperty] private string? _nombre;

    [ObservableProperty] private string? _primerApellido;
    [ObservableProperty] private string? _segundoApellido;
    [ObservableProperty] private ObservableCollection<HechosDesaparicion> _hechosDesaparicion = new();
    [ObservableProperty] private ObservableCollection<Expediente> _temporal = new();


    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value is null) return;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", value.Id);
    }

    async partial void OnMunicipioSelectedChanged(Municipio? value)
    {
        if (value is null) return;
        Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", value.Id);
    }


    [RelayCommand]
    private async Task BuscharHechos()
    {
        HechosDesaparicion = await CebvNetwork.GetRoute<HechosDesaparicion>
            ($"filtrar-hechos?nombre={Nombre}&apellidoPaterno={PrimerApellido}&apellidoMaterno={SegundoApellido}");
    }

    [RelayCommand]
    private void ClearPersona()
    {
        Nombre = null;
        PrimerApellido = null;
        SegundoApellido = null;
    }

    [RelayCommand]
    private void AddExpediente(HechosDesaparicion item)
    {
        if (item.ReporteId == Reporte.Id) return;
        if (Reporte.Expedientes.FirstOrDefault(i => i.Reporte.Id == item.Id) is null) return;

        var viewModel = new RelacionarExpedienteViewModel(Desaparecido.Persona.NombreCompleto, item);

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

        Reporte.Expedientes.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpediente(Expediente expediente)
    {
        var itemToRemove = Reporte.Expedientes.FirstOrDefault(i => i.Id == expediente.Id);

        if (itemToRemove is not null)
        {
            Reporte.Expedientes.Remove(itemToRemove);
        }
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

    [RelayCommand]
    private async Task OnReporteClick()
    {
        if (ExpedienteSelected.Reporte.Id is null) return;

        _dashboardNavigationService.Navigate(typeof(LoadingPage));
        Console.WriteLine(ExpedienteSelected.Reporte.Id);
        Reporte = await _reporteService.Reload(ExpedienteSelected.Reporte.Id.Value);
        _reporteService.SetStatusReporte(EstadoReporte.Cargado);
        Console.WriteLine($"Reporte {Reporte.Id} cargado");
        _dashboardNavigationService.Navigate(typeof(FormularioCebvPage));
        _dashboardNavigationService.ClearNavigationStack();
    }
}