using System.Collections.ObjectModel;
using System.IO;
using Cebv.core.domain;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.domain;
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
    
    
    private async void CargarCatalogos() =>
        Estados = await UbicacionNetwork.GetEstados();
    
    async partial void OnEstadoChanged(Estado value) =>
        Municipios = await UbicacionNetwork.GetMuncipios(value.Id);
    
    
    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty]
    private string[]? _openedFilePathUno = [];
    
    [RelayCommand]
    private void OnOpenFileUno()
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

        OpenedFilePathUno = openFileDialog.FileNames;
    }
    
    
    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty]
    private string[]? _openedFilePathDos = [];
    
    [RelayCommand]
    private void OnOpenFileDos()
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

        OpenedFilePathDos = openFileDialog.FileNames;
    }
    
    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty]
    private string[]? _openedFilePathTres = [];
    
    [RelayCommand]
    private void OnOpenFileTres()
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

        OpenedFilePathTres = openFileDialog.FileNames;
    }
}