using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Cebv.core.util;
using Cebv.features.formulario_cebv.circunstancias_desaparicion.data;
using static Cebv.core.util.enums.TipoExpediente;

namespace Cebv.features.formulario_cebv.circunstancias_desaparicion.presentation;

public partial class CircunstanciaDesaparicionPage : Page
{
    public CircunstanciaDesaparicionPage()
    {
        InitializeComponent();
    }

    private void DateConverter(object sender, RoutedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        
        string? datePickerName = textBox.Tag?.ToString();
        DatePicker? datePicker = FindName(datePickerName) as DatePicker;
    
        if (textBox?.Text != null && datePicker != null)
        {
            datePicker.SelectedDate = new CebvDateConverter().Convert(textBox.Text, typeof(string), null, 
                CultureInfo.CurrentCulture) as DateTime?;
        }
    }

    private void ExpedientesDirectos_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Expediente;
        e.Accepted = item?.Tipo == Directo;
    }

    private void ExpedientesIndirectos_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Expediente;
        e.Accepted = item?.Tipo == Indirecto;
    }

    private void OnNavigateToRelatedReport(object sender, MouseButtonEventArgs e)
    {
        ((CircunstanciaDesaparicionViewModel)DataContext).ReporteClickCommand.Execute(null);
    }
    private void MinDesaparicionFG_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (MinDesaparicionFG.Text.Length>1)
        {
            FechaPercatoDatePicker.Focus();
        }
        if (MinDesaparicionFG.Text.Length==0)
        {
            HoraDesaparicionFG.Focus();
        }
    }

    private void HoraDesaparicionFG_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (HoraDesaparicionFG.Text.Length>1)
        {
            MinDesaparicionFG.Focus();
        }
        /* ESTE POR SI SE QUIERE QUE AL BORRAR SE VAYA AL CAMPO ANTERIOR PERO A MI GUSTO ESTA MEH
        if (HoraDesaparicion.Text.Length==0)
        {
            FechaDesaparicion.Focus();
        }*/
        
    }
    private void MinDesaparicionPercato_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (MinDesaparicionPercato.Text.Length>1)
        {
            AclaracionFecha.Focus();
        }
        if (MinDesaparicionPercato.Text.Length==0)
        {
            HoraDesaparicionPercato.Focus();
        }
    }

    private void HoraDesaparicionPercato_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (HoraDesaparicionPercato.Text.Length>1)
        {
            MinDesaparicionPercato.Focus();
        }
        /* ESTE POR SI SE QUIERE QUE AL BORRAR SE VAYA AL CAMPO ANTERIOR PERO A MI GUSTO ESTA MEH
        if (HoraDesaparicion.Text.Length==0)
        {
            FechaDesaparicion.Focus();
        }*/
        
    }
    
    private void MinDesaparicionFG2_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (MinDesaparicionFG2.Text.Length>1)
        {
            FechaPercatoAprox.Focus();
        }
        if (MinDesaparicionFG2.Text.Length==0)
        {
            HoraDesaparicionFG2.Focus();
        }
    }

    private void HoraDesaparicionFG2_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (HoraDesaparicionFG2.Text.Length>1)
        {
            MinDesaparicionFG2.Focus();
        }
        /* ESTE POR SI SE QUIERE QUE AL BORRAR SE VAYA AL CAMPO ANTERIOR PERO A MI GUSTO ESTA MEH
        if (HoraDesaparicion.Text.Length==0)
        {
            FechaDesaparicion.Focus();
        }*/
        
    }
    private void MinDesaparicionPercato2_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (MinDesaparicionPercato2.Text.Length>1)
        {
            AclaracionFecha2.Focus();
        }
        if (MinDesaparicionPercato2.Text.Length==0)
        {
            HoraDesaparicionPercato2.Focus();
        }
    }

    private void HoraDesaparicionPercato2_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (HoraDesaparicionPercato2.Text.Length>1)
        {
            MinDesaparicionPercato2.Focus();
        }
        /* ESTE POR SI SE QUIERE QUE AL BORRAR SE VAYA AL CAMPO ANTERIOR PERO A MI GUSTO ESTA MEH
        if (HoraDesaparicion.Text.Length==0)
        {
            FechaDesaparicion.Focus();
        }*/
        
    }
}