using System.Windows.Controls;
using System.Windows.Data;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.features.formulario_cebv.testing.presentation;

public partial class TestingPage : Page
{
    public TestingPage()
    {
        InitializeComponent();
    }
    
    private void TelefonosMoviles_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Telefono;
        e.Accepted = (bool) item?.EsMovil!;
    }
}