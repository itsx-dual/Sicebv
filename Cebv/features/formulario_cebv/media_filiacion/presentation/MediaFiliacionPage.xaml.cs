using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.media_filiacion.presentation;

public partial class MediaFiliacionPage : Page
{
    public MediaFiliacionPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<MediaFiliacionViewModel>();

    }
}