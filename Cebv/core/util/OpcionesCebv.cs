namespace Cebv.core.util;

public static class OpcionesCebv
{
    public const string NoEspecifica = "No específica";
    public const string No = "No";
    public const string Si = "Sí";

    public static string GetLabel(bool? value)
    {
        return value.HasValue
            ? (value.Value ? Si : No)
            : NoEspecifica;
    }
}