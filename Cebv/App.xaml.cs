using System.Windows;
using Cebv.features.formulario_cebv.persona_desaparecida.data;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;
using Cebv.features.formulario_cebv.presentation;
using Cebv.features.formulario_cebv.presentation.data;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
        InitializeComponent();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Viewmodel principal del formulario.
        services.AddSingleton<FormularioCebvViewModel>();
        services.AddSingleton<FormularioCebvPage>();
        services.AddSingleton<PersonaDesaparecidaViewModel>();

        services.AddSingleton<IFormularioService, FormularioService>();

        return services.BuildServiceProvider();
    }
}