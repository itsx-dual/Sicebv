using System.ComponentModel.DataAnnotations;
using Cebv.core.util.snackbar;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.core.util;

public static class ValidationHelpers
{
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;

    public static void ShowErrorsSnack(IEnumerable<ValidationResult> errores, string titulo = "Errores de validación:")
    {
        if (!errores.Any()) _snackBarService.Show("No hay errores.", "", 
            ControlAppearance.Primary, 
            new SymbolIcon(SymbolRegular.Check24), 
            new TimeSpan(0,0,5));;
        
        var mesgErrores = errores.Aggregate(string.Empty, (current, error) => current + (error.ErrorMessage + Environment.NewLine));
        _snackBarService.Show(titulo, mesgErrores, ControlAppearance.Caution, new SymbolIcon(SymbolRegular.Warning20), new TimeSpan(0,0,5));
    }
}