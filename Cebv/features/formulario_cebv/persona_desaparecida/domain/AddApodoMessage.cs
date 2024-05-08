using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.persona_desaparecida.domain;

public class AddApodoMessage : ValueChangedMessage<string>
{
    public AddApodoMessage(string value) : base(value)
    {
    }
}