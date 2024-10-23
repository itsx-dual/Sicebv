namespace Cebv.features.formulario_cebv.vehiculos_involucrados.data;

public class VehiculosInvolucradosDictionary
{
    public static Dictionary<string, object?> GetVehiculosInvolucrados(Vehiculo vehiculo)
    {
        return new Dictionary<string, object?>
        {
            {"Relacion con la persona desaparecida o no localizada", vehiculo.RelacionVehiculo},
            {"Marca", vehiculo.MarcaVehiculo},
            {"Submarca", vehiculo.Submarca},
            {"Color", vehiculo.Color},
            {"Placas", vehiculo.Placa},
            {"Modelo", vehiculo.Modelo},
            {"Numero Serie", vehiculo.NumeroSerie},
            {"Numero Motor", vehiculo.NumeroMotor},
            {"Numero permiso", vehiculo.NumeroPermiso},
            {"Tipo de vehiculo", vehiculo.TipoVehiculo},
            {"Uso vehiculo", vehiculo.UsoVehiculo},
            {"Señas particulares", vehiculo.Descripcion},
            {"Estatus vehiculo", vehiculo.Localizado}
        };
    }
}