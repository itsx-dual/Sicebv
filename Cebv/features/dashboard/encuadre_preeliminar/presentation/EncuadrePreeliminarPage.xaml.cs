using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.viewmodels;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarPage : Page
{
    public EncuadrePreeliminarPage()
    {
        InitializeComponent();
    }
    
    private void TelefonosMoviles_OnFilter(object sender, FilterEventArgs e)
    {
        var item = e.Item as Telefono;
        e.Accepted = (bool) item?.EsMovil!;
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (this.DataContext != null) 
        {
            Point posicion = e.GetPosition(RegionCuerpoImage);
            Color colorRegionCuerpo = this.GetPixelColor(RegionCuerpoImage, posicion);
            Color colorLado = this.GetPixelColor(LadoImage, posicion);

            ((EncuadrePreeliminarViewModel) DataContext).ColorRegionCuerpo = colorRegionCuerpo.ToString().Substring(3);
            ((EncuadrePreeliminarViewModel) DataContext).ColorLado = colorLado.ToString().Substring(3);
        }
    }
    
    private Color GetPixelColor(Image image, Point position)
    {
        // Create a RenderTargetBitmap of the same size as the Image
        RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Default);
        renderTargetBitmap.Render(image);

        // Create a CroppedBitmap to get the pixel color at the specified position
        CroppedBitmap croppedBitmap = new CroppedBitmap(renderTargetBitmap, new Int32Rect((int)position.X, (int)position.Y, 1, 1));

        // Create a byte array to hold the pixel color
        byte[] pixelColor = new byte[4];

        // Copy the pixel color from the CroppedBitmap to the byte array
        croppedBitmap.CopyPixels(pixelColor, 4, 0);

        // Return the pixel color as a Color object
        return Color.FromRgb(pixelColor[2], pixelColor[1], pixelColor[0]);
    }

    private void Imagenes_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext == null) return;
        if (Imagenes == null) return;
        
        //Imagenes.Children.Clear();
        
        Console.WriteLine("DataContext Changed");
        
        var archivos = ((EncuadrePreeliminarViewModel)DataContext).Files;
        if (archivos == null) return;

        foreach (var imagen in archivos)
        {
            Image wpfuiImage = new()
            {
                Source = new BitmapImage(new Uri(imagen)),
                CornerRadius = new CornerRadius(8),
                Margin = new Thickness(5),
                ClipToBounds = true,
                Width = 300,
                Height = 300,
                Stretch = Stretch.UniformToFill
            };

            //Imagenes.Children.Add(wpfuiImage);
        }
    }

    private void Grid_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var grid = sender as Grid;
    }
}