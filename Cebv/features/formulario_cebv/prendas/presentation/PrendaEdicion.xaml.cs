using System.Windows;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.prendas.presentation;

public partial class PrendaEdicion : FluentWindow
{
    public PrendaEdicion()
    {
        InitializeComponent();
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        if (DataContext is PrendaEdicionViewModel vm)
        {
            vm.CloseAction = null;
        }

        base.OnClosing(e);
    }

    private void OnCancelar(object sender, RoutedEventArgs e) =>
        Close();
}