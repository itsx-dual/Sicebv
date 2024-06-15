using System.Windows;
using Cebv.core.modules.persona.presentation;
using Cebv.core.util.navigation;
using Cebv.features.formulario_cebv.datos_complementarios.presentation;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;
using Cebv.features.formulario_cebv.intrumentos_juridicos.presentation;
using Cebv.features.formulario_cebv.media_filiacion_complementaria.presentation;
using Cebv.features.formulario_cebv.media_filiacion.presentation;
using Cebv.features.formulario_cebv.persona_desaparecida.presentation;
using Cebv.features.formulario_cebv.prendas.presentation;
using Cebv.features.formulario_cebv.presentation;
using Cebv.features.formulario_cebv.reportante.presentation;
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
        services.AddSingleton<IFormularioCebvNavigationService, FormularioCebvNavigationService>();

        services.AddSingleton<DatosReporteViewModel>();
        services.AddSingleton<InstrumentoJuridicoViewModel>();
        services.AddSingleton<ReportanteViewModel>();
        services.AddSingleton<DesaparecidoViewModel>();
        services.AddSingleton<MediaFiliacionViewModel>();
        services.AddSingleton<MediaFiliacionComplementariaViewModel>();
        services.AddSingleton<PrendasViewModel>();
        services.AddSingleton<DatosComplemenatiosViewModel>();
        return services.BuildServiceProvider();
    }
}