using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.prendas.data;
using Cebv.features.formulario_cebv.prendas.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendaEdicionViewModel : ObservableObject
{
    public event EventHandler<Prenda>? PrendaGuardada;
    
    public int Index { get; set; }

    /**
     * Variables de la prenda a editar
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _gruposPertenencias = new();

    [ObservableProperty] private Catalogo _grupoPertenencia = new();

    [ObservableProperty] private ObservableCollection<Catalogo> _pertenencias = new();
    [ObservableProperty] private Catalogo? _pertenencia;

    [ObservableProperty] private ObservableCollection<Catalogo> _colores = new();
    [ObservableProperty] private Catalogo? _color;

    [ObservableProperty] private string? _marca;
    [ObservableProperty] private string? _descripcion;


    public PrendaEdicionViewModel(Prenda prenda, int index)
    {
        Index = index;
        
        // Iniciar la inicialización asíncrona
        InitializeAsync(prenda);
    }
    
    private async void InitializeAsync(Prenda prenda)
    {
        await CargarCatalogosAsync();
        
        //Pertenencias = await PrendasNetwork.GetPertenencias(prenda.GrupoPertenencia!.Id);

        // Asignar propiedades después de cargar los catálogos
        GrupoPertenencia = prenda.GrupoPertenencia!;
        Pertenencia = prenda.Pertenencia;
        Color = prenda.Color;
        Marca = prenda.Marca;
        Descripcion = prenda.Descripcion;
    }
    
    private async Task CargarCatalogosAsync()
    {
        //GruposPertenencias = await PrendasNetwork.GetGruposPertenencias();
        //Colores = await PrendasNetwork.GetColores();
    }

    //async partial void OnGrupoPertenenciaChanged(Catalogo value) =>
    //    Pertenencias = await PrendasNetwork.GetPertenencias(value.Id);

    [RelayCommand]
    private void GuardarPrenda()
    {
        if (Pertenencia is null) return;

        var prenda = new Prenda
        {
            GrupoPertenencia = GrupoPertenencia,
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