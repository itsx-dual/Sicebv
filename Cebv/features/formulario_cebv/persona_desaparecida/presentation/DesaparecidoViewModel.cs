using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.reporte.data;
using Cebv.core.modules.ubicacion.data;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using Cebv.features.formulario_cebv.presentation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

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

    public DesaparecidoViewModel()
    {
        var reporteService = App.Current.Services.GetService<IReporteService>();

        if (reporteService?.GetStatusReporteActual() == EstadoReporte.Cargado)
        {
            var reportado = reporteService.GetReporteActual().Desaparecidos.First();
            Nombre = reportado.Persona.Nombre;
            ApellidoPaterno = reportado.Persona.ApellidoPaterno;
            ApellidoMaterno = reportado.Persona.ApellidoMaterno;
        }
    }

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