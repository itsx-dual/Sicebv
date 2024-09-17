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
        Sexos = await CebvNetwork.GetRoute<Catalogo>("sexos");
        Generos = await CebvNetwork.GetRoute<Catalogo>("generos");
        Religiones = await CebvNetwork.GetRoute<Catalogo>("religiones");
        Lenguas = await CebvNetwork.GetRoute<Catalogo>("lenguas");
        Nacionalidades = await CebvNetwork.GetRoute<Catalogo>("nacionalidades");
        Escolaridades = await CebvNetwork.GetRoute<Catalogo>("escolaridades");
        EstatusEscolaridades = await CebvNetwork.GetRoute<Catalogo>("estatus-escolaridades");
        EstadosConyugales = await CebvNetwork.GetRoute<Catalogo>("estados-conyugales");
        Parentescos = await CebvNetwork.GetRoute<Catalogo>("parentescos");
    }
    
    
}