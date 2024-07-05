using Cebv.app.presentation;
using Cebv.core.util.navigation;
using Cebv.features.dashboard.presentation;
using Cebv.features.login.presentation;

namespace Cebv;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            // Inicio de sesion
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginPage>();
            services.AddTransient<LoginWindow>();

            // Dashboard
            services.AddSingleton<DashboardWindow>();
            services.AddSingleton<DashboardPage>();
            services.AddSingleton<DashboardViewModel>();
            
            // Navegación principal
            services.AddSingleton<IDashboardNavigationService, DashboardNavigationService>();
            
            // Manejo de errores
            services.AddSingleton<ISnackbarService, SnackbarService>();
            
            services.AddSingleton<IFormularioCebvNavigationService, FormularioCebvNavigationService>();
            services.AddSingleton<IReporteService, ReporteService>();
        }).Build();

    [STAThread]
    public static void Main()
    {
        Host.Start();

        App app = new();
        app.InitializeComponent();
        app.MainWindow = Host.Services.GetRequiredService<LoginWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }
}