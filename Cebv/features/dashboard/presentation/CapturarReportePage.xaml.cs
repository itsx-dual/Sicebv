using System.Windows;
using System.Windows.Controls;
using Cebv.features.reporte.presentation;

namespace Cebv.features.dashboard.presentation;

public partial class CapturarReportePage : Page
{
    public CapturarReportePage()
    {
        InitializeComponent();
    }

    private void CapturarReportePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        FormularioReportesNavigationView.Navigate(typeof(ReporteFormularioPage));
    }
}