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

        FechaDesaparicionAprox.LostFocus += DateConverter;
        FechaPercatoAprox.LostFocus += DateConverter;
    }

    private void DateConverter(object sender, RoutedEventArgs e)
    {
        if (FechaDesaparicionAprox != null)
        {
            string fechaAprox = FechaDesaparicionAprox.Text;
            ConvertFormat convert = new ConvertFormat(fechaAprox);
            FechaDesaparicion.SelectedDate = convert.GetDate();
        }

        if (FechaPercatoAprox != null)
        {
            string fechaAprox = FechaPercatoAprox.Text;
            ConvertFormat convert = new ConvertFormat(fechaAprox);
            FechaPercato.SelectedDate = convert.GetDate();
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
}