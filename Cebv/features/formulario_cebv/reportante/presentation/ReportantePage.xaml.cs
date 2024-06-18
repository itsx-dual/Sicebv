using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Cebv.features.formulario_cebv.reportante.presentation;

public partial class ReportantePage : Page
{
    public ReportantePage()
    {
        InitializeComponent();
        
        DataContext = App.Current.Services.GetService<ReportanteViewModel>();


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