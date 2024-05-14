namespace Cebv.core.data;

public class OpcionCebv
{
    public string Opcion { get; set; } = "";
    public bool? Valor { get; set; }
}

public class OpcionesCebv
{
    public const string Si = "Si";
    public const string No = "No";
    public const string NoEspecifica = "No especifica";

    public static readonly List<string> Opciones = new() { Si, No, NoEspecifica };

    public static bool? MappingToBool(string value)
    {
        return value switch
        {
            Si => true,
            No => false,
            _ => null
        };
    }
}