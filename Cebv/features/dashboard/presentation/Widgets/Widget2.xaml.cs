using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Cebv.core.modules.reporte.data;

namespace Cebv.features.dashboard.presentation;

public partial class Widget2 : UserControl
{
    private int _currentIndex = 0;

    public Widget2()
    {
        InitializeComponent();
        DataContextChanged += WidgetDataContextChanged;
    }

    private void WidgetDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is ReportesDesaparicionViewModel viewModel)
        {
            UpdateCarousel(viewModel.ReportesPendientes);
        }
    }

    private void UpdateCarousel(ObservableCollection<ReporteResponse> reportesPendientes)
    {
        if (reportesPendientes == null || reportesPendientes.Count == 0)
        {
            CarouselListBox.ItemsSource = null;
            return;
        }

        // Solo muestra un elemento en el carrusel
        CarouselListBox.ItemsSource = new ObservableCollection<ReporteResponse> { reportesPendientes[_currentIndex] };
    }

    private void OnPreviousClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is ReportesDesaparicionViewModel viewModel)
        {
            if (viewModel.ReportesPendientes.Count == 0) return;

            _currentIndex = (_currentIndex - 1 + viewModel.ReportesPendientes.Count) % viewModel.ReportesPendientes.Count;
            UpdateCarousel(viewModel.ReportesPendientes);
        }
    }

    private void OnNextClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is ReportesDesaparicionViewModel viewModel)
        {
            if (viewModel.ReportesPendientes.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % viewModel.ReportesPendientes.Count;
            UpdateCarousel(viewModel.ReportesPendientes);
        }
    }
}   