using System.Windows;
using Wpf.Ui.Controls;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class AgregarExpediente : FluentWindow
{
    public AgregarExpediente()
    {
        InitializeComponent();
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
        if (DataContext is RelacionarExpedienteViewModel vm)
        {
            vm.CloseAction = null;
        }

        base.OnClosing(e);
    }

    private void OnCancelar(object sender, RoutedEventArgs e) =>
        Close();
}