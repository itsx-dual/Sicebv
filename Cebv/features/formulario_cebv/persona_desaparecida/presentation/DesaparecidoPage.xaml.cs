using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class DesaparecidoPage : Page
{
    public DesaparecidoPage()
    {
        InitializeComponent();
            
        FechaAprox.LostFocus += DateConverter;
    }

    private void DateConverter(object sender, RoutedEventArgs e)
    {
        if (FechaAprox  != null)
        {
            string fechaAprox = FechaAprox.Text;
            ConvertFormat convert = new ConvertFormat(fechaAprox);
            DatePicker.SelectedDate = convert.GetDate();
        }
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

    private void RedesSociales_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Contacto;
        e.Accepted = item?.Tipo == "Red Social";
    }
}