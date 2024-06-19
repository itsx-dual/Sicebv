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
    // Telefono móvil
    [ObservableProperty] private string _telefonoMovil = String.Empty;

    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    [ObservableProperty] private Catalogo? _companiaTelefonica;
    [ObservableProperty] private string _observacionesMovil = String.Empty;
    [ObservableProperty] private ObservableCollection<Telefono> _telefonosMoviles = new();

    // Teléfono fijo
    [ObservableProperty] private string _telefonoFijo = String.Empty;
    [ObservableProperty] private string _observacionesFijo = String.Empty;
    [ObservableProperty] private ObservableCollection<Telefono> _telefonosFijos = new();

    // Correo electrónico
    [ObservableProperty] private string _correoElectronico = String.Empty;
    [ObservableProperty] private string _observacionesCorreoElectronico = String.Empty;
    [ObservableProperty] private ObservableCollection<CorreoElectronico> _correosElectronicos = new();

    // Redes sociales
    [ObservableProperty] private ObservableCollection<Catalogo> _tiposRedesSociales = new();
    [ObservableProperty] private Catalogo? _tipoRedSocial;
    [ObservableProperty] private string _usuario = String.Empty;
    [ObservableProperty] private string _observacionesRedSocial = String.Empty;
    [ObservableProperty] private ObservableCollection<RedSocial> _redesSociales = new();

    /**
     * Añadir y eliminar teléfonos móviles
     */
    [RelayCommand]
    private void AddTelefonoMovil()
    {
        if (String.IsNullOrEmpty(TelefonoMovil)) return;

        Telefono telefono = new Telefono()
        {
            Numero = TelefonoMovil,
            CompaniaTelefonica = CompaniaTelefonica,
            Observaciones = ObservacionesMovil
        };

        TelefonosMoviles.Add(telefono);

        TelefonoMovil = String.Empty;
        CompaniaTelefonica = null;
        ObservacionesMovil = String.Empty;
    }

    [RelayCommand]
    private void RemoveTelefonoMovil(Telefono telefono) =>
        TelefonosMoviles.Remove(telefono);


    /**
     * Añadir y eliminar teléfonos fijos
     */
    [RelayCommand]
    private void AddTelefonoFijo()
    {
        if (String.IsNullOrEmpty(TelefonoFijo)) return;

        Telefono telefono = new Telefono()
        {
            Numero = TelefonoFijo,
            Observaciones = ObservacionesFijo
        };

        TelefonosFijos.Add(telefono);

        TelefonoFijo = String.Empty;
        ObservacionesFijo = String.Empty;
    }

    [RelayCommand]
    private void RemoveTelefonoFijo(Telefono telefono) =>
        TelefonosFijos.Remove(telefono);


    /**
     * Añadir y eliminar correos electrónicos
     */
    [RelayCommand]
    private void AddCorreoElectronico()
    {
        if (String.IsNullOrEmpty(CorreoElectronico) ||
            !CorreoElectronico.Contains('@') ||
            !CorreoElectronico.Contains('.')) return;

        CorreoElectronico correo = new CorreoElectronico()
        {
            Correo = CorreoElectronico,
            Observaciones = ObservacionesCorreoElectronico
        };

        CorreosElectronicos.Add(correo);

        CorreoElectronico = String.Empty;
        ObservacionesCorreoElectronico = String.Empty;
    }

    [RelayCommand]
    private void RemoveCorreoElectronico(CorreoElectronico correo) =>
        CorreosElectronicos.Remove(correo);


    /**
     * Añadir y eliminar redes sociales
     */
    [RelayCommand]
    private void AddRedSocial()
    {
        if (TipoRedSocial == null || String.IsNullOrEmpty(Usuario)) return;

        RedSocial redSocial = new RedSocial()
        {
            TipoRedSocial = TipoRedSocial,
            Usuario = Usuario,
            Observaciones = ObservacionesRedSocial
        };

        RedesSociales.Add(redSocial);

        TipoRedSocial = null;
        Usuario = String.Empty;
        ObservacionesRedSocial = String.Empty;
    }

    [RelayCommand]
    private void RemoveRedSocial(RedSocial redSocial) =>
        RedesSociales.Remove(redSocial);


    /**
     * Peticiones a la API para obtener los catálogos
     */
    private async void CargarCatalogos()
    {
        CompaniasTelefonicas = await ContactoNetwork.GetCompaniasTelefonicas();
        TiposRedesSociales = await ContactoNetwork.GetTiposRedesSociales();
    }
}