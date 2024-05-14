using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.persona_desaparecida.domain;

public class GuardarBorradorMessage : ValueChangedMessage<bool>
{
    public GuardarBorradorMessage(bool value) : base(value)
    {
    }
}