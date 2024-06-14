using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosDeLocalizacionPage : Page
{
    public DatosDeLocalizacionPage()
    {
        InitializeComponent();
    }

    private void ImagenesUno_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (ImagenesUno == null) return;
        
        ImagenesUno.Children.Clear();
        
        Console.WriteLine("DataContext Changed");
        
        var archivos = ((DatosLocalizacionViewModel)DataContext).OpenedFilePathUno;
        if (archivos == null) return;

        foreach (var imagen in archivos)
        {
            Image wpfuiImage = new()
            {
                Source = new BitmapImage(new Uri(imagen)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(5),
                Width = 300,
                Height = 300,
                Stretch = Stretch.UniformToFill
            };

            ImagenesUno.Children.Add(wpfuiImage);
        }
    }

    private void ImagenesDos_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (ImagenesDos == null) return;
        
        ImagenesDos.Children.Clear();
        
        Console.WriteLine("DataContext Changed");
        
        var archivos = ((DatosLocalizacionViewModel)DataContext).OpenedFilePathDos;
        if (archivos == null) return;

        foreach (var imagen in archivos)
        {
            Image wpfuiImage = new()
            {
                Source = new BitmapImage(new Uri(imagen)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(5),
                Width = 300,
                Height = 300,
                Stretch = Stretch.UniformToFill
            };

            ImagenesDos.Children.Add(wpfuiImage);
        }
    }

    private void Informes_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (Informes == null) return;
        
        Informes.Children.Clear();
        
        Console.WriteLine("DataContext Changed");
        
        var archivos = ((DatosLocalizacionViewModel)DataContext).OpenedFilePathTres;
        if (archivos == null) return;

        foreach (var imagen in archivos)
        {
            Image wpfuiImage = new()
            {
                Source = new BitmapImage(new Uri(imagen)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(5),
                Width = 300,
                Height = 300,
                Stretch = Stretch.UniformToFill
            };

            Informes.Children.Add(wpfuiImage);
        }
    }
}