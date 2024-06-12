using System.Collections.ObjectModel;
using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionViewModel: ObservableObject
{
    /**
     * Constructor de la clase
     */
    public MediaFiliacionViewModel()
    {
        
    }
    
    /**
     * Variables de la clase
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _complexiones = new();
    [ObservableProperty] private Catalogo _complexion = new();


    /**
     * Peticiones a la API para obtener los catalogos
     */
    private async void CargarCatalogos()
    {
     
    }
    
}