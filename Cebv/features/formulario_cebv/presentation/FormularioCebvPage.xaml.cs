using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cebv.core.util.navigation;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvPage
{
    public FormularioCebvPage()
    {
        InitializeComponent();
        var navigationService = App.Current.Services.GetService<IFormularioCebvNavigationService>();
        navigationService.SetNavigationControl(FormularioNavigationView);
    }

    private void FormularioCebvPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        FormularioNavigationView.Navigate(typeof(DatosReportePage));
    }
}