using Cebv.core.data;

namespace Cebv.core.modules.contacto.data;

public class TelefonoMovil
{
    public string Telefono { get; set; } = String.Empty;
    public Catalogo? CompaniaTelefonica { get; set; }
    public string? Observaciones { get; set; }
}
