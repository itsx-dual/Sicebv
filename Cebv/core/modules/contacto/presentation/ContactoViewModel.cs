using System.Collections.ObjectModel;
using Cebv.core.data;
using Cebv.core.modules.contacto.data;
using Cebv.core.modules.contacto.domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.core.modules.contacto.presentation;

public partial class ContactoViewModel : ObservableObject
{
    /**
     * Constructor de la clase
     */
    public ContactoViewModel()
    {
        CargarCatalogos();
    }

    /**
     * Variables de la clase
     */
    [ObservableProperty] private string _telefonoMovil = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private Catalogo _companiaTelefonica = new();
    [ObservableProperty] private string _observacionesMovil = String.Empty;
    [ObservableProperty] private ObservableCollection<TelefonoMovil> _telefonosMoviles = new();

    [ObservableProperty] private string _telefonoFijo = String.Empty;
    [ObservableProperty] private string _observacionesFijo = String.Empty;

    [ObservableProperty] private string _correoElectronico = String.Empty;
    [ObservableProperty] private string _observacionesCorreoElectronico = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _tiposRedesSociales = new();
    [ObservableProperty] private Catalogo _tipoRedSocial = new();
    [ObservableProperty] private string _usuario = String.Empty;
    [ObservableProperty] private string _observacionesRedSocial = String.Empty;

    [RelayCommand]
    private void AddTelefonoFijo()
    {
        TelefonoMovil telefono = new TelefonoMovil()
        {
            Telefono = TelefonoMovil,
            CompaniaTelefonica = CompaniaTelefonica,
            Observaciones = ObservacionesMovil
        };

        TelefonoMovil = string.Empty;
        CompaniaTelefonica = new();
        ObservacionesMovil = string.Empty;

        TelefonosMoviles.Add(telefono);
    }

    /**
     * Peticiones a la API para obtener los catálogos
     */
    private async void CargarCatalogos()
    {
        CompaniasTelefonicas = await ContactoNetwork.GetCompaniasTelefonicas();
        TiposRedesSociales = await ContactoNetwork.GetTiposRedesSociales();
    }
}