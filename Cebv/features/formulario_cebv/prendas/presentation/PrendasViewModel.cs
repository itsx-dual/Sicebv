using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.prendas.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendasViewModel : ObservableObject
{
    private readonly IReporteService _reporteService = App.Current.Services.GetService<IReporteService>()!;
    private readonly IFormularioCebvNavigationService _navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>()!;
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    /**
     * Constructor de la clase
     */
    public PrendasViewModel() => LoadAsync();

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();
    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private bool _hayPrendas;
    
    /**
     * Variables para la insercion
     */
    [ObservableProperty] private PrendaVestir _prendaVestir = new();
    [ObservableProperty] private Catalogo? _grupoPertenencia;
    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void LoadAsync()
    {
        await CargarCatalogos();
        Reporte = _reporteService.GetReporte();
        
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.First();
        HayPrendas = Desaparecido.PrendasVestir.Any();
    }

    private async Task CargarCatalogos()
    {
        GruposPertenencias = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
    }

    partial void OnPrendaVestirChanged(PrendaVestir value)
    {
        GrupoPertenencia = value.Pertenencia?.GrupoPertenencia;
    }

    async partial void OnGrupoPertenenciaChanged(Catalogo? value)
    {
        if (value?.Id is null) return;
        Pertenencias = await CebvNetwork.GetByFilter<Pertenencia>("pertenencias", "grupo_pertenencia_id", value.Id.ToString()!);
    }
    
    /**
     * Añadir y eliminar prendas
     */
    [RelayCommand]
    private void OnAddPrenda()
    {
        //TODO: Validacion
        Desaparecido.PrendasVestir.Add(PrendaVestir);
        PrendaVestir = new PrendaVestir();
        HayPrendas = Desaparecido.PrendasVestir.Any();
    }

    [RelayCommand]
    private async Task OnEditPrenda(PrendaVestir prenda)
    {
        var prendaClone = new PrendaVestir(prenda);
        
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Editar Prenda de Vestir",
                Content = new EditPrendaVestirUserControl
                {
                    DataContext = new PrendasViewModel
                    {
                        PrendaVestir = prendaClone,
                    }
                },
                PrimaryButtonText = "Guardar",
                CloseButtonText = "Descartar"
            },
            new CancellationToken()
        );
        
        if (result != ContentDialogResult.Primary) return;
        
        // Se busca el indice del elemento seleccionado en la lista.
        var index = Desaparecido.PrendasVestir.ToList().FindIndex(x => x.Equals(prenda));
        
        // Se sobreescribe la instancia anterior.
        Desaparecido.PrendasVestir[index] = prendaClone;
    }

    [RelayCommand]
    private void OnRemovePrenda(PrendaVestir prenda)
    {
        Desaparecido.PrendasVestir.Remove(prenda);
        HayPrendas = Desaparecido.PrendasVestir.Any();
    } 
        

    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        await _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}