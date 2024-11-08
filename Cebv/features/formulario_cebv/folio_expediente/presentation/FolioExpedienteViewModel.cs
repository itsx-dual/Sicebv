using System.Collections.ObjectModel;
using System.Windows;
using Cebv.app.presentation;
using Cebv.core.domain;
using Cebv.core.modules.desaparecido.data;
using Cebv.core.modules.sistema.data;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.core.util.reporte.data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.folio_expediente.data;
using Cebv.features.login.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using static Cebv.core.util.enums.TipoDesaparicion;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedienteViewModel : ObservableObject
{
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

    [RelayCommand]
    private void GetInformeInicio()
    {
        if (Desaparecido.Id == null || Desaparecido.Id < 1) return;
        var webview = new WebView2Window($"reportes/documentos/informes-inicio/{Desaparecido.Id}", "Informe de inicio");
        webview.Show();
    }


    [RelayCommand]
    private void OnGuardarYSiguiente(Type pageType)
    {
        _reporteService.Sync();
        _navigationService.Navigate(pageType);
    }
}