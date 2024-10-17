using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.presentation;

public partial class InicioViewModel : ObservableObject
{
    private IContentDialogService _dialogService = App.Current.Services.GetService<IContentDialogService>()!;

    [ObservableProperty] private string _dialogResultText;
    
    [RelayCommand]
    private async Task OnShowContentDialog(object content)
    {
        ContentDialogResult result = await _dialogService.ShowAsync(
            new ContentDialog()
            {
                Title = "Save your work?",
                Content = content,
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Don't Save",
                CloseButtonText = "Cancel",
            },
            new CancellationToken()
        );

        DialogResultText = result switch
        {
            ContentDialogResult.Primary => "User saved their work",
            ContentDialogResult.Secondary => "User did not save their work",
            _ => "User cancelled the dialog"
        };
    }
}