using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public partial class ExpedientePretty : ObservableObject
{
    public ExpedientePretty(
        string? tipo,
        Catalogo? parentesco,
        ReporteHechos? reporte
    )
    {
        _tipo = tipo;
        _parentesco = parentesco;
        _reporte = reporte;
    }

    public ExpedientePretty()
    {
    }

    [ObservableProperty] private string? _tipo;
    [ObservableProperty] private Catalogo? _parentesco;
    [ObservableProperty] private ReporteHechos? _reporte;
}