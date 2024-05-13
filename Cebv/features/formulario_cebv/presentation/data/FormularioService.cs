using Cebv.features.reporte.data;

namespace Cebv.features.formulario_cebv.presentation.data;

public interface IFormularioService
{
    ReporteRequest Reporte { get; set; }
}

public class FormularioService : IFormularioService
{
    public ReporteRequest Reporte { get; set; }
}