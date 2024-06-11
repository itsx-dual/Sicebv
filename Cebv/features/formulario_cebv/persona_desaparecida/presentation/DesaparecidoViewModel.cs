using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoViewModel : ObservableObject
{
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    [ObservableProperty] private PersonaViewModel _desaparecido = new();

    /**
     * Información de la persona desaparecida.
     */
    public string NombreCompleto => $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}";

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _nombre;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoPaterno;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NombreCompleto))]
    private string? _apellidoMaterno;

    [ObservableProperty] private string _identidadResguardada = String.Empty;


    // Información de nacimiento
    [ObservableProperty] private bool _fechaAproximada;

    [ObservableProperty] private DateTime? _fechaNacimiento;
    [ObservableProperty] private ObservableCollection<Estado> _lugaresNacimientos = new();
    [ObservableProperty] private Estado _lugarNacimiento = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private Catalogo _nacionalidad = new();


    // Datos de domicilio
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    // Datos de contacto
    [ObservableProperty] private ContactoViewModel _contacto = new();

    /**
     * Datos sociemográficos de la persona desaparecida.
     */
    [ObservableProperty] private string _hablaEspanolOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _hablaEspanol;

    [ObservableProperty] private string _sabeLeerOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeLeer;

    [ObservableProperty] private string _sabeEscribirOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sabeEscribir;

    /**
     * Encabezado del formulario con el nombre completo de la persona desaparecida.
     */
    partial void OnNombreChanged(string? value)
    {
        Desaparecido.Nombre = value!;
        Encabezado();
    }

    partial void OnApellidoPaternoChanged(string? value)
    {
        Desaparecido.ApellidoPaterno = value!;
        Encabezado();
    }

    partial void OnApellidoMaternoChanged(string? value)
    {
        Desaparecido.ApellidoMaterno = value!;
        Encabezado();
    }

    private void Encabezado()
    {
        WeakReferenceMessenger.Default.Send(new NombreCompletoMessage(NombreCompleto));
    }

    partial void OnHablaEspanolOpcionChanged(string value) =>
        HablaEspanol = OpcionesCebv.MappingToBool(value);

    partial void OnSabeLeerOpcionChanged(string value) =>
        SabeLeer = OpcionesCebv.MappingToBool(value);

    partial void OnSabeEscribirOpcionChanged(string value) =>
        SabeEscribir = OpcionesCebv.MappingToBool(value);
}