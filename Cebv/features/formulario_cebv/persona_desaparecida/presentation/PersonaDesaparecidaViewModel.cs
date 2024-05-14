using Cebv.core.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    /**
     * Información de la persona desaparecida.
     */
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    [NotifyCanExecuteChangedFor(nameof(SendFullNameCommand))]
    private string? _nombre;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    [NotifyCanExecuteChangedFor(nameof(SendFullNameCommand))]
    private string? _apellidoPaterno;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    [NotifyCanExecuteChangedFor(nameof(SendFullNameCommand))]
    private string? _apellidoMaterno;

    [ObservableProperty] private string _nombreCompleto = string.Empty;

    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private List<string> _transitoEstadosUnidosList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _transitoEstadosUnidosSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _transitoEstadosUnidos;
    [ObservableProperty] private bool? _segundaPregunta;

    /**
     * Datos sociemográficos de la persona desaparecida.
     */
    [ObservableProperty] private List<string> _hablaEspanolList = OpcionesCebv.Opciones;

    [ObservableProperty] private string _hablaEspanolSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _hablaEspanol;

    [ObservableProperty] private List<string> _sabeLeerList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _sabeLeerSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeLeer;

    [ObservableProperty] private List<string> _sabeEscribirList = OpcionesCebv.Opciones;
    [ObservableProperty] private string _sabeEscribirSelected = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeEscribir;

    /**
     * Mapeo de los valores string a boolean.
     */
    partial void OnTransitoEstadosUnidosSelectedChanged(string value)
    {
        TransitoEstadosUnidos = OpcionesCebv.MappingToBool(value);
        SegundaPregunta = TransitoEstadosUnidos == true;
    }

    partial void OnNombreChanged(string? value)
    {
        NombreCompleto = $"{value} {ApellidoPaterno} {ApellidoMaterno}";
    }

    /**
     * Message for full name
     */
    [RelayCommand]
    public void SendFullName()
    {
        WeakReferenceMessenger.Default.Send(new NombreCompletoMessage(NombreCompleto));
    }
}