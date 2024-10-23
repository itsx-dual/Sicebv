using System.Collections.ObjectModel;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;

public partial class MediaFiliacionComplementariaViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    /**
     * Constructor de la clase
     */
    public MediaFiliacionComplementariaViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Desaparecido.Persona.MediaFiliacionComplementaria ??= new();
    }

    /**
     * Peticiones a la API para cargar los catálogos
     */
    private async void InitAsync()
    {
        TiposMentones = await CebvNetwork.GetRoute<Catalogo>("tipos-mentones");
        TiposIntervenciones = await CebvNetwork.GetRoute<Catalogo>("tipos-intervenciones-quirurgicas");
        EnfermedadesPieles = await CebvNetwork.GetRoute<Catalogo>("tipos-enfermedades-piel");
    }

    /**
     * Variables de la clase
     */
    // Dientes
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    // Proyección del mentón
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMentones = new();

    // Intervenciones quirúrgicas
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposIntervenciones = new();
    [ObservableProperty] private Catalogo? _tipoIntervencion;
    [ObservableProperty] private string? _tipoIntervencionDescripcion;

    /**
     * Comandos para agregar y eliminar intervenciones quirúrgicas
     */
    [RelayCommand]
    private void OnAddIntervencionQuirurgica()
    {
        if (TipoIntervencion is null) return;

        if (Desaparecido.Persona.IntervencionesQuirurgicas.Any(i =>
                i.TipoIntervencionQuirurgica?.Id == TipoIntervencion.Id)) return;

        Desaparecido.Persona.IntervencionesQuirurgicas.Add(new()
            { TipoIntervencionQuirurgica = TipoIntervencion, Descripcion = TipoIntervencionDescripcion }
        );

        TipoIntervencion = null;
        TipoIntervencionDescripcion = null;
    }

    [RelayCommand]
    private void OnRemoveIntervencionQuirurgica(IntervencionQuirurgica intervencionQuirurgica) =>
        Desaparecido.Persona.IntervencionesQuirurgicas.Remove(intervencionQuirurgica);

    // Enfermedades en la piel
    [ObservableProperty] private ObservableCollection<Catalogo> _enfermedadesPieles = new();
    [ObservableProperty] private Catalogo? _enfermedadPiel;
    [ObservableProperty] private string? _enfermedadPielDescripcion;

    /**
     * Comandos para agregar o eliminar enfermedades en la piel
     */
    [RelayCommand]
    private void OnAddEnfermedadPiel()
    {
        if (EnfermedadPiel is null) return;

        //Se entiende @var i como un objeto de tipo EnfermedadPiel
        if (Desaparecido.Persona.EnfermedadesPiel.Any(i =>
                i.TipoEnfermedadPiel?.Id == EnfermedadPiel.Id)) return;

        Desaparecido.Persona.EnfermedadesPiel.Add(new()
            { TipoEnfermedadPiel = EnfermedadPiel, Descripcion = EnfermedadPielDescripcion }
        );

        EnfermedadPiel = null;
        EnfermedadPielDescripcion = null;
    }

    [RelayCommand]
    private void OnRemoveEnfermedadPiel(EnfermedadPiel enfermedadPiel) =>
        Desaparecido.Persona.EnfermedadesPiel.Remove(enfermedadPiel);
    
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion;

        var properties = ListEmptyElements.GetMediaFiliacionComplementaria(Desaparecido, this);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
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