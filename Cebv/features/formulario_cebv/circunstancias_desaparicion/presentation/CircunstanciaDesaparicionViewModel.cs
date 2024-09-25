using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.hipotesis.presentation;
using static Cebv.core.util.enums.EtapaHipotesis;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
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

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = null!;
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

    /**
     * Constructor de la clase.
     */
    public CircunstanciaDesaparicionViewModel()
    {
        LoadAsync();
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any())
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Reporte.HechosDesaparicion ??= new();


        HipotesisPrimaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialPrimaria);
        HipotesisSecundaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialSecundaria);

        EnsureHipotesisExists(ref _hipotesisPrimaria, InicialPrimaria);
        EnsureHipotesisExists(ref _hipotesisSecundaria, InicialSecundaria);
    }

    private async void LoadAsync()
    {
        TiposDomicilio = await CebvNetwork.GetRoute<Catalogo>("tipos-domicilio");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");
        var reporte = _reporteService.GetReporte();

        // Lugar de los hechos
        var estadoId =
            reporte.HechosDesaparicion?.Direccion.Asentamiento?.Municipio?.Estado?.Id;

        var municipioId =
            reporte.HechosDesaparicion?.Direccion.Asentamiento?.Municipio?.Id;

        if (estadoId is not null)
        {
            EstadoSelected = Estados.FirstOrDefault(x => x.Id == estadoId);
            Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", estadoId);
        }

        if (municipioId != null)
        {
            MunicipioSelected = Municipios.FirstOrDefault(x => x.Id == municipioId);
            Asentamientos = await CebvNetwork.GetByFilter<Asentamiento>("asentamientos", "municipio_id", municipioId);
        }

        FoliosPrevios();
    }

    private void EnsureHipotesisExists(ref Hipotesis? hipotesis, string etapa)
    {
        if (hipotesis is not null) return;

        var nuevaHipotesis = new Hipotesis { Etapa = etapa };

        hipotesis = nuevaHipotesis;

        Reporte.Hipotesis.Add(hipotesis);
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

    [ObservableProperty] private ObservableCollection<Persona> _personas = new();

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
        if (Reporte.Expedientes.Any(p => p.Persona?.Id == persona.Id)) return;

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

        Reporte.Expedientes.Add(expediente);
    }

    [RelayCommand]
    private void RemoveExpediente(Expediente expediente)
    {
        Reporte.Expedientes.Remove(expediente);
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