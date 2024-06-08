using Cebv.core.modules.reportante.data;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Cebv.features.formulario_cebv.reportante.domain;

public class AddReportanteMessage : ValueChangedMessage<ReportanteRequest>
{
    public AddReportanteMessage(ReportanteRequest value) : base(value)
    {
    }
}