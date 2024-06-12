using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.formulario_cebv.vehiculos_involucrados.presentation;

public partial class VehiculosInvolucradosPage : Page
{
    public VehiculosInvolucradosPage()
    {
        InitializeComponent();
    }

    private void Imagenes_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (Imagenes == null) return;
        
        Imagenes.Children.Clear();
        
        Console.WriteLine("DataContext Changed");
        
        var archivos = ((VehiculosInvolucradosViewModel)DataContext).OpenedFilePath;
        if (archivos == null) return;

        foreach (var imagen in archivos)
        {
            Image wpfui_image = new()
            {
                Source = new BitmapImage(new Uri(imagen)),
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(5),
                Width = 300,
                Height = 300,
                Stretch = Stretch.UniformToFill
            };

            Imagenes.Children.Add(wpfui_image);
        }
    }
}