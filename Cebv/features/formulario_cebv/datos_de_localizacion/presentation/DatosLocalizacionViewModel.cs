using System.Collections.ObjectModel;
using System.IO;
using Cebv.core.domain;
using Cebv.core.modules.hipotesis.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using static Cebv.core.data.OpcionesCebv;
using static Cebv.core.util.CollectionsHelper;
using static Cebv.core.util.enums.EtapaHipotesis;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosLocalizacionViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();
    [ObservableProperty] private Hipotesis _p = new();
    [ObservableProperty] private Hipotesis? _hipotesisPrimaria;
    [ObservableProperty] private Hipotesis? _hipotesisSecundaria;
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private ObservableCollection<Asentamiento> _asentamientos = new();

    [ObservableProperty] private Estado? _estadoSelected;
    [ObservableProperty] private Municipio? _municipioSelected;

    [ObservableProperty] private HipotesisViewModel _hipotesis = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposDomicilio = new();

    public DatosLocalizacionViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Desaparecido.Localizacion ??= new();
        
        HipotesisPrimaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == FinalPrimaria);
        HipotesisSecundaria = Reporte.Hipotesis.FirstOrDefault(x => x.Etapa == FinalSecundaria);
        
        EnsureObjectExists(ref _hipotesisPrimaria, Reporte.Hipotesis, P.ParametrosFinalPrimaria);
        EnsureObjectExists(ref _hipotesisSecundaria, Reporte.Hipotesis, P.ParametrosFinalSecundaria);

        _localizadoVivo = Desaparecido.EstatusPreliminar?.Id == 3;
    }

    private async void InitAsync()
    {
        EstatusPersonas = await CebvNetwork.GetRoute<BasicResource>("estatus-personas");
        Estados = await CebvNetwork.GetRoute<Estado>("estados");

        var reporte = _reporteService.GetReporte();

        var mpio = reporte.Desaparecidos.FirstOrDefault()?.Localizacion?.MunicipioLocalizacion;

        if (mpio is not null)
        {
            Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", mpio.Estado?.Id!);
            MunicipioSelected = mpio;
        }
    }

    [ObservableProperty] private ObservableCollection<BasicResource> _estatusPersonas = new();
    [ObservableProperty] private bool _localizadoVivo;

    async partial void OnEstadoSelectedChanged(Estado? value)
    {
        if (value?.Id is null) return;
        Municipios = await CebvNetwork.GetByFilter<Municipio>("municipios", "estado_id", value.Id);
    }


    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty] private string? _openedInformeLocalizacionPath;

    [RelayCommand]
    private void OnOpenInformeLocalizacionFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Multiselect = false,
                Filter = "PDF files (*.pdf)|*.pdf|Word documents (*.doc;*.docx)|*.doc;*.docx"
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        OpenedInformeLocalizacionPath = openFileDialog.FileName;
    }


    [ObservableProperty] private string[]? _openedPruebaVidaPath = [];

    [RelayCommand]
    private void OnOpenPruebaVidaFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        OpenedPruebaVidaPath = openFileDialog.FileNames;
    }

    [ObservableProperty] private string[]? _openedIdentificacionOficialPath = [];

    [RelayCommand]
    private void OnOpenIdentificacionOficialFile()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        OpenedIdentificacionOficialPath = openFileDialog.FileNames;
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}