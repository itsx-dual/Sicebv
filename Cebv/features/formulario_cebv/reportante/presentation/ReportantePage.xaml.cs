using System.Windows.Controls;
using System.Windows.Data;
using Cebv.core.util.reporte.viewmodels;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();
    }

    private void TelefonosMoviles_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Telefono;
        e.Accepted = (bool) item?.EsMovil!;
    }

    private void TelefonosFijos_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Telefono;
        e.Accepted = (bool) !item?.EsMovil!;
    }

    private void CorreosElectronicos_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Contacto;
        e.Accepted = item?.Tipo == "Correo Electronico";
    }
}