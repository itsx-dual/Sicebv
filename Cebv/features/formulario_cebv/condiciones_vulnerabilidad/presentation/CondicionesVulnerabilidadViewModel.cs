using System.Collections.ObjectModel;
using Cebv.core.domain;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.persona.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public partial class CondicionesVulnerabilidadViewModel : ObservableObject
{
    private static IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;

    public CondicionesVulnerabilidadViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        TiposSangre = await CebvNetwork.GetRoute<Catalogo>("tipos-sangre");
        EnfoquesDiferenciados = await CebvNetwork.GetRoute<Catalogo>("tipos-enfoque-diferenciado");
        SituacionesMigratorias = await CebvNetwork.GetRoute<Catalogo>("situaciones-migratorias");
        CondicionesSalud = await CebvNetwork.GetRoute<Catalogo>("tipos-condiciones-salud");

        Reporte = _reporteService.GetReporte();

        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.EnfoqueDiferenciado ??= new EnfoqueDiferenciado();
        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.ContextoSocial ??= new ContextoSocial();
    }

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    /**
     * Tipo de sangre
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposSangre = new();

    [ObservableProperty] private ObservableCollection<string> _factoresRhesus = new() { "Positivo", "Negativo" };

    /**
     * Condiciones de salud
     */
    [ObservableProperty] private ObservableCollection<string> _indolesSalud = new() { "Fisica", "Psicologica" };

    [ObservableProperty] private ObservableCollection<Catalogo>? _condicionesSalud;

    [ObservableProperty] private Catalogo? _condicionSalud;

    [ObservableProperty] private string? _indoleSalud;
    [ObservableProperty] private string? _tratamiento;
    [ObservableProperty] private string? _observaciones;

    [RelayCommand]
    private void OnAgregarCondicionSalud()
    {
        if (CondicionSalud is null ||
            IndoleSalud is null ||
            Tratamiento is null ||
            Observaciones is null) return;
        
        var condicionSalud = new CondicionSalud(null, null, CondicionSalud, IndoleSalud, Tratamiento, Observaciones);

        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.CondicionesSalud.Add(condicionSalud);

        ClearCondicionSaludForm();
    }

    [RelayCommand]
    private void OnRemoverCondicionSalud(CondicionSalud condicionSalud)
    {
        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.CondicionesSalud.Remove(condicionSalud);
    }

    private void ClearCondicionSaludForm()
    {
        CondicionSalud = null;
        IndoleSalud = null;
        Tratamiento = null;
        Observaciones = null;
    }

    /**
     * Información migratoria
     */
    [ObservableProperty] private string _transitoEstadosUnidos = No;

    [ObservableProperty] private ObservableCollection<Catalogo>? _situacionesMigratorias;

    /**
     * Enfoque diferenciado
     */
    [ObservableProperty] private string _pertenenciaGrupal = No;

    [ObservableProperty] private ObservableCollection<Catalogo>? _enfoquesDiferenciados;

    [ObservableProperty] private Catalogo? _enfoqueDiferenciado;

    [RelayCommand]
    private void OnAgregarEnfoquePersonal()
    {
        if (EnfoqueDiferenciado is null) return;

        var enfoquePersonal = new EnfoquePersonal(null, null, EnfoqueDiferenciado);
        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.EnfoquesPersonales.Add(enfoquePersonal);
        EnfoqueDiferenciado = null;
    }

    [RelayCommand]
    private void OnEliminarEnfoquePersonal(EnfoquePersonal enfoquePersonal)
    {
        Reporte.Desaparecidos.FirstOrDefault()!.Persona!.EnfoquesPersonales.Remove(enfoquePersonal);
    }

    [RelayCommand]
    private void OnGuardarYSiguente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}