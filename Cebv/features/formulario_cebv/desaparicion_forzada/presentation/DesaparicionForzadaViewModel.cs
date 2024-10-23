using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util;
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

    [ObservableProperty] private Reporte _reporte = null!;
    
    private bool cancelar = true;

    /**
     * Constructor de la clase
     */
    public DesaparicionForzadaViewModel()
    {
        LoadAsync();
    }

    private async void LoadAsync()
    {
        Autoridades = await CebvNetwork.GetRoute<Catalogo>("autoridades");
        Particulares = await CebvNetwork.GetRoute<Catalogo>("particulares");
        MetodosCaptura = await CebvNetwork.GetRoute<Catalogo>("metodos-captura");
        MediosCaptura = await CebvNetwork.GetRoute<Catalogo>("medios-captura");
        Sexos = await CebvNetwork.GetRoute<Catalogo>("sexos");
        EstatusPerpetradores = await CebvNetwork.GetRoute<Catalogo>("estatus-perpetradores");

        Reporte = _reporteService.GetReporte();

        Reporte.DesaparicionForzada ??= new DesaparicionForzada();

        Reporte.Perpetradores ??= new ObservableCollection<Perpetrador>();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private Dictionary<string, bool?> _opcionesCebv = Opciones;

    // Autoridad
    [ObservableProperty] private string _sufrioDesaparicionForzada = No;

    [ObservableProperty] private ObservableCollection<Catalogo> _autoridades = new();

    // Particular
    [ObservableProperty] private string _sufrioDesaparicionParticular = No;

    [ObservableProperty] private ObservableCollection<Catalogo> _particulares = new();

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _metodosCaptura = new();

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _mediosCaptura = new();

    // Detencion legal previa
    [ObservableProperty] private string _detencionLegalExtorsion = No;

    // Ha sido avistado previamente
    [ObservableProperty] private string _haSidoAvistado = No;
   
    /**
     * Logica para perpetradores
     */
    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private Catalogo? _sexo;

    [ObservableProperty] private ObservableCollection<Catalogo> _estatusPerpetradores = new();
    [ObservableProperty] private Catalogo? _estatusPerpetrador;

    [ObservableProperty] private string _perpetradorDescripcion = String.Empty;

    // Limpiar la selección de la pantalla
    private void ClearForm()
    {
        Nombre = String.Empty;
        PerpetradorDescripcion = String.Empty;
        Sexo = null;
        EstatusPerpetrador = null;
    }

    // Añadir un perpetrador
    [RelayCommand]
    private void AddPerpetrador()
    {
        if (string.IsNullOrEmpty(Nombre)) return;

        var perpetrador = new Perpetrador
        {
            Nombre = Nombre,
            Descripcion = PerpetradorDescripcion,
            Sexo = Sexo,
            EstatusPerpetrador = EstatusPerpetrador
        };

        Reporte.Perpetradores?.Add(perpetrador);

        ClearForm();
    }

    // Eliminar perpetrador
    [RelayCommand]
    private void RemovePerpetrador(Perpetrador perpetrador)
    {
        Reporte.Perpetradores?.Remove(perpetrador);
    }
    
    /**
     * Información relevante
     */
    [ObservableProperty] private string _delitoDesaparicion = No;

    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = DesaparicionForzadaDictionary.GetDesaparicionForzada(Reporte, this);
        var emptyElements = ListEmptyElements.GetEmptyElements(properties);
        
        if (emptyElements.Count > 0)
        {
            var dialogo = new ShowDialog();

            // Esperar a que se muestre el ContentDialog
            await dialogo.ShowContentDialogCommand.ExecuteAsync(emptyElements);
            
            if (dialogo.Confirmacion == "Guardar")
            {
                confirmacion = true;
            }
            else if (dialogo.Confirmacion == "No guardar")
            {
                cancelar = false;
                return cancelar;
            }
        }
        else confirmacion = true;

        return confirmacion;
    }
    
    [RelayCommand]
    private async Task OnGuardarYSiguiente(Type pageType)
    {
        if (!await EnlistarCampos())
        {
            if (!cancelar)
            {
                _navigationService.Navigate(pageType);
                return;
            }
            
            return;
        }
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}