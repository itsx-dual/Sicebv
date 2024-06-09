using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.domain;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    /**
     * Datos de control
     */
    [ObservableProperty] private bool _esAnonimo;

    [ObservableProperty] private bool _puedeGuardar;

    /**
     * Información básica del reportante.
     */
    [ObservableProperty] private PersonaViewModel _reportante = new();

    // Datos de identificación de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();
    
    // Datos de contacto
    [ObservableProperty] private ContactoViewModel _contacto = new();
    
    // Domicilio
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    // Información relevante
    [ObservableProperty] private string _informacionRelevante = String.Empty;
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private Catalogo _escolaridad = new();

    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "Terminada", "En curso", "No especifica"
    };

    [ObservableProperty] private string _estatusEscolaridad = "No especifica";

    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private Catalogo _estadoConyugal = new();
    [ObservableProperty] private List<string> _pertenenciasColectivos = OpcionesCebv.Opciones;
    [ObservableProperty] private string _pertenciaColectivo = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaC = false;
    [ObservableProperty] private string _nombreColectivo = String.Empty;

    /**
     * Constructor
     */
    public ReportanteViewModel()
    {
        CargarCatalogos();
    }


    /**
     * Peticiones a la Api
     */
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetParentescos();
        Escolaridades = await PersonaNetwork.GetEscolaridades();
        EstadosConyugales = await PersonaNetwork.GetEstadosConyugales();
    }

    partial void OnPertenciaColectivoChanging(string value) =>
        PertenenciaC = OpcionesCebv.MappingToBool(value);

    /**
     * Guardar Borrador
     */
    private void ValidarBorrador()
    {
        if (Reportante.Nombre == String.Empty ||
            Reportante.ApellidoPaterno == String.Empty ||
            Reportante.ApellidoMaterno == String.Empty)
            PuedeGuardar = false;
        else PuedeGuardar = true;

        WeakReferenceMessenger.Default.Send(new GuardarBorradorMessage(PuedeGuardar));
    }

    [RelayCommand]
    public void GuardarBorrador()
    {
        ValidarBorrador();
        Console.WriteLine(Reportante.Nombre);
    }
}