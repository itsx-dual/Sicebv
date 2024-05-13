using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.reportante.domain;
using Cebv.features.reporte.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    [ObservableProperty] private ReporteRequest _reporte = null!;

    public ReportanteDetailViewModel()
    {
        CatalogosReportante();
    }

    private async void CatalogosReportante()
    {
        Parentescos = (ObservableCollection<Catalogo>)await ReportanteNetwork.GetParentescos();
    }

}