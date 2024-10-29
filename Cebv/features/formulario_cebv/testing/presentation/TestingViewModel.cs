using System.Collections.ObjectModel;
using Cebv.core.domain;
using Cebv.core.util;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.formulario_cebv.testing.presentation;

public partial class TestingViewModel : ObservableObject
{
    [ObservableProperty] private Reporte _reporte = null!;
    [ObservableProperty] private Desaparecido _desaparecido = new();
    [ObservableProperty] private Reportante _reportante = new();
 
    [ObservableProperty] private ObservableCollection<Catalogo> _companiasTelefonicas = new();
    
    [ObservableProperty] private string? _noTelefonoMovil;
    [ObservableProperty] private string? _observacionesMovil;
    [ObservableProperty] private Catalogo? _companiaTelefonicaSelected;
    [ObservableProperty] private bool _tieneTelefonosMoviles;
    
    public TestingViewModel()
    {
        Cargar();
        
        TieneTelefonosMoviles = Desaparecido.Persona.Telefonos.Any(x => (bool)x.EsMovil!);
    }
        
    private async void Cargar()
    {
        CompaniasTelefonicas = await CebvNetwork.GetRoute<Catalogo>("companias-telefonicas");
    }
    
    [RelayCommand]
    private void OnAddTelefonoMovil()
    {
        if (NoTelefonoMovil is null) return;

        Desaparecido.Persona.Telefonos.Add(new Telefono
        {
            Numero = NoTelefonoMovil,
            Observaciones = ObservacionesMovil,
            EsMovil = true,
            Compania = CompaniaTelefonicaSelected
        });

        NoTelefonoMovil = null;
        ObservacionesMovil = null;
        CompaniaTelefonicaSelected = null;
    }
    
    [RelayCommand]
    private void OnEliminarTelefono(Telefono telefono) => Desaparecido.Persona.Telefonos.Remove(telefono);

    [RelayCommand]
    private async Task OnEditarTelefono(Telefono telefono)
    {
        var showEditList = new ShowDialogEditList();
        
        // Crea una instancia de EditarTelefonoDialogContent y asigna el DataContext
        var dialogContent = new EditTelefono
        {
            DataContext = this
        };

        await showEditList.ShowContentDialogCommand.ExecuteAsync(dialogContent);

        if (showEditList.Confirmacion)
        {
            Desaparecido.Persona.Telefonos.Remove(telefono);
            
            Desaparecido.Persona.Telefonos.Add(new Telefono
            {
                Numero = NoTelefonoMovil,
                Observaciones = ObservacionesMovil,
                EsMovil = true,
                Compania = CompaniaTelefonicaSelected
            });
            
            NoTelefonoMovil = null;
            ObservacionesMovil = null;
            CompaniaTelefonicaSelected = null;
        }
    }
}