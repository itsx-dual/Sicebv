using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarViewModel : ObservableValidator
{
    partial void OnNoTelefonoDesaparecidoChanged(string value)
    {
        ValidateProperty(value, nameof(NoTelefonoDesaparecido));
        var errors = GetErrors(nameof(NoTelefonoDesaparecido));
        if (errors.Count() == 0) return;
        
        var message = errors.Aggregate(string.Empty, (current, error) => current + (error.ErrorMessage + Environment.NewLine));
        _snackBarService.Show("El numero de telefono tiene errores.",
            message,
            ControlAppearance.Caution,
            new SymbolIcon(SymbolRegular.Warning28),
            new TimeSpan(0,0,5));
    }
    
    
}