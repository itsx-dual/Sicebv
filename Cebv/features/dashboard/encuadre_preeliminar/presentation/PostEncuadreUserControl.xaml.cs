using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class PostEncuadreUserControl : UserControl
{
    private Window? _window;
    public PostEncuadreUserControl()
    {
        InitializeComponent();
    }

    private void Aceptar_OnClick(object sender, RoutedEventArgs e)
    {
        _window = Window.GetWindow(this);
        if (_window == null) return;
        _window.DialogResult = true;
        _window.Close();
    }

    private void Cancelar_OnClick(object sender, RoutedEventArgs e)
    {
        _window = Window.GetWindow(this);
        if (_window == null) return;
        _window.DialogResult = false;
        _window.Close();
    }
}