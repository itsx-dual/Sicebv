using System.Collections.ObjectModel;
using System.Reflection;
using Cebv.core.modules.reportante.domain;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableObject
{
    private static IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Reportante _reportante;
    [ObservableProperty] private Desaparecido _desaparecido;
    
    // Catalogos y valores predefinidos
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMedios = new();
    [ObservableProperty] private ObservableCollection<MedioConocimiento> _medios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();
    
    // Valores seleccionados
    [ObservableProperty] private Catalogo _tipoMedioSelected;
    [ObservableProperty] private Estado _estadoSelected;
    [ObservableProperty] private Municipio _municipioSelected;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaReportanteSelected;
    [ObservableProperty] private Catalogo? _compañiaTelefonicaDesaparecidoSelected;
    
    // Valores para insercion a listas
    [ObservableProperty] private string _noTelefonoReportante;
    [ObservableProperty] private string _ObservacionesTelefonoReportante;
    [ObservableProperty] private string _noTelefonoDesaparecido;
    [ObservableProperty] private string _ObservacionesTelefonoDesaparecido;

    [ObservableProperty] private bool _seConoceFechaNacimientoDesaparecido = true;
    [ObservableProperty] private bool _seConoceFechaExactaHechos = true;

    public EncuadrePreeliminarViewModel() =>
        InitAsync();

    private async Task CargarCatalogos()
    {
        Sexos = await ReportanteNetwork.GetCatalogo("sexos");
        Generos = await ReportanteNetwork.GetCatalogo("generos");
        Parentescos = await ReportanteNetwork.GetCatalogo("parentescos");
        Nacionalidades = await ReportanteNetwork.GetCatalogo("nacionalidades");
        CompaniasTelefonicas = await DesaparecidoNetwork.GetCatalogo("companias-telefonicas");
        Estados = await ReportanteNetwork.GetEstados();
        TiposMedios = await DatosReporteNetwork.GetTiposMedios();
    }
    

    private async void InitAsync()
    {
        await CargarCatalogos();
        Reporte = _reporteService.GetReporte();

        if (Reporte.Reportantes.Any())
        {
            Reportante = Reporte.Reportantes.First();
        }
        else
        {
            Reportante = new Reportante();
            Reporte.Reportantes.Add(Reportante);
        }

        if (Reporte.Desaparecidos.Any())
        {
            Desaparecido = Reporte.Desaparecidos.First();
        }
        else
        {
            Desaparecido = new Desaparecido();
            Reporte.Desaparecidos.Add(Desaparecido);
        }
    }
    
    async partial void OnTipoMedioSelectedChanged(Catalogo value) =>
        Medios = await DatosReporteNetwork.GetMedios(value.Id);

    async partial void OnEstadoSelectedChanged(Estado value) =>
        Municipios = await ReportanteNetwork.GetMunicipiosDeEstado(value.Id);
    
    async partial void OnMunicipioSelectedChanged(Municipio value) =>
        Asentamientos = await ReportanteNetwork.GetAsentamientosDeMunicipio(value.Id);
    
    [RelayCommand]
    private void OnAddTelefonoMovilReportante()
    {
        if (NoTelefonoReportante.Length <= 0) return;
        
        var telefonos = Reportante.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoReportante,
            Observaciones = ObservacionesTelefonoReportante,
            EsMovil = true,
            Compania = CompañiaTelefonicaReportanteSelected
        });
        
        NoTelefonoReportante = string.Empty; 
        ObservacionesTelefonoReportante = string.Empty;
        CompañiaTelefonicaReportanteSelected = null;
    }
    
    [RelayCommand]
    private void OnAddTelefonoMovilDesaparecido()
    {
        if (NoTelefonoDesaparecido.Length <= 0) return;
        
        var telefonos = Desaparecido.Persona?.Telefonos;
        telefonos?.Add(new Telefono
        {
            Numero = NoTelefonoDesaparecido,
            Observaciones = ObservacionesTelefonoDesaparecido,
            EsMovil = true,
            Compania = CompañiaTelefonicaDesaparecidoSelected
        });
        
        NoTelefonoDesaparecido = string.Empty; 
        ObservacionesTelefonoDesaparecido = string.Empty;
        CompañiaTelefonicaDesaparecidoSelected = null;
    }
    
    [RelayCommand]
    private void OnRemoveTelefonoReportante(Telefono telefono)
    {
        Reportante.Persona.Telefonos?.Remove(telefono);
    }
    
    [RelayCommand]
    private void OnRemoveTelefonoDesaparecido(Telefono telefono)
    {
        Desaparecido.Persona.Telefonos?.Remove(telefono);
    }

    [RelayCommand]
    private void OnGuardarReporte()
    {
        if (_reporteService.Sync() == null)
        {
            _snackBarService.Show("Error fatal", "No se pudo actualizar o ingresar la informacion del reporte", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.Wallet24), new TimeSpan(0,0,5));
        }
    }
}