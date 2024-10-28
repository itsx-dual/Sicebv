using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.senas_particulares.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.senas_particulares.presentation;

public partial class SenasParticularesViewModel : ObservableObject
{
    private IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = new();
    [ObservableProperty] private Desaparecido _desaparecido = new();

    // Catalogos
    [ObservableProperty] private ObservableCollection<CatalogoColor> _vistas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos = new();
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados = new();
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo = new();

    // Valores seleccionados
    [ObservableProperty] private Catalogo _tipoSelected = new();
    [ObservableProperty] private CatalogoColor _regionCuerpoSelected = new();
    [ObservableProperty] private CatalogoColor _ladoSelected = new();
    [ObservableProperty] private CatalogoColor _vistaSelected = new();
    [ObservableProperty] private string _colorRegionCuerpo = string.Empty;
    [ObservableProperty] private string _colorLado = string.Empty;
    [ObservableProperty] private string _colorVista = string.Empty;

    // Propiedades para insercion a lista
    [ObservableProperty] private int _cantidad = 1;
    [ObservableProperty] private string _descripcion = string.Empty;

    public SenasParticularesViewModel()
    {
        InitAsync();
    }

    private void DefaultValues()
    {
        ColorRegionCuerpo = "3F48CC";
        ColorLado = "6C7156";
        VistaSelected = Vistas.First(e => e.Nombre == "NO ESPECIFICA");
        Descripcion = "";
        Cantidad = 1;
    }

    private async Task CargarCatalogos()
    {
        Vistas = await CebvNetwork.GetRoute<CatalogoColor>("vistas");
        Tipos = await CebvNetwork.GetRoute<Catalogo>("tipos");
        RegionesCuerpo = await CebvNetwork.GetRoute<CatalogoColor>("regiones-cuerpo");
        Lados = await CebvNetwork.GetRoute<CatalogoColor>("lados");
    }

    private async void InitAsync()
    {
        await CargarCatalogos();
        DefaultValues();
        Reporte = _reporteService.GetReporte();

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

    partial void OnColorRegionCuerpoChanged(string value)
    {
        if (value.Length < 1) return;

        // TODO: Aqui hay un error
        var region = RegionesCuerpo.FirstOrDefault(e => e.Color == value);
        RegionCuerpoSelected = region ?? RegionesCuerpo.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorLadoChanged(string value)
    {
        if (value.Length < 1) return;

        var lado = Lados.FirstOrDefault(e => e.Color == value);
        LadoSelected = lado ?? Lados.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorVistaChanged(string value)
    {
        if (value.Length < 1) return;

        var vista = Vistas.FirstOrDefault(e => e.Color == value);
        VistaSelected = vista ?? Vistas.First(e => e.Nombre == "NO ESPECIFICA");
    }

    [RelayCommand]
    private void OnAddSenaParticular()
    {
        Desaparecido.Persona.SenasParticulares.Add(new SenaParticular
        {
            Cantidad = Cantidad,
            Descripcion = Descripcion,
            Foto = null,
            RegionCuerpo = RegionCuerpoSelected,
            Vista = VistaSelected,
            Lado = LadoSelected,
            Tipo = TipoSelected
        });
    }

    [RelayCommand]
    private void OnDeleteSenaParticular(dynamic sena)
    {
        var senaParticular = sena as SenaParticular;
        if (senaParticular != null) Desaparecido.Persona.SenasParticulares.Remove(senaParticular);
    }
    
    

    private bool _cancelar = true;
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = SenasParticularesDictionary.GetSenaParticular(this);   
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            if (dialogo.Confirmacion == "Guardar") confirmacion = true;
            else if (dialogo.Confirmacion == "No guardar") return _cancelar = false;
        }
        else confirmacion = true;

        return confirmacion;
    }

    [RelayCommand]
    private async Task OnGuardarYContinuar(Type pageType)
    {
        if (!await EnlistarCampos())
        {   
            if (!_cancelar) _navigationService.Navigate(pageType);

            return;
        }

        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}