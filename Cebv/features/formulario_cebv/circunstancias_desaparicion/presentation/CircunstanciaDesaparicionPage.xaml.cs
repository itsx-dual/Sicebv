using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

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
        if (FechaDesaparicionAprox  != null)
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
}