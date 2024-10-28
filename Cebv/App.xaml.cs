using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cebv.core.util;
using Cebv.core.util.navigation;
using Cebv.core.util.reporte;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;
using Cebv.features.formulario_cebv.presentation;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using ISnackbarService = Cebv.core.util.snackbar.ISnackbarService;
using SnackbarService = Cebv.core.util.snackbar.SnackbarService;

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
        ApplicationThemeManager.ApplySystemTheme();
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

        services.AddTransient<FormularioCebvViewModel>();
        services.AddTransient<CircunstanciaDesaparicionViewModel>();
        
        services.AddSingleton<IReporteService, ReporteService>();
        services.AddSingleton<ISnackbarService, SnackbarService>();
        services.AddSingleton<IDashboardNavigationService, DashboardNavigationService>();
        services.AddSingleton<IFormularioCebvNavigationService, FormularioCebvNavigationService>();
        services.AddSingleton<IContentDialogService, ContentDialogService>();

            
        return services.BuildServiceProvider();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewTextInputEvent, new TextCompositionEventHandler(TextBoxHelper.PreviewTextInput));
        EventManager.RegisterClassHandler(typeof(TextBox), TextBox.TextChangedEvent, new TextChangedEventHandler(TextBoxHelper.AutoCompleted));
        EventManager.RegisterClassHandler(typeof(TextBox), TextBox.TextChangedEvent, new TextChangedEventHandler(TextBoxHelper.UpperCaseText));
        EventManager.RegisterClassHandler(typeof(TextBox), TextBox.LostFocusEvent, new RoutedEventHandler(TextBoxHelper.ValidText));
        EventManager.RegisterClassHandler(typeof(DatePicker), DatePicker.SelectedDateChangedEvent, new EventHandler<SelectionChangedEventArgs>(TextBoxHelper.DatePickerSelectedDateChanged));
        EventManager.RegisterClassHandler(typeof(TextBox), TextBox.LostFocusEvent, new RoutedEventHandler(TextBoxHelper.ValidText));
        
        base.OnStartup(e);
    }
}