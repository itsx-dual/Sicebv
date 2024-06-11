using System.Windows;
using System.Windows.Controls;
using Cebv.core.util;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();
        SubscribeTexBoxesEvents(this);

        NombreTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };

        ApellidoPaternoTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };

        ApellidoMaternoTb.LostFocus += (sender, e) =>
        {
            if (DataContext is ReportanteViewModel viewModel)
                viewModel.GuardarBorrador();
        };
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