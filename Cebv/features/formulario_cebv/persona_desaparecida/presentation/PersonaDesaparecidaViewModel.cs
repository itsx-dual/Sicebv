using Cebv.core.util;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    [ObservableProperty] private bool _mostrarDireccion;

    // Datos sociodemográficos.
    [ObservableProperty] private bool? _hablaEspanol = false;
    [ObservableProperty] private string _hablaEspanolLabel = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeLeer = false;
    [ObservableProperty] private string _sabeLeerLabel = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeEscribir = false;
    [ObservableProperty] private string _sabeEscribirLabel = OpcionesCebv.No;

    /// <summary>
    /// Manejador de etiquetas según los valores tertiarios del checkbox (True, False, Null).
    /// Esto es un elemento de la UI que hace explícita la selección del usuario.
    /// </summary>
    partial void OnHablaEspanolChanged(bool? value) =>
        HablaEspanolLabel = OpcionesCebv.GetLabel(value);

    partial void OnSabeLeerChanged(bool? value) =>
        SabeLeerLabel = OpcionesCebv.GetLabel(value);

    partial void OnSabeEscribirChanged(bool? value) =>
        SabeEscribirLabel = OpcionesCebv.GetLabel(value);
}