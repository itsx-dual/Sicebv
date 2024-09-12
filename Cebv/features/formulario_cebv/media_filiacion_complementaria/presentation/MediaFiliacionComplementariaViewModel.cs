using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.modules.persona.data;
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
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;

    /**
     * Constructor de la clase
     */
    public MediaFiliacionComplementariaViewModel()
    {
        LoadAsync();
    }

    /**
     * Peticiones a la API para cargar los catálogos
     */
    private async void LoadAsync()
    {
        TiposMentones = await CebvNetwork.GetCatalogo("tipos-mentones");
        TiposIntervenciones = await CebvNetwork.GetCatalogo("tipos-intervenciones-quirurgicas");
        EnfermedadesPieles = await CebvNetwork.GetCatalogo("tipos-enfermedades-piel");

        Reporte = _reporteService.GetReporte();

        Reporte.Desaparecidos.FirstOrDefault()!.Persona.MediaFiliacionComplementaria ??= new();

        DienteOpcion =
            OpcionesCebv.MappingToString(Reporte.Desaparecidos[0].Persona.MediaFiliacionComplementaria!
                .TieneAusenciaDental);
        TratamientoDentalOpcion =
            OpcionesCebv.MappingToString(Reporte.Desaparecidos[0].Persona.MediaFiliacionComplementaria!
                .TieneTratamientoDental);
    }

    /**
     * Variables de la clase
     */
    // Dientes
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private string _dienteOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _diente = false;

    [ObservableProperty] private string _tratamientoDentalOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _tratamientoDental = false;

    // Proyección del mentón
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposMentones = new();

    // Intervenciones quirúrgicas
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposIntervenciones = new();
    [ObservableProperty] private Catalogo? _tipoIntervencion;
    [ObservableProperty] private string _tipoIntervencionDescripcion = string.Empty;

    /**
     * Comandos para agregar y eliminar intervenciones quirúrgicas
     */
    [RelayCommand]
    private void OnAddIntervencionQuirurgica()
    {
        if (TipoIntervencion is null) return;

        //Se entiende @var i como un objeto de tipo IntervencionQuirurgica
        if (Reporte.Desaparecidos[0].Persona.IntervencionesQuirurgicas.Any(i =>
                i.TipoIntervencionQuirurgica?.Id == TipoIntervencion.Id)) return;

        Reporte.Desaparecidos[0].Persona.IntervencionesQuirurgicas.Add(
            new()
            {
                TipoIntervencionQuirurgica = TipoIntervencion,
                Descripcion = TipoIntervencionDescripcion
            }
        );

        TipoIntervencion = null;
        TipoIntervencionDescripcion = string.Empty;
    }

    [RelayCommand]
    private void OnRemoveIntervencionQuirurgica(IntervencionQuirurgica intervencionQuirurgica)
    {
        Reporte.Desaparecidos[0].Persona.IntervencionesQuirurgicas.Remove(intervencionQuirurgica);
    }

    // Enfermedades en la piel
    [ObservableProperty] private ObservableCollection<Catalogo> _enfermedadesPieles = new();
    [ObservableProperty] private Catalogo? _enfermedadPiel;
    [ObservableProperty] private string _enfermedadPielDescripcion = string.Empty;


    /**
     * Comandos para agregar o eliminar enfermedades en la piel
     */
    [RelayCommand]
    private void OnAddEnfermedadPiel()
    {
        if (EnfermedadPiel is null) return;

        //Se entiende @var i como un objeto de tipo EnfermedadPiel
        if (Reporte.Desaparecidos[0].Persona.EnfermedadesPiel.Any(i =>
                i.TipoEnfermedadPiel?.Id == EnfermedadPiel.Id)) return;

        Reporte.Desaparecidos[0].Persona.EnfermedadesPiel.Add(
            new()
            {
                TipoEnfermedadPiel = EnfermedadPiel,
                Descripcion = EnfermedadPielDescripcion
            }
        );

        EnfermedadPiel = null;
        EnfermedadPielDescripcion = string.Empty;
    }

    [RelayCommand]
    private void OnRemoveEnfermedadPiel(EnfermedadPiel enfermedadPiel)
    {
        Reporte.Desaparecidos[0].Persona.EnfermedadesPiel.Remove(enfermedadPiel);
    }

    partial void OnDienteOpcionChanged(string value)
    {
        Diente = OpcionesCebv.MappingToBool(value);

        Reporte.Desaparecidos[0].Persona.MediaFiliacionComplementaria!.TieneAusenciaDental = Diente;
    }

    partial void OnTratamientoDentalOpcionChanged(string value)
    {
        TratamientoDental = OpcionesCebv.MappingToBool(value);

        Reporte.Desaparecidos[0].Persona.MediaFiliacionComplementaria!.TieneTratamientoDental = TratamientoDental;
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}