using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = Wpf.Ui.Controls.Image;

namespace Cebv.features.formulario_reportes.presentation.senas_particulares;

public partial class SenasParticularesPage : Page
{
    public SenasParticularesPage()
    {
        InitializeComponent();
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (this.DataContext != null) 
        {
            Point posicion = e.GetPosition(RegionCuerpoImage);
            Color colorRegionCuerpo = this.GetPixelColor(RegionCuerpoImage, posicion);
            Color colorLado = this.GetPixelColor(LadoImage, posicion);

            ((dynamic)this.DataContext).ColorRegionCuerpo = colorRegionCuerpo.ToString().Substring(3);
            ((dynamic)this.DataContext).ColorLado = colorLado.ToString().Substring(3);
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

    private void VerificarInt(object sender, TextCompositionEventArgs e)
    {
        //throw new NotImplementedException();
    }
}