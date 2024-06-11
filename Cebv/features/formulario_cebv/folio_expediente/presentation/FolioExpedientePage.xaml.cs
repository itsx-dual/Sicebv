using System.Windows.Controls;
using Cebv.core.util;
using System.Windows;

namespace Cebv.features.formulario_cebv.folio_expediente.presentation;

public partial class FolioExpedientePage : Page
{
    public FolioExpedientePage()
    {
        InitializeComponent();
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