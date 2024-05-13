using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.condiciones_vulnerabilidad.presentation;

public class CondicionesSalud
{
    public string condicion { get; set; }
    public string tratamiento { get; set; }
    public string naturaleza { get; set; }
}
public partial class CondicionesVulnerabilidadViewModel : ObservableObject
{
    [ObservableProperty] 
    private ObservableCollection<CondicionesSalud> _condicionesSalud;
    
    [ObservableProperty] 
    private ObservableCollection<string> _enfoquesDiferenciadosCatalogo;
    [ObservableProperty] 
    private ObservableCollection<string> _enfoquesDiferenciadosSeleccionados;
    [ObservableProperty] 
    private string _enfoqueDiferenciado;

    [ObservableProperty]
    private string _caracteristicaRiesgo;
    [ObservableProperty]
    private ObservableCollection<string> _caracteristicasRiesgo;
    
    [ObservableProperty]
    private string _informacionRelevante;
    [ObservableProperty]
    private ObservableCollection<string> _informacionesRelevantes;
    
    [ObservableProperty]
    private ObservableCollection<string> ;
    

    public CondicionesVulnerabilidadViewModel()
    {
        CondicionesSalud = new ObservableCollection<CondicionesSalud>();

        EnfoquesDiferenciadosCatalogo = new ObservableCollection<string>();
        EnfoquesDiferenciadosSeleccionados = new ObservableCollection<string>();
        CaracteristicasRiesgo = new ObservableCollection<string>();
        InformacionesRelevantes = new ObservableCollection<string>();
        
        EnfoquesDiferenciadosCatalogo.Add("CONDUCTOR DE TRANSPORTE PUBLICO");
        EnfoquesDiferenciadosCatalogo.Add("DEFENSORA DE DERECHOS HUMANOS");
        EnfoquesDiferenciadosCatalogo.Add("DISCAPACIDAD FISICA");
        EnfoquesDiferenciadosCatalogo.Add("DISCAPACIDAD INTELECTUAL");
        EnfoquesDiferenciadosCatalogo.Add("DISCAPACIDAD MENTAL");
        EnfoquesDiferenciadosCatalogo.Add("DISCAPACIDAD SENSORIAL");
        EnfoquesDiferenciadosCatalogo.Add("ES PERSONAL DE SEGURIDAD PUBLICA O PRIVADA"); 
        EnfoquesDiferenciadosCatalogo.Add("HABLA IDIOMA O LENGUA INDIGENA");
        EnfoquesDiferenciadosCatalogo.Add("PERIODISTA");
        EnfoquesDiferenciadosCatalogo.Add("PERSONA EXTRANJERA EN MEXICO");
        EnfoquesDiferenciadosCatalogo.Add("PERTENECE A ALGUN SINDICATO");
        EnfoquesDiferenciadosCatalogo.Add("PERTENECE A ALGUNA ONG");
        EnfoquesDiferenciadosCatalogo.Add("PERTENECE A LA COMUNIDAD LGBTTTIQ");
        EnfoquesDiferenciadosCatalogo.Add("PERTENECE PUEBLO O COMUNIDAD INDIGENA");
        EnfoquesDiferenciadosCatalogo.Add("SERVIDOR PUBLICO");
        EnfoquesDiferenciadosCatalogo.Add("NO ESPECIFICA");

        EnfoqueDiferenciado = EnfoquesDiferenciadosCatalogo.Last();
        CaracteristicaRiesgo = string.Empty;
        InformacionRelevante = string.Empty;
    }

    [RelayCommand]
    public void AddEnfoqueDiferenciado()
    {
        EnfoquesDiferenciadosSeleccionados.Add(EnfoqueDiferenciado);
    }
    
    [RelayCommand]
    public void RemoveEnfoqueDiferenciado(string elemento)
    {
        Console.WriteLine($"Item para borrar: {elemento}");
        EnfoquesDiferenciadosSeleccionados.Remove(elemento);
    }

    [RelayCommand]
    public void AddCaracteristicaRiesgo()
    {
        CaracteristicaRiesgo = CaracteristicaRiesgo.Trim();
        if (CaracteristicaRiesgo == string.Empty || CaracteristicaRiesgo.Length <= 0) return;
        CaracteristicasRiesgo.Add(CaracteristicaRiesgo);
        CaracteristicaRiesgo = string.Empty;
    }

    [RelayCommand]
    public void RemoveCaracteristicaRiesgo(string elemento)
    {
        CaracteristicasRiesgo.Remove(elemento);
    }
    
    [RelayCommand]
    public void AddInformacionRelevante()
    {
        InformacionRelevante = InformacionRelevante.Trim();
        if (InformacionRelevante == string.Empty || InformacionRelevante.Length <= 0) return;
        InformacionesRelevantes.Add(InformacionRelevante);
        InformacionRelevante = string.Empty;
    }

    [RelayCommand]
    public void RemoveInformacionRelevante(string elemento)
    {
        InformacionesRelevantes.Remove(elemento);
    }
}