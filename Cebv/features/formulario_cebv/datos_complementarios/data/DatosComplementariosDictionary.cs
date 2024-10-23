using Cebv.core.util.reporte.viewmodels;
using Cebv.features.formulario_cebv.datos_complementarios.presentation;

namespace Cebv.features.formulario_cebv.datos_complementarios.data;

public class DatosComplementariosDictionary
{
    public static Dictionary<string, object?> GetDatosComplementarios(Reporte reporte, DatoComplementarioViewModel datoComplementario)
    {
        return new Dictionary<string, object?>
        {
            {"Calle", reporte.DatoComplementario?.Direccion.Calle },
            {"Numero exterior", reporte.DatoComplementario?.Direccion.NumeroExterior },
            {"Numero interior", reporte.DatoComplementario?.Direccion.NumeroInterior },
            {"Colonia", reporte.DatoComplementario?.Direccion.Colonia },
            {"Codigo postal", reporte.DatoComplementario?.Direccion.CodigoPostal },
            {"Estado", datoComplementario.EstadoSelected },
            {"Municipio", datoComplementario.MunicipioSelected },
            {"Localidad", reporte.DatoComplementario?.Direccion.Asentamiento },
            {"Entre calle 1", reporte.DatoComplementario?.Direccion.Calle1 },
            {"Entre calle 2", reporte.DatoComplementario?.Direccion.Calle2 },
            {"Tramo carretero", reporte.DatoComplementario?.Direccion.TramoCarretero },
            {"Referencia", reporte.DatoComplementario?.Direccion.Referencia}
        };
    }
}