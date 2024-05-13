using Cebv.features.formulario_cebv.datos_del_reporte.domain;
using Cebv.features.formulario_cebv.presentation.data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.presentation;

public class FormularioCebvViewModel : IRecipient<AddReporteMessage>
{
    private readonly IFormularioService _formularioService;

    public FormularioCebvViewModel(IFormularioService formularioService)
    {
        _formularioService = formularioService;
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void Receive(AddReporteMessage message)
    {
        _formularioService.Reporte = message.Value;
    }
}