using System.Collections.ObjectModel;
using Cebv.core.domain;
using static Cebv.core.data.OpcionesCebv;
using Cebv.core.modules.persona.data;
using Cebv.core.util;
using Cebv.core.util.enums;
using static Cebv.core.util.enums.FactorRhesus;
using static Cebv.core.util.enums.IndoleSalud;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.condiciones_vulnerabilidad.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Catalogo = Cebv.core.util.reporte.viewmodels.Catalogo;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public partial class CondicionesVulnerabilidadViewModel : ObservableObject
{
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    public CondicionesVulnerabilidadViewModel()
    {
        InitAsync();

        Reporte = _reporteService.GetReporte();
        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Desaparecido;

        Desaparecido.Persona.EnfoqueDiferenciado ??= new();
        Desaparecido.Persona.ContextoSocial ??= new();
        Desaparecido.Persona.Embarazo ??= new();
    }

    private async void InitAsync()
    {
        TiposSangre = await CebvNetwork.GetRoute<Catalogo>("tipos-sangre");
        EnfoquesDiferenciados = await CebvNetwork.GetRoute<Catalogo>("tipos-enfoque-diferenciado");
        SituacionesMigratorias = await CebvNetwork.GetRoute<Catalogo>("situaciones-migratorias");
        CondicionesSalud = await CebvNetwork.GetRoute<Catalogo>("tipos-condiciones-salud");
    }

    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    /**
     * Tipo de sangre
     */
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposSangre = [];

    [ObservableProperty]
    private ObservableCollection<string> _factoresRhesus = [Positivo, Negativo, FactorRhesus.NoEspecifica];

    /**
     * Condiciones de salud
     */
    [ObservableProperty] private ObservableCollection<string> _indolesSalud = [Fisica, Psicologica];

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

        Desaparecido.Persona.CondicionesSalud.Add(condicionSalud);

        ClearCondicionSaludForm();
    }

    [RelayCommand]
    private void OnRemoverCondicionSalud(CondicionSalud condicionSalud)
    {
        Desaparecido.Persona.CondicionesSalud.Remove(condicionSalud);
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
    [ObservableProperty] private ObservableCollection<Catalogo>? _situacionesMigratorias;

    /**
     * Enfoque diferenciado
     */
    [ObservableProperty] private ObservableCollection<Catalogo>? _enfoquesDiferenciados;

    [ObservableProperty] private Catalogo? _enfoqueDiferenciado;

    [RelayCommand]
    private void OnAgregarEnfoquePersonal()
    {
        if (EnfoqueDiferenciado is null) return;

        var enfoquePersonal = new EnfoquePersonal(null, null, EnfoqueDiferenciado);
        Desaparecido.Persona.EnfoquesPersonales.Add(enfoquePersonal);
        EnfoqueDiferenciado = null;
    }

    [RelayCommand]
    private void OnEliminarEnfoquePersonal(EnfoquePersonal enfoquePersonal)
    {
        Desaparecido.Persona.EnfoquesPersonales.Remove(enfoquePersonal);
    }

    private bool _cancelar = true;
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = CondicionesVulnerabilidadDictionary.GetCondicionesVulneravilidad(Desaparecido, this);
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