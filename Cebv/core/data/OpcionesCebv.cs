namespace Cebv.core.data;

public static class OpcionesCebv
{
    public const string Si = "Si";
    public const string No = "No";
    public const string NoEspecifica = "No especifica";

    public static readonly Dictionary<string, bool?> Opciones = new()
    {
        { Si, true },
        { No, false },
        { NoEspecifica, null }
    };
}