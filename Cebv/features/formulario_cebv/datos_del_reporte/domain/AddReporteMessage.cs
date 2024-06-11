using Cebv.core.modules.reporte.data;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.datos_del_reporte.domain;

public class AddReporteMessage : ValueChangedMessage<ReporteRequest>
{
    public AddReporteMessage(ReporteRequest value) : base(value)
    {
    }
}