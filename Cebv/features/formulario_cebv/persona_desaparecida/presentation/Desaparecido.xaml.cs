using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class Desaparecido : Page
{
    public Desaparecido()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<DesaparecidoViewModel>();
    }
}