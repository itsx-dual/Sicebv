using System.Collections.ObjectModel;
using System.IO;
using Cebv.core.data;
using Cebv.features.formulario_cebv.vehiculos_involucrados.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.presentation;

public partial class VehiculosInvolucradosViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public VehiculosInvolucradosViewModel()
    {
        CargarCatalogos();
    }
    
    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty]
    private string[]? _openedFilePath = [];
    
    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _relaciones = new();
    [ObservableProperty] private Catalogo _relacion = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _marcas = new();
    [ObservableProperty] private Catalogo _marca = new();
    [ObservableProperty] private string _submarca = String.Empty;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo _color = new();
    
    [ObservableProperty] private string _placa = String.Empty;
    [ObservableProperty] private string _modelo = String.Empty;
    [ObservableProperty] private string _numeroSerie = String.Empty;
    [ObservableProperty] private string _numeroMotor = String.Empty;
    [ObservableProperty] private string _numeroPermiso = String.Empty;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposVehiculos = new();
    [ObservableProperty] private Catalogo _tipoVehiculo = new();
    
    [ObservableProperty] private ObservableCollection<Catalogo> _usosVehiculos = new();
    [ObservableProperty] private Catalogo _usoVehiculo = new();
    
    [ObservableProperty] private string _senasParticulares = String.Empty;

    
    [RelayCommand]
    private void OnOpenFile()
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

        OpenedFilePath = openFileDialog.FileNames;
    }
    
    private async void CargarCatalogos()
    {
        Relaciones = await VehiculosNetwork.GetRelacionesVehiculos();
        Marcas = await VehiculosNetwork.GetMarcasVehiculos();
        Colores = await VehiculosNetwork.GetColores();
        TiposVehiculos = await VehiculosNetwork.GetTiposVehiculos();
        UsosVehiculos = await VehiculosNetwork.GetUsosVehiculos();
    }
}