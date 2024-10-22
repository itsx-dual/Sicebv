using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.data;

public partial class ExpedientePretty : ObservableObject
{
    public ExpedientePretty(
        int? id,
        string? tipo,
        Catalogo? parentesco,
        ReporteHechos? reporte
    )
    {
        _id = id;
        _tipo = tipo;
        _parentesco = parentesco;
        _reporte = reporte;
    }

    public ExpedientePretty()
    {
    }
    [ObservableProperty] private int? _id; 
    [ObservableProperty] private string? _tipo;
    [ObservableProperty] private Catalogo? _parentesco;
    [ObservableProperty] private ReporteHechos? _reporte;
}