using Cebv.core.data;

namespace Cebv.core.modules.contacto.data;

public class RedSocial
{
    public Catalogo TipoRedSocial { get; set; } = new();
    public string Usuario { get; set; } = string.Empty;
    public string? Observaciones { get; set; }
}