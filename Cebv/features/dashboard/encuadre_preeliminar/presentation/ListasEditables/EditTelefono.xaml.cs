using System.Windows;
using System.Windows.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation.ListasEditables;

public partial class EditTelefono : UserControl
{
    public EditTelefono(string visibility)
    {
        InitializeComponent();

        switch (visibility)
        {
            case "Reportante":
                Reportante.Visibility = Visibility.Visible;
                Desaparecido.Visibility = Visibility.Collapsed;
                break;
            case "Desaparecido":
                Reportante.Visibility = Visibility.Collapsed;
                Desaparecido.Visibility = Visibility.Visible;
                break;
            default:
                Reportante.Visibility = Visibility.Collapsed;
                Desaparecido.Visibility = Visibility.Collapsed;
                break;
        }
    }
}