using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.data;

[JsonObject(MemberSerialization.OptIn)]
public partial class Vehiculo : ObservableObject
{
    [JsonConstructor]
    public Vehiculo(
        int? id,
        int? reporteId,
        Catalogo? relacionVehiculo, //
        Catalogo? tipoVehiculo, // 3
        Catalogo? usoVehiculo, // 3
        Catalogo? marcaVehiculo, // 2
        Catalogo? color, // 2
        string? submarca, // 1
        string? placa, // footer
        string? modelo, // 1
        string? numeroSerie,
        string? numeroMotor,
        string? numeroPermiso, //
        string? descripcion, //
        bool localizado //
    )
    {
        Id = id;
        ReporteId = reporteId;
        RelacionVehiculo = relacionVehiculo;
        TipoVehiculo = tipoVehiculo;
        UsoVehiculo = usoVehiculo;
        MarcaVehiculo = marcaVehiculo;
        Color = color;
        Submarca = submarca;
        Placa = placa;
        Modelo = modelo;
        NumeroSerie = numeroSerie;
        NumeroMotor = numeroMotor;
        NumeroPermiso = numeroPermiso;
        Descripcion = descripcion;
        Localizado = localizado;
    }

    public Vehiculo()
    {
    }

    [ObservableProperty, JsonProperty("id")]
    private int? _id;

    [ObservableProperty, JsonProperty("reporte_id")]
    private int? _reporteId;

    [ObservableProperty, JsonProperty("relacion_vehiculo")]
    private Catalogo? _relacionVehiculo;

    [ObservableProperty, JsonProperty("tipo_vehiculo")]
    private Catalogo? _tipoVehiculo;

    [ObservableProperty, JsonProperty("uso_vehiculo")]
    private Catalogo? _usoVehiculo;

    [ObservableProperty, JsonProperty("marca_vehiculo")]
    private Catalogo? _marcaVehiculo;

    [ObservableProperty, JsonProperty("color")]
    private Catalogo? _color;

    [ObservableProperty, JsonProperty("submarca")]
    private string? _submarca;

    [ObservableProperty, JsonProperty("placa")]
    private string? _placa;

    [ObservableProperty, JsonProperty("modelo")]
    private string? _modelo;

    [ObservableProperty, JsonProperty("numero_serie")]
    private string? _numeroSerie;

    [ObservableProperty, JsonProperty("numero_motor")]
    private string? _numeroMotor;

    [ObservableProperty, JsonProperty("numero_permiso")]
    private string? _numeroPermiso;

    [ObservableProperty, JsonProperty("descripcion")]
    private string? _descripcion;

    [ObservableProperty, JsonProperty("localizado")]
    private bool _localizado;
}