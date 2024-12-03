using System.ComponentModel.DataAnnotations;
using System.Windows;
using Cebv.core.util;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel;

public partial class EncuadrePreeliminarViewModel
{
    // Valores para insercion a listas

    [ObservableProperty] private Telefono _telefonoReportante = new() { EsMovil = true };
    [ObservableProperty] private bool _reportanteTieneTelefonos;
    
    [RelayCommand]
    private void OnAddTelefonoMovilReportante()
    {
        TelefonoReportante.ValidarReportante();

        if (TelefonoReportante.HasErrors)
        {
            ValidationHelpers.ShowErrorsSnack(TelefonoReportante.GetErrors(), "No se puede agregar el numero de telefono del reportante.");
            return;
        }
        
        Reportante.Persona.Telefonos.Add(TelefonoReportante);
        ReportanteTieneTelefonos = Reportante.Persona.Telefonos.Any();
        TelefonoReportante = new() { EsMovil = true};
    }
    
    [RelayCommand]
    private void OnRemoveTelefonoReportante(Telefono telefono)
    {
        Reportante.Persona.Telefonos.Remove(telefono);
        ReportanteTieneTelefonos = Reportante.Persona.Telefonos.Any();
    }
    
    [RelayCommand]
    private async Task OnEditarTelefonoReportante(Telefono telefono)
    {
        await EditarTelefono(telefono, Reportante.Persona.Telefonos, Visibility.Collapsed);
    }
}