using System.Collections.ObjectModel;
using Cebv.core.modules.desaparecido.data;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.datos_del_reporte.domain;

public class AddDocumentoMessage : ValueChangedMessage<ObservableCollection<DocumentoLegal>>
{
    public AddDocumentoMessage(ObservableCollection<DocumentoLegal> value) : base(value)
    {
    }
}