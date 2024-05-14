using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.persona_desaparecida.domain;

public class NombreCompletoMessage : ValueChangedMessage<string>
{
    public NombreCompletoMessage(string value) : base(value)
    {
    }
}