using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util;

public partial class ShowDialogEditList : ObservableObject
{
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private bool _confirmacion;
    
    [RelayCommand]
    private async Task OnShowContentDialog(object content)
    {
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Editar elemento",
                Content = content,
                PrimaryButtonText = "Guardar",
                CloseButtonText = "No guardar"
            },
            new CancellationToken()
        );
        
        Confirmacion = result switch
        {
            ContentDialogResult.Primary => true,
            _ => false
        };
    }
}