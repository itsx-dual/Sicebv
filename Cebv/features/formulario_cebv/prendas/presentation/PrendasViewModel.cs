using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.prendas.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public enum PrendasUiState
{
    Normal,
    Editar
}

public partial class PrendasViewModel : ObservableObject
{
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;
    
    private static IDashboardNavigationService _navigationService =
        App.Current.Services.GetService<IDashboardNavigationService>()!;
    
    [ObservableProperty] private Reporte _reporte;
    
    [ObservableProperty] private PrendasUiState _uiState;
    [ObservableProperty] private Desaparecido _desaparecido;

    /**
     * Constructor de la clase
     */
    public PrendasViewModel()
    {
        LoadAsync();
        UiState = PrendasUiState.Normal;
        Reporte = _reporteService.GetReporte();
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();
    [ObservableProperty] private Catalogo? _grupoPertenencia;

    [ObservableProperty] private ObservableCollection<Catalogo> _pertenencias = new();
    [ObservableProperty] private Catalogo? _pertenencia;

    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo? _color;

    [ObservableProperty] private string? _marca;
    [ObservableProperty] private string _descripcion = string.Empty;

    // Lista de prendas
    [ObservableProperty] private ObservableCollection<Prenda> _prendas = new();
    [ObservableProperty] private Prenda? _prendaEditada;

    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void LoadAsync()
    {
        GruposPertenencias = await CebvNetwork.GetCatalogo("grupos-pertenencias");
        Colores = await CebvNetwork.GetCatalogo("colores");
    }

    async partial void OnGrupoPertenenciaChanged(Catalogo? value)
    {
        if (value?.Id is null) return;
        
        Pertenencias = await CebvNetwork.GetByFilter<Catalogo>("pertenencias", "grupo_pertenencia_id", value.Id.ToString()!);
    }

    /**
     * Añadir y eliminar prendas
     */
    [RelayCommand]
    private void AddPrenda()
    {
        if (Pertenencia is null) return;

        Prendas.Add(new Prenda
        {
            GrupoPertenencia = GrupoPertenencia,
            Pertenencia = Pertenencia,
            Color = Color!,
            Marca = Marca,
            Descripcion = Descripcion
        });

        LimpiarCampos();
    }

    [RelayCommand]
    private void EditPrenda(Prenda prenda)
    {
        var index = Prendas.IndexOf(prenda);
        var prendaEdicionViewModel = new PrendaEdicionViewModel(prenda, index);

        // Suscribirse al evento de guardado
        prendaEdicionViewModel.PrendaGuardada += OnPrendaGuardada;

        // Abrir la ventana de edición de la prenda
        var dialog = new PrendaEdicion { DataContext = prendaEdicionViewModel };

        // Configurar la acción de cierre para la ventana de edición
        if (dialog.DataContext is PrendaEdicionViewModel vm)
        {
            vm.CloseAction = () => dialog.Close();
        }

        dialog.ShowDialog();
    }

    private void OnPrendaGuardada(object? sender, Prenda prenda)
    {
        if (sender is not PrendaEdicionViewModel vm) return;

        Prendas[vm.Index] = prenda;
    }

    [RelayCommand]
    private void RemovePrenda(Prenda prenda) =>
        Prendas.Remove(prenda);


    /**
     * Test de UI dinamica según el estado de la UI
     */
    [ObservableProperty] private bool _modoEdicion;

    partial void OnUiStateChanged(PrendasUiState value) => ModoEdicion = CambiarModo();


    private bool CambiarModo()
    {
        bool modo = UiState switch
        {
            PrendasUiState.Normal => false,
            _ => true,
        };

        return modo;
    }

    [RelayCommand]
    private void OnModoEdicion(Prenda prenda)
    {
        UiState = PrendasUiState.Editar;
        
        PrendaEditada = prenda;
        
        Pertenencia = prenda.Pertenencia;
        Color = prenda.Color;
        Marca = prenda.Marca;
        Descripcion = prenda.Descripcion;
    }
    
    [RelayCommand]
    private void OnCancelarEdicion()
    {
        LimpiarCampos();
        PrendaEditada = null;
        UiState = PrendasUiState.Normal;
    }
    
    [RelayCommand]
    private void OnConfirmarEdicion()
    {
        if (PrendaEditada is null) return;
        
        var index = Prendas.IndexOf(PrendaEditada);
        
        var prendaEditada = new Prenda
        {
            GrupoPertenencia = GrupoPertenencia,
            Pertenencia = Pertenencia!,
            Color = Color!,
            Marca = Marca,
            Descripcion = Descripcion
        };
        
        Prendas[index] = prendaEditada;
        
        LimpiarCampos();
        PrendaEditada = null;
        UiState = PrendasUiState.Normal;
        
    }
    
    private void LimpiarCampos()
    {
        Pertenencia = null;
        Color = null;
        Marca = null;
        Descripcion = string.Empty;
    }
    
    
    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}