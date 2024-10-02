using System.Collections.ObjectModel;
using System.IO;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace Cebv.features.formulario_cebv.comunicacion_difusion.presentation;

public partial class GeneracionBoletinViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    public GeneracionBoletinViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);

        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
        
        EsMayorEdad = CalcularEdad();
    }

    private async void InitAsync()
    {
        TiposBoletines = await CebvNetwork.GetRoute<Catalogo>("tipos-boletines");
        EstatusPersonas = await CebvNetwork.GetRoute<BasicResource>("estatus-personas");
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposBoletines = new();
    [ObservableProperty] private Catalogo _tipoBoletin = new();
    [ObservableProperty] private ObservableCollection<BasicResource> _estatusPersonas = new();
    [ObservableProperty] private BasicResource _estatusPersona = new();
    
    [ObservableProperty] private bool _esMayorEdad;
    
    private bool CalcularEdad()
    {
        if (Desaparecido.Persona.FechaNacimiento is null) return false;

        return Desaparecido.Persona.FechaNacimiento.Value.AddYears(18) <= DateTime.Now;
    }

    /**
     * Path de las imagenes seleccionadas
     */
    [ObservableProperty] private string[]? _openedFilePath = [];

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

    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}