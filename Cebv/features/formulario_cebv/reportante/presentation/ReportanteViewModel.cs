using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.presentation;
using Cebv.core.modules.persona.presentation;
using Cebv.core.modules.reportante.domain;
using Cebv.core.modules.ubicacion.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportanteViewModel : ObservableObject
{
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    /**
    * Constructor
    */
    public ReportanteViewModel()
    {
        CargarCatalogos();
    }


    /**
     * Datos de control
     */
    [ObservableProperty] private bool _esAnonimo;

    [ObservableProperty] private bool _puedeGuardar;

    [ObservableProperty] private PersonaViewModel _reportante = App.Current.Services.GetService<PersonaViewModel>()!;


    // Datos de identificación de la persona
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    [ObservableProperty] private Catalogo _parentesco = new();

    // Datos de contacto
    [ObservableProperty] private ContactoViewModel _contacto = new();

    // Domicilio
    [ObservableProperty] private UbicacionViewModel _ubicacion = new();

    // Información relevante
    [ObservableProperty] private string _informacionRelevante = String.Empty;

    [ObservableProperty] private List<string> _estatusEscolaridades = new()
    {
        "Terminada", "En curso", "No especifica"
    };

    [ObservableProperty] private string _estatusEscolaridad = "No especifica";
    
    [ObservableProperty] private string _pertenciaColectivo = OpcionesCebv.No;
    [ObservableProperty] private bool? _pertenenciaC = false;
    [ObservableProperty] private string _nombreColectivo = String.Empty;
    
    // Amezanas
    [ObservableProperty] private string _victimaExtorsionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _victimaExtorsion;

    [ObservableProperty] private string _recibioAmenazaOpcion= OpcionesCebv.No;
    [ObservableProperty] private bool? _recibioAmenaza;
    
    // Busquedas pasadas
    [ObservableProperty] private string _participoBusquedaOpcion= OpcionesCebv.No;
    [ObservableProperty] private bool? _participoBusqueda;
    
    [ObservableProperty] private ObservableCollection<Catalogo> _colectivos = new();
    [ObservableProperty] private Catalogo _colectivo = new();

    /**
     * Peticiones a la Api
     */
    private async void CargarCatalogos()
    {
        Parentescos = await ReportanteNetwork.GetParentescos();
        Colectivos = await ReportanteNetwork.GetColectivos();
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