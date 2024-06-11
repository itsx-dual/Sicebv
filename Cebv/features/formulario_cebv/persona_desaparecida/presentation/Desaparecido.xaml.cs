using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.persona_desaparecida.presentation;

public partial class Desaparecido : Page
{
    public Desaparecido()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<DesaparecidoViewModel>();
        SubscribeTexBoxesEvents(this);
    }
    
    private void SubscribeTexBoxesEvents(DependencyObject depObj)
    {
        foreach (TextBox textBox in HelperMethods.FindVisualChildren<TextBox>(depObj))
        {
            textBox.TextChanged += TextBoxHelper.UpperCaseText;
            textBox.LostFocus += TextBoxHelper.TrimmedText;
        }
    }
}