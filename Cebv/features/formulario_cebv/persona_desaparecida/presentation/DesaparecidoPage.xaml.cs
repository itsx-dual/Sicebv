using System.Windows;
using System.Windows.Controls;
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
}