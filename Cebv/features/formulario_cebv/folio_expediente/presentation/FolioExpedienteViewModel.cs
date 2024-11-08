using System.Collections.ObjectModel;
using System.Windows;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.sistema.data;
using Cebv.core.util;
using Cebv.core.util.enums;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.desaparicion_forzada.data;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Cebv.features.login.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using static Cebv.core.util.enums.TipoDesaparicion;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedienteViewModel : ObservableObject
{
    private readonly ISnackbarService _snackBarService =
        App.Current.Services.GetService<ISnackbarService>()!;
    
    private readonly IReporteService _reporteService =
        App.Current.Services.GetService<IReporteService>()!;

    private readonly IFormularioCebvNavigationService _navigationService =
        App.Current.Services.GetService<IFormularioCebvNavigationService>()!;

    [ObservableProperty] private Reporte _reporte;
    [ObservableProperty] private Desaparecido _desaparecido = new();

    [ObservableProperty]
    private Dictionary<string, string> _tiposDesapariciones = new() { { Unica, U }, { Multiple, M } };

    public FolioExpedienteViewModel()
    {
        InitAsync();
        
        Reporte = _reporteService.GetReporte();

        if (!Reporte.Desaparecidos.Any()) Reporte.Desaparecidos.Add(Desaparecido);
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        Reporte.ExpedienteFisico ??= new();
    }

    private async void InitAsync()
    {
        TiposReportes = await CebvNetwork.GetRoute<BasicResource>("tipos-reportes");
        Areas = await CebvNetwork.GetRoute<Catalogo>("areas?all");
        Usuarios = await CebvNetwork.GetRoute<UserAdmin>("usuario");
    }

    [ObservableProperty] private ObservableCollection<BasicResource> _tiposReportes = new();
    [ObservableProperty] private ObservableCollection<UserAdmin> _usuarios = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _areas = new();

    [ObservableProperty] private Folio _folio = new();

    [ObservableProperty] private string _errorMessage = String.Empty;
    [ObservableProperty] private Visibility _errorVisibility = Visibility.Collapsed;


    [RelayCommand]
    private async Task AsignarFolio()
    {
        await _reporteService.Sync();
        Reporte = _reporteService.GetReporte();
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;

        ErrorVisibility = Visibility.Collapsed;
        var result = await CebvNetwork.SetFolio(Reporte.Id.ToString());

        switch (result)
        {
            case Success:
                ErrorVisibility = Visibility.Visible;
                ErrorMessage = "Folio asignado correctamente";
                break;

            case Error:
                ErrorVisibility = Visibility.Visible;
                ErrorMessage = result.error;
                break;
        }

        await _reporteService.Sync();
        Reporte = _reporteService.GetReporte();
        Desaparecido = Reporte.Desaparecidos.FirstOrDefault()!;
    }

    private bool _cancelar = true;
    private async Task<bool> EnlistarCampos()
    {
        bool confirmacion = false;

        var properties = FolioExpedienteDictionary.GetFolioExpediente(Reporte, Desaparecido);
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
        if (FolioExpedienteDictionary.ValidateFolioExpediente(Reporte) == Validaciones.ExistenErrores)
        {
            _snackBarService.Show(
                "Error en los campos",
                "Por favor, revise los campos obligatorios y corrija los siguientes errores:\n " +
                "El campo Tipo de Reporte es obligatorio",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning48),
                new TimeSpan(0, 0, 10));
            return;
        }
        
        if (FolioExpedienteDictionary.ValidateFolioExpediente(Reporte) == Validaciones.HayInstanciasNulas)
        {
            _snackBarService.Show(
                "Instancias nulas",
                "Instancias nulas aun no cargadas, por favor espere a que se carguen",
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.Warning48),
                new TimeSpan(0, 0, 10));
            return;
        }
        
        if (!await EnlistarCampos())
        {
            if (!_cancelar) _navigationService.Navigate(pageType);
                
            return;
        }
        
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}