using System.ComponentModel.DataAnnotations;
using Cebv.core.data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.modules.contacto.data;

public class Telefono
{
    
    public string Numero { get; set; } = String.Empty;
    public Catalogo? CompaniaTelefonica { get; set; }
    public string? Observaciones { get; set; }
}
