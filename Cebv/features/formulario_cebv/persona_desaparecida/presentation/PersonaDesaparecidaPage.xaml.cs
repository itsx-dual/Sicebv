using System.Windows.Controls;
using Cebv.features.formulario_cebv.persona_desaparecida.data;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class PersonaDesaparecidaPage : Page
{
    public PersonaDesaparecidaViewModel ViewModel { get; }
    public PersonaDesaparecidaPage()
    {
        InitializeComponent();
        ViewModel = App.Current.Services.GetService<PersonaDesaparecidaViewModel>()!;
        this.DataContext = ViewModel;
    }
}