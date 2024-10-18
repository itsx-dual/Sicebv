using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util;

public partial class ShowDialog : ObservableObject
{
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private bool _confirmacion;
    
    [RelayCommand]
    private async Task OnShowContentDialog(List<string> emptyElements)
    {
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "¿Esta seguro de guardar?",
                Content = "Los siguientes campos estan vacios, ¿esta seguro de guardar?" + string.Join("\n", emptyElements),
                PrimaryButtonText = "Guardar Y Continuar",
                SecondaryButtonText = "Don't Save",
                CloseButtonText = "Cancelar"
            },
            new CancellationToken()
        );
        
        Confirmacion = result switch
        {
            ContentDialogResult.Primary => true,
            ContentDialogResult.Secondary => false,
            _ => false
        };
    }
}