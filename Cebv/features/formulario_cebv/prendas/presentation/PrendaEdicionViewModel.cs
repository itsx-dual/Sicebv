using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendaEdicionViewModel : ObservableObject
{
    public event EventHandler<PrendaVestir>? PrendaGuardada;
    
    public int Index { get; set; }

    /**
     * Variables de la prenda a editar
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();

    [ObservableProperty] private Catalogo? _grupoPertenencia = new();

    [ObservableProperty] private ObservableCollection<Pertenencia> _pertenencias = new();
    [ObservableProperty] private Pertenencia? _pertenencia;

    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo? _color;

    [ObservableProperty] private string? _marca;
    [ObservableProperty] private string? _descripcion;


    public PrendaEdicionViewModel(PrendaVestir prenda, int index)
    {
        Index = index;
        
        // Iniciar la inicialización asíncrona
        InitializeAsync(prenda);
    }
    
    private async void InitializeAsync(PrendaVestir prenda)
    {
        await CargarCatalogosAsync();
        
        //Pertenencias = await PrendasNetwork.GetPertenencias(prenda.GrupoPertenencia!.Id);

        // Asignar propiedades después de cargar los catálogos
        GrupoPertenencia = prenda.Pertenencia?.GrupoPertenencia!;
        Pertenencia = prenda.Pertenencia;
        Color = prenda.Color;
        Marca = prenda.Marca;
        Descripcion = prenda.Descripcion;
    }
    
    private async Task CargarCatalogosAsync()
    {
        GruposPertenencias = await CebvNetwork.GetRoute<Catalogo>("grupos-pertenencias");
        Colores = await CebvNetwork.GetRoute<Catalogo>("colores");
    }

    async partial void OnGrupoPertenenciaChanged(Catalogo? value)
    {
        if (value?.Id is null) return;

        Pertenencias =
            await CebvNetwork.GetByFilter<Pertenencia>("pertenencias", "grupo_pertenencia_id", value.Id.ToString()!);
    }

    [RelayCommand]
    private void GuardarPrenda()
    {
        if (GrupoPertenencia is null ||
            Pertenencia is null ||
            Color is null ||
            Descripcion == string.Empty)
            return;

        var prenda = new PrendaVestir
        {
            Pertenencia = Pertenencia,
            Color = Color,
            Marca = Marca,
            Descripcion = Descripcion
        };

        PrendaGuardada?.Invoke(this, prenda);

        if (CloseAction != null)
            CloseAction();
    }
    

    public Action? CloseAction { get; set; }
}