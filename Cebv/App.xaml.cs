using System.Windows;
using Cebv.core.modules.persona.presentation;
using Cebv.core.util.navigation;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;
using Cebv.features.formulario_cebv.presentation;
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

        services.AddScoped<FormularioCebvViewModel>();
        services.AddScoped<PersonaViewModel>();
        services.AddScoped<DesaparecidoViewModel>();
        services.AddSingleton<IFormularioCebvNavigationService, FormularioCebvNavigationService>();
            
        return services.BuildServiceProvider();
    }
}