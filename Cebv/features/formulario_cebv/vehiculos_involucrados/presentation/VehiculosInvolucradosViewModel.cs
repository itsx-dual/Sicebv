using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.vehiculos_involucrados.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.presentation;

public partial class VehiculosInvolucradosViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    private ShowDialog _showDialog;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Vehiculo _vehiculo = new();

    /**
     * Constructor de la clase
     */
    public VehiculosInvolucradosViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();
    }

    private async void InitAsync()
    {
        Relaciones = await CebvNetwork.GetRoute<Catalogo>("relaciones-vehiculos");
        Marcas = await CebvNetwork.GetRoute<Catalogo>("marcas-vehiculos");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
        TiposVehiculos = await CebvNetwork.GetRoute<Catalogo>("tipos-vehiculos");
        UsosVehiculos = await CebvNetwork.GetRoute<Catalogo>("usos-vehiculos");
    }

    // Path de las imagenes seleccionadas
    [ObservableProperty] private string[]? _openedFilePath = [];

    [ObservableProperty] private ObservableCollection<Catalogo> _relaciones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _marcas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposVehiculos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _usosVehiculos = new();

    [RelayCommand]
    private void OnAddVehiculo()
    {
        if (Vehiculo.RelacionVehiculo is null) return;
        Reporte.Vehiculos.Add(Vehiculo);
        Vehiculo = new();
    }
    
    [RelayCommand]
    private void OnRemoveVehiculo(Vehiculo vehiculo)
    {
        Reporte.Vehiculos.Remove(vehiculo);
    }

    [RelayCommand]
    private void OnOpenFile()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
            Multiselect = true
        };

        if (openFileDialog.ShowDialog() is not true) return;

        if (!File.Exists(openFileDialog.FileName)) return;

        OpenedFilePath = openFileDialog.FileNames;
    }

    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion;
        
        Vehiculo vehiculo = Vehiculo;
        List<string> emptyElements = ListEmptyElements.EnlistarElementosVacios(vehiculo);

        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            confirmacion = dialogo.Confirmacion;
        }
        else confirmacion = true;

        return confirmacion;
    }
    
    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    { 
        if (!await EnlistarCampos())
            return;
      
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}