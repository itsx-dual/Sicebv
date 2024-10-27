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

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido;

    // Catalogos
    [ObservableProperty] private ObservableCollection<Catalogo> _vistas;
    [ObservableProperty] private ObservableCollection<Catalogo> _tipos;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _lados;
    [ObservableProperty] private ObservableCollection<CatalogoColor> _regionesCuerpo;

    // Valores seleccionados
    [ObservableProperty] private Catalogo _vistaSelected;
    [ObservableProperty] private Catalogo _tipoSelected;
    [ObservableProperty] private CatalogoColor _regionCuerpoSelected;
    [ObservableProperty] private CatalogoColor _ladoSelected;
    [ObservableProperty] private string _colorRegionCuerpo;
    [ObservableProperty] private string _colorLado;

    // Propiedades para insercion a lista
    [ObservableProperty] private int _cantidad = 1;
    [ObservableProperty] private string _descripcion;

    public SenasParticularesViewModel()
    {
        InitAsync();
    }

    private void DefaultValues()
    {
        ColorRegionCuerpo = "3F48CC";
        ColorLado = "6C7156";
        Descripcion = "";
        Cantidad = 1;
    }

    private async Task CargarCatalogos()
    {
        Vistas = await CebvNetwork.GetRoute<Catalogo>("vistas");
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
        if (value is null) return;

        // TODO: Aqui hay un error
        var region = RegionesCuerpo.FirstOrDefault(e => e.Color == value);
        RegionCuerpoSelected = region ?? RegionesCuerpo.First(e => e.Nombre == "NO ESPECIFICA");
    }

    partial void OnColorLadoChanged(string value)
    {
        if (value is null) return;
        
        var lado = Lados.FirstOrDefault(e => e.Color == value);
        LadoSelected = lado ?? Lados.First(e => e.Nombre == "NO ESPECIFICA");
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