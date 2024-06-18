namespace Cebv.core.data;

public enum OpcionesCebvs
{
    Si = 1,
    No = 0,
    NoEspecifica = -1
}

public class OpcionesCebv
{
    public const string Si = "Si";
    public const string No = "No";
    public const string NoEspecifica = "No especifica";

    public static readonly List<string> Opciones = new() { Si, No, NoEspecifica };

    public static readonly Dictionary<string, bool?> Ops = new()
    {
        { "Si", true },
        { "No", false },
        { "No especifica", null }
    };

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