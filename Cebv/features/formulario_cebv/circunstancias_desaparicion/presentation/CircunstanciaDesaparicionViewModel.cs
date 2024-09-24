using System.Collections.ObjectModel;
using Cebv.core.domain;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.hipotesis.presentation;
using Cebv.core.modules.ubicacion.presentation;
using static Cebv.core.util.enums.EtapaHipotesis;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using Cebv.features.formulario_cebv.folio_expediente.data;
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

    /**
     * Constructor de la clase.
     */
    public CircunstanciaDesaparicionViewModel()
    {
        LoadAsync();
        Reporte = _reporteService.GetReporte();

        Reporte.HechosDesaparicion ??= new();

        HipotesisPrimaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialPrimaria);
        HipotesisSecundaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == InicialSecundaria);

        EnsureHipotesisExists(ref _hipotesisPrimaria, InicialPrimaria);
        EnsureHipotesisExists(ref _hipotesisSecundaria, InicialSecundaria);
    }

    private async void LoadAsync()
    {
        TiposDomicilio = await CebvNetwork.GetRoute<Catalogo>("tipos-domicilio");

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
        if (!Reporte.Desaparecidos.Any())
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        if (Desaparecido.Persona?.Id is null) return;

        // TODO: Eliminar esto y hacerlo generico
        Folios = await CircunstanciaDesaparicionNetwork.GetFoliosPrevios(Desaparecido.Persona.Id);
    }

    [ObservableProperty] private ObservableCollection<Folio> _folios = new();

    /**
     * Variables de la clase
     */
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    // Hipotesis
    [ObservableProperty] private HipotesisViewModel _hipotesis = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposDomicilio = new();

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