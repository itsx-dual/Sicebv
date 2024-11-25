using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.components.mensaje_basico;

public partial class MensajeBasicoViewModel : ObservableObject
{
    [ObservableProperty] private string _mensaje;
}