using System.Globalization;
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
    }

    private void DateConverter(object sender, RoutedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        
        string? datePickerName = textBox.Tag?.ToString();
        DatePicker? datePicker = FindName(datePickerName) as DatePicker;
    
        if (textBox.Text != null && datePicker != null)
        {
            datePicker.SelectedDate = new CebvDateConverter().Convert(textBox.Text, typeof(string), null, 
                CultureInfo.CurrentCulture) as DateTime?;
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