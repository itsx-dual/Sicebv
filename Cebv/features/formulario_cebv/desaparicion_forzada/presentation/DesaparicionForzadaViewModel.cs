using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.features.formulario_cebv.desaparicion_forzada.domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.features.formulario_cebv.desaparicion_forzada.presentation;

public partial class DesaparicionForzadaViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public DesaparicionForzadaViewModel()
    {
        CargarCatalogos();
    }
    
    /**
     * Variables de la clase
     */
    [ObservableProperty] private List<string> _opciones = OpcionesCebv.Opciones;

    // Autoridad
    [ObservableProperty] private string _sufrioDesaparicionForzadaOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sufrioDesaparicionForzada = false;

    [ObservableProperty] private ObservableCollection<Catalogo> _autoridades = new();
    [ObservableProperty] private Catalogo _autoridad = new();

    [ObservableProperty] private string _autoriddadDescripcion = string.Empty;

    // Particular
    [ObservableProperty] private string _sufrioDesaparicionParticularOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _sufrioDesaparicionParticular = false;

    [ObservableProperty] private ObservableCollection<Catalogo> _particulares = new();
    [ObservableProperty] private Catalogo _particular = new();

    [ObservableProperty] private string _particularDescripcion = string.Empty;

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _metodosCaptura = new();
    [ObservableProperty] private Catalogo _metodoCaptura = new();
    [ObservableProperty] private string _metodoCapturaDescripcion = string.Empty;

    // Metodo de captura
    [ObservableProperty] private ObservableCollection<Catalogo> _mediosCaptura = new();
    [ObservableProperty] private Catalogo _medioCaptura = new();
    [ObservableProperty] private string _medioCapturaDescripcion = string.Empty;

    // Detencion legal previa
    [ObservableProperty] private string _detencionLegalExtorsionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _detencionLegalExtorsion = false;

    [ObservableProperty] private string _detencionLegalExtorsionDescripcion = String.Empty;

    // Ha sido avistado previamente
    [ObservableProperty] private string _haSidoAvistadoOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _haSidoAvistado = false;

    [ObservableProperty] private string _haSidoAvistadoDescripcion = String.Empty;

    // Información sobre el perpetrador
    [ObservableProperty] private string _nombre = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private Catalogo _sexo = new();

    [ObservableProperty] private string _perpetradorDescripcion = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _estatusPerpetradores = new();
    [ObservableProperty] private Catalogo _estatusPerpetrador = new();

    [ObservableProperty] private string _grupoPerpetradorDescripcion = String.Empty;

    [ObservableProperty] private string _delitoDesaparicionOpcion = OpcionesCebv.No;
    [ObservableProperty] private bool? _delitoDesaparicion = false;

    [ObservableProperty] private string _delitoDesaparicionDescripcion = String.Empty;

    /**
     * Permite cargar los catalogos necesarios para el formulario
     */
    private async void CargarCatalogos()
    {
        Autoridades = await DesaparicionForzadaNetwork.GetAutoridades();
        Particulares = await DesaparicionForzadaNetwork.GetParticulares();
        MetodosCaptura = await DesaparicionForzadaNetwork.GetMetodosCaptura();
        MediosCaptura = await DesaparicionForzadaNetwork.GetMediosCaptura();
        Sexos = await DesaparicionForzadaNetwork.GetSexos();
        EstatusPerpetradores = await DesaparicionForzadaNetwork.GetEstatusPerpetradores();
    }
}