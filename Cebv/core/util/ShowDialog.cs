using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util;

public partial class ShowDialog : ObservableObject
{
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private string _confirmacion = string.Empty;
    
    [RelayCommand]
    private async Task OnShowContentDialog(List<string> emptyElements)
    {
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "¿Esta seguro de guardar?",
                Content = "Los siguientes campos estan vacios, ¿esta seguro de guardar?\n" + string.Join("\n", emptyElements),
                PrimaryButtonText = "Guardar",
                SecondaryButtonText = "No guardar",
                CloseButtonText = "Cancelar"
            },
            new CancellationToken()
        );
        
        Confirmacion = result switch
        {
            ContentDialogResult.Primary => "Guardar",
            ContentDialogResult.Secondary => "No guardar",
            _ => "Cancelar"
        };
    }
}