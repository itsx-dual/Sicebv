using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.domain;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.desaparicion_forzada.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.data.OpcionesCebv;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

public partial class DesaparicionForzadaViewModel : ObservableObject
{
    private IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;

    /**
     * Constructor de la clase
     */
    public DesaparicionForzadaViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Autoridades = await CebvNetwork.GetCatalogo("autoridades");
        Particulares = await CebvNetwork.GetCatalogo("particulares");
        MetodosCaptura = await CebvNetwork.GetCatalogo("metodos-captura");
        MediosCaptura = await CebvNetwork.GetCatalogo("medios-captura");

        Reporte = _reporteService.GetReporte();

        Reporte.DesaparicionForzada ??= new DesaparicionForzada();

        // Desaparicion por autoridad
        SufrioDesaparicionForzadaOpcion =
            MappingToString(Reporte.DesaparicionForzada?.DesaparecioAutoridad ?? false);

        SufrioDesaparicionForzada = MappingToBool(SufrioDesaparicionForzadaOpcion);

        // Desaparicion por particular
        SufrioDesaparicionParticularOpcion =
            MappingToString(Reporte.DesaparicionForzada?.DesaparecioParticular ?? false);

        SufrioDesaparicionParticular = MappingToBool(SufrioDesaparicionParticularOpcion);

        // Detencion legal previa
        DetencionLegalExtorsionOpcion =
            MappingToString(Reporte.DesaparicionForzada?.DetencionPreviaExtorsion ?? false);

        DetencionLegalExtorsion = MappingToBool(DetencionLegalExtorsionOpcion);

        // Ha sido avistado previamente
        HaSidoAvistadoOpcion =
            MappingToString(Reporte.DesaparicionForzada?.HaSidoAvistado ?? false);

        HaSidoAvistado = MappingToBool(HaSidoAvistadoOpcion);

        // Delito de desaparición
        DelitoDesaparicionOpcion =
            MappingToString(Reporte.DesaparicionForzada?.DelitosDesaparicion ?? false);

        DelitoDesaparicion = MappingToBool(DelitoDesaparicionOpcion);
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    // Autoridad
    [ObservableProperty] private string _sufrioDesaparicionForzadaOpcion;
    [ObservableProperty] private bool? _sufrioDesaparicionForzada;

    partial void OnSufrioDesaparicionForzadaOpcionChanged(string value)
    {
        SufrioDesaparicionForzada = MappingToBool(value);
        if (Reporte.DesaparicionForzada is not null)
            Reporte.DesaparicionForzada.DesaparecioAutoridad = SufrioDesaparicionForzada;
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _autoridades = new();

    // Particular
    [ObservableProperty] private string _sufrioDesaparicionParticularOpcion = No;
    [ObservableProperty] private bool? _sufrioDesaparicionParticular = false;

    partial void OnSufrioDesaparicionParticularOpcionChanged(string value)
    {
        SufrioDesaparicionParticular = MappingToBool(value);
        if (Reporte.DesaparicionForzada is not null)
            Reporte.DesaparicionForzada.DesaparecioParticular = SufrioDesaparicionParticular;
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _particulares = new();

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _metodosCaptura = new();

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _mediosCaptura = new();

    // Detencion legal previa
    [ObservableProperty] private string _detencionLegalExtorsionOpcion = No;
    [ObservableProperty] private bool? _detencionLegalExtorsion = false;

    partial void OnDetencionLegalExtorsionOpcionChanged(string value)
    {
        DetencionLegalExtorsion = MappingToBool(value);
        if (Reporte.DesaparicionForzada is not null)
            Reporte.DesaparicionForzada.DetencionPreviaExtorsion = DetencionLegalExtorsion;
    }

    // Ha sido avistado previamente
    [ObservableProperty] private string _haSidoAvistadoOpcion = No;
    [ObservableProperty] private bool? _haSidoAvistado = false;

    partial void OnHaSidoAvistadoOpcionChanged(string value)
    {
        HaSidoAvistado = MappingToBool(value);
        if (Reporte.DesaparicionForzada is not null)
            Reporte.DesaparicionForzada.HaSidoAvistado = HaSidoAvistado;
    }

    // Información sobre el perpetrador
    [ObservableProperty] private PerpetradorViewModel _perpetrador = new();

    [ObservableProperty] private string _grupoPerpetradorDescripcion = String.Empty;

    [ObservableProperty] private string _delitoDesaparicionOpcion;
    [ObservableProperty] private bool? _delitoDesaparicion;

    partial void OnDelitoDesaparicionOpcionChanged(string value)
    {
        DelitoDesaparicion = MappingToBool(value);
        if (Reporte.DesaparicionForzada is not null)
            Reporte.DesaparicionForzada.DelitosDesaparicion = DelitoDesaparicion;
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}