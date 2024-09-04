using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cebv.core.modules.persona.presentation;

public partial class PersonaViewModel : ObservableObject
{
    public PersonaViewModel()
    {
        LoadCatalogs();
    }

    [ObservableProperty] private ObservableCollection<Catalogo> _sexos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _generos = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _religiones = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _lenguas = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _nacionalidades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _escolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estatusEscolaridades = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _estadosConyugales = new();
    [ObservableProperty] private ObservableCollection<Catalogo> _parentescos = new();
    
    private async void  LoadCatalogs()
    {
        Sexos = await CebvNetwork.GetCatalogo("sexos");
        Generos = await CebvNetwork.GetCatalogo("generos");
        Religiones = await CebvNetwork.GetCatalogo("religiones");
        Lenguas = await CebvNetwork.GetCatalogo("lenguas");
        Nacionalidades = await CebvNetwork.GetCatalogo("nacionalidades");
        Escolaridades = await CebvNetwork.GetCatalogo("escolaridades");
        EstatusEscolaridades = await CebvNetwork.GetCatalogo("estatus-escolaridades");
        EstadosConyugales = await CebvNetwork.GetCatalogo("estados-conyugales");
        Parentescos = await CebvNetwork.GetCatalogo("parentescos");
    }
    
    
}