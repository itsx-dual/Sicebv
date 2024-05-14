using Cebv.core.data;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaViewModel : ObservableObject
{
    [ObservableProperty] private bool _puedeGuardar;

    /**
     * Información de la persona desaparecida.
     */
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;

    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

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
        //Emcabezado();
        //GuardarBorrador();
    }
    
    
    partial void OnApellidoPaternoChanged(string? value)
    {
       // Emcabezado();
        //GuardarBorrador();
    }
    
    partial void OnApellidoMaternoChanged(string? value)
    {
        //Emcabezado();
        //GuardarBorrador();
    }
    public void Emcabezado()
    {
        WeakReferenceMessenger.Default.Send(new NombreCompletoMessage(NombreCompleto));
    }

    public void GuardarBorrador()
    {
        if (Nombre != null && ApellidoPaterno != null && ApellidoMaterno != null) PuedeGuardar = true;
        else PuedeGuardar = false;
        
        WeakReferenceMessenger.Default.Send(new GuardarBorradorMessage(PuedeGuardar));
    }
}