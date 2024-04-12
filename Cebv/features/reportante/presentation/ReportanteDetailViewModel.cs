using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Cebv.core.data;
using Cebv.core.services;
using Cebv.features.reportante.domain;
using Cebv.features.reporte.data;
using Cebv.features.reporte.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.reportante.presentation;

public partial class ReportanteDetailViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = null!;
    [ObservableProperty] private Catalogo _parentescoSeleccionado = null!;

    [ObservableProperty] private bool _denunciaAnonima;

    [ObservableProperty] private bool _informacionConsentimiento;

    [ObservableProperty] private bool _informacionExclusivaBusqueda;

    [ObservableProperty] private bool _publicacionRegistroNacional;

    [ObservableProperty] private bool _publicacionBoletin;

    [ObservableProperty] private bool _pertenenciaColectivo;

    [ObservableProperty] private string _nombreColectivo = null!;
    [ObservableProperty] private string _informacionRelevante = null!;

    [ObservableProperty] private Reporte _reporte = null!;

    public ReportanteDetailViewModel()
    {
        CatalogosReportante();
    }

    private async void CatalogosReportante()
    {
        Parentescos = (ObservableCollection<Catalogo>)await ReportanteNetwork.GetParentescos();
    }

    [RelayCommand]
    private async Task CrearReportante()
    {
        await ReportanteNetwork.PostReportante(
            ReporteService.Reporte.Id,
            5,
            ParentescoSeleccionado.Id,
            DenunciaAnonima,
            InformacionConsentimiento,
            InformacionExclusivaBusqueda,
            PublicacionRegistroNacional,
            PublicacionBoletin,
            PertenenciaColectivo,
            NombreColectivo,
            InformacionRelevante);
    }
}