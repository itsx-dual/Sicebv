using Cebv.app.presentation;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Cebv.core.util;

public static class DialogHelper
{
    private static readonly IContentDialogService DialogService =
        App.Current.Services.GetService<IContentDialogService>()!;

    public static async Task<ContentDialogResult> ShowDialog(dynamic content, string title = "Editar") =>
        await DialogService.ShowAsync(
            new ContentDialog
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "Guardar",
                CloseButtonText = "Cancelar"
            },
            new CancellationToken()
        );

    public static void ShowWebview(string uri, string titulo) => new WebView2Window(uri, titulo).Show();
}