using System.Windows;
using System.Windows.Controls;
using Cebv.features.dashboard.comunicacion_difusion.comunicacion.presentation;

namespace Cebv.features.dashboard.comunicacion_difusion.presentation;

public partial class ComunicacionDifusionPage : Page
{
    public ComunicacionDifusionPage()
    {
        InitializeComponent();
    }

    private void ComunicacionDifusion_OnLoaded(object sender, RoutedEventArgs e)
    {
        ComunicacionDifusionNavigationView.Navigate(typeof(ComunicacionPage));
    }
}