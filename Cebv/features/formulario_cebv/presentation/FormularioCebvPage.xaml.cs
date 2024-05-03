using System.Windows;
using System.Windows.Controls;
using Cebv.features.formulario_cebv.datos_del_reporte.presentation;

namespace Cebv.features.formulario_cebv.presentation;

public partial class FormularioCebvPage : Page
{
    public FormularioCebvPage()
    {
        InitializeComponent();
    }

    private void FormularioCebvPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        FormularioNavigationView.Navigate(typeof(DatosReportePage));
    }
}