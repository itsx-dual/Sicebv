using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.formulario_cebv.datos_de_localizacion.presentation;

public partial class DatosLocalizacionPage : Page
{
    public DatosLocalizacionPage()
    {
        InitializeComponent();
    }
    
    
    private void PruebasVidaFiles(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (PruebasVida == null) return;
        
        PruebasVida.Children.Clear();
        
        
        var archivos = ((DatosLocalizacionViewModel)DataContext).OpenedPruebaVidaPath;
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

            PruebasVida.Children.Add(wpfuiImage);
        }
    }
    
    private void IdentificacionesOficialesFiles(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (IdentificacionesOficiales == null) return;
        
        IdentificacionesOficiales.Children.Clear();
        
        
        var archivos = ((DatosLocalizacionViewModel)DataContext).OpenedIdentificacionOficialPath;
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

            IdentificacionesOficiales.Children.Add(wpfuiImage);
        }
    }
}