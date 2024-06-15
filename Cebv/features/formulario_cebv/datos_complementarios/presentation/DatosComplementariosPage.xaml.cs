using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.datos_complementarios.presentation;

public partial class DatosComplementariosPage : Page
{
    public DatosComplementariosPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<DatosComplemenatiosViewModel>();
    }
}