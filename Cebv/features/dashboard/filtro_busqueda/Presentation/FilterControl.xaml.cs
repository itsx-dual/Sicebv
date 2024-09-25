using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.filtro_busqueda.Presentation;

public partial class FilterControl : UserControl
{
    public FilterControl()
    {
        InitializeComponent();
    }

    private void NavigationViewItem_Click(object sender, RoutedEventArgs e)
    {
        // Identificamos qué item fue clickeado usando la propiedad Tag
        if (sender is NavigationViewItem selectedItem)
        {
            string tag = selectedItem?.Tag?.ToString();

            switch (tag)
            {
                case "General":
                    FiltrosGenerales.Visibility = Visibility.Visible;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Reportante":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Visible;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Desaparecido":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Visible;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Hechos":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Visible;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Hipotesis":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Visible;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Media filiacion":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Visible;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
                case "Señas particulares":
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Visible;
                    break;
                default:
                    FiltrosGenerales.Visibility = Visibility.Collapsed;
                    Reportante.Visibility = Visibility.Collapsed;
                    Desaparecido.Visibility = Visibility.Collapsed;
                    HechosDesaparicion.Visibility = Visibility.Collapsed;
                    HipotesisOficial.Visibility = Visibility.Collapsed;
                    MediaFiliacion.Visibility = Visibility.Collapsed;
                    SeñasParticulares.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}