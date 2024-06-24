

using System.Collections.ObjectModel;
using System.IO;
using Cebv.core.data;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.domain;
using Cebv.features.formulario_cebv.control_ogpi.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosLocalizacionViewModel : ObservableObject
{
    /**
     * Constructor
     */
    public DatosLocalizacionViewModel() => CargarCatalogos();

    /**
     * Localización
     */
    [ObservableProperty] private bool _localizadaConVida;

    [ObservableProperty] private ObservableCollection<Estado> _estados = new();
    [ObservableProperty] private Estado _estado = new();
    [ObservableProperty] private ObservableCollection<Municipio> _municipios = new();
    [ObservableProperty] private Municipio _municipio = new();

    // Hipotesis
    [ObservableProperty] private ObservableCollection<TipoHipotesis> _tiposHipotesis = new();
    [ObservableProperty] private TipoHipotesis _tipoHipotesisUno = new();
    [ObservableProperty] private TipoHipotesis _tipoHipotesisDos = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _sitios = new();
    [ObservableProperty] private Catalogo _sitio = new();

    [ObservableProperty] private string _areaCodifica = String.Empty;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _estatusPersonas = new();
    [ObservableProperty] private Catalogo _estatusPersona = new();


    private async void CargarCatalogos()
    {
        //Estados = await UbicacionNetwork.GetEstados();
        TiposHipotesis = await CircunstanciaDesaparicionNetwork.GetTiposHipotesis();
        Sitios = await CircunstanciaDesaparicionNetwork.GetSitios();
        EstatusPersonas = await ControlOgpiNetwork.GetEstatusPersonas();

    }

    async partial void OnEstadoChanged(Estado value) =>
        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);


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
}