using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaPage : Page
{
    public PersonaDesaparecidaPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<PersonaDesaparecidaViewModel>();
    }
}