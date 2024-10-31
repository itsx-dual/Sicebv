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

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendasViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    [ObservableProperty] private PrendasUiState _uiState;

    /**
     * Constructor de la clase
     */
    public PrendasViewModel()
    {
        LoadAsync();
        UiState = PrendasUiState.Normal;
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Desaparecido;
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();

    [ObservableProperty] private Catalogo? _grupoPertenencia;

    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();
    [ObservableProperty] private Pertenencia? _pertenencia;

    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo? _color;

    [ObservableProperty] private string? _marca;
    [ObservableProperty] private string? _descripcion;

    // Lista de prendas
    [ObservableProperty] private PrendaVestir? _prendaEditada;

    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void LoadAsync()
    {
        GruposPertenencias = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
    }

    async partial void OnGrupoPertenenciaChanged(Catalogo? value)
    {
        if (value?.Id is null) return;

        Pertenencias = await CebvNetwork
            .GetByFilter<Pertenencia>("pertenencias", "grupo_pertenencia_id", value.Id.ToString()!);
    }

    /**
     * Añadir y eliminar prendas
     */
    [RelayCommand]
    private void AddPrenda()
    {
        if (GrupoPertenencia is null ||
            Pertenencia is null ||
            Color is null ||
            Descripcion is null)
            return;

        Desaparecido.PrendasVestir.Add(new PrendaVestir
        {
            Pertenencia = Pertenencia,
            Color = Color!,
            Marca = Marca,
            Descripcion = Descripcion
        });

        LimpiarCampos();
    }

    [RelayCommand]
    private void EditPrenda(PrendaVestir prenda)
    {
        var index = Desaparecido.PrendasVestir.IndexOf(prenda);

        if (index == -1) return;

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

    private void OnPrendaGuardada(object? sender, PrendaVestir prenda)
    {
        if (sender is not PrendaEdicionViewModel vm) return;

        Desaparecido.PrendasVestir[vm.Index] = prenda;
    }

    [RelayCommand]
    private void RemovePrenda(PrendaVestir prenda) => Desaparecido.PrendasVestir.Remove(prenda);


    /**
     * Test de UI dinamica según el estado de la UI
     */
    [ObservableProperty] private bool _modoEdicion;

    partial void OnUiStateChanged(PrendasUiState value) => ModoEdicion = CambiarModo();


    private bool CambiarModo()
    {
        var modo = UiState switch
        {
            PrendasUiState.Normal => false,
            _ => true,
        };

        return modo;
    }

    [RelayCommand]
    private void OnModoEdicion(PrendaVestir prenda)
    {
        UiState = PrendasUiState.Editar;

        PrendaEditada = prenda;

        Pertenencia = prenda.Pertenencia;
        Color = prenda.Color;
        Marca = prenda.Marca;
        Descripcion = prenda.Descripcion!;
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

        var index = Desaparecido.PrendasVestir.IndexOf(PrendaEditada);
        
        if (index == -1) return;

        var prendaEditada = new PrendaVestir
        {
            Pertenencia = Pertenencia,
            Color = Color,
            Marca = Marca,
            Descripcion = Descripcion
        };

        Desaparecido.PrendasVestir[index] = prendaEditada;

        LimpiarCampos();
        PrendaEditada = null;
        UiState = PrendasUiState.Normal;
    }

    private void LimpiarCampos()
    {
        GrupoPertenencia = null;
        Pertenencia = null;
        Color = null;
        Marca = null;
        Descripcion = null;
    }
    
    private bool _cancelar = true;
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = PrendasDictionary.GetPrendas(this);   
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
    private async Task OnGuardarYSiguiente(Type pageType)
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