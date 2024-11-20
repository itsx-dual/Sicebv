using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cebv.features.dashboard.encuadre_preeliminar.presentation;
using EncuadrePreeliminarViewModel = Cebv.features.dashboard.encuadre_preeliminar.presentation.viewmodel.EncuadrePreeliminarViewModel;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.formulario_cebv.comunicacion_difusion.presentation;

public partial class GeneracionBoletinPage : Page
{
    public GeneracionBoletinPage()
    {
        InitializeComponent();
    }

    private void Imagenes_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ListView listview) return;
        if (listview.DataContext is not EncuadrePreeliminarViewModel dataContext) return;
        if (!listview.Items.Cast<dynamic>().Any()) return;

        if (e.Key == Key.Delete)
        {
            // Trato de respetar MVVM lo mas posible.
            dataContext.DeleteDesaparecidoImagenCommand.Execute(listview.SelectedItem);
        }
    }
}