using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    [ObservableProperty] private bool _mostrarDireccion;
}