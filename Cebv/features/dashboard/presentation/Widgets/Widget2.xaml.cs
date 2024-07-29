using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Cebv.core.modules.reporte.data;

namespace Cebv.features.dashboard.presentation;

public partial class Widget2 : UserControl
{
    private int _currentIndex = 0;
    private ObservableCollection<ReporteResponse> _reportesPendientes;

    public Widget2()
    {
        InitializeComponent();
        DataContextChanged += WidgetDataContextChanged;
    }

    private void WidgetDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is ReportesDesaparicionViewModel viewModel)
        {
            _reportesPendientes = viewModel.ReportesPendientes;
            UpdateCarousel();
        }
    }

    private void UpdateCarousel()
    {
        var visibleItems = new ObservableCollection<ReporteResponse>();
        for (int i = _currentIndex; i < _currentIndex + 1 && i < _reportesPendientes.Count; i++)
        {
            visibleItems.Add(_reportesPendientes[i]);
        }

        CarouselListBox.ItemsSource = visibleItems;
    }
}