using System.Windows;
using System.Windows.Controls;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();

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
}