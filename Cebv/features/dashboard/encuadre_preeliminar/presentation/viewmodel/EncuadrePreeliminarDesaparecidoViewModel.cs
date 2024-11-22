using Cebv.core.util;
using Cebv.core.util.reporte.viewmodels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel;

public partial class EncuadrePreeliminarViewModel
{
    [ObservableProperty] private bool _seDesconoceFechaNacimientoDesaparecido;
    [ObservableProperty] private bool _desaparecidoTieneTelefonos;
    [ObservableProperty] private string? _curp;
    [ObservableProperty] private bool _noHayCurp;
    [ObservableProperty] private DateTime _fechaNacimientoDesaparecido = DateTime.Today;
    [ObservableProperty] private Telefono _telefonoDesaparecido = new() { EsMovil = true };
    [ObservableProperty] private int _anosDesaparecido;
    [ObservableProperty] private int _mesesDesaparecido;
    [ObservableProperty] private int _diasDesaparecido;
    
    partial void OnCurpChanged(string? value)
    {
        NoHayCurp = value?.Length == 0;
        if (Desaparecido.Persona.Curp is null) return;
        Desaparecido.Persona.Curp = value;
    }
    
    // Telefonos
    [RelayCommand]
    private void OnAddTelefonoMovilDesaparecido()
    {
        TelefonoDesaparecido.ValidarDesaparecido();

        if (TelefonoDesaparecido.HasErrors)
        {
            ValidationHelpers.ShowErrorsSnack(TelefonoDesaparecido.GetErrors(), "No se puede agregar el numero de telefono del desaparecido.");
            return;
        }
        
        Desaparecido.Persona.Telefonos.Add(TelefonoDesaparecido);
        DesaparecidoTieneTelefonos = Desaparecido.Persona.Telefonos.Any();
        TelefonoDesaparecido = new() { EsMovil = true};
    }
    
    [RelayCommand]
    private void OnRemoveTelefonoDesaparecido(Telefono telefono)
    {
        Desaparecido.Persona.Telefonos.Remove(telefono);
        DesaparecidoTieneTelefonos = Desaparecido.Persona.Telefonos.Any();
    }
    
    [RelayCommand]
    private async Task OnEditarTelefonoDesaparecido(Telefono telefono)
    {
        await EditarTelefono(telefono, Desaparecido.Persona.Telefonos);
    }
    
    // Lidiar con fechas de nacimiento y la fecha de desaparicion para calcular la edad al momento de la desaparicion.
    partial void OnSeDesconoceFechaNacimientoDesaparecidoChanged(bool value)
    {
        if (value)
        {
            Desaparecido.Persona.FechaNacimiento = null;
            Desaparecido.FechaNacimientoAproximada = FechaNacimientoDesaparecido;
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }
        else
        {
            Desaparecido.Persona.FechaNacimiento = FechaNacimientoDesaparecido;
            Desaparecido.FechaNacimientoAproximada = null;
            Desaparecido.EdadMomentoDesaparicionAnos = null;
            Desaparecido.EdadMomentoDesaparicionMeses = null;
            Desaparecido.EdadMomentoDesaparicionDias = null;
        }
    }
    
    partial void OnFechaNacimientoDesaparecidoChanged(DateTime value)
    {
        DiferenciaFechas(FechaNacimientoDesaparecido, FechaDesaparicion);
        if (SeDesconoceFechaNacimientoDesaparecido)
        {
            Desaparecido.FechaNacimientoAproximada = value;
            Desaparecido.EdadMomentoDesaparicionAnos = AnosDesaparecido;
            Desaparecido.EdadMomentoDesaparicionMeses = MesesDesaparecido;
            Desaparecido.EdadMomentoDesaparicionDias = DiasDesaparecido;
        }
        else
        {
            Desaparecido.Persona.FechaNacimiento = value;
        }
    }
}