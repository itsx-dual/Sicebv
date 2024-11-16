using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Image = Wpf.Ui.Controls.Image;
using ListView = Wpf.Ui.Controls.ListView;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarPage : Page
{
    private static ISnackbarService _snackBarService = App.Current.Services.GetService<ISnackbarService>()!;
    
    public EncuadrePreeliminarPage()
    {
        InitializeComponent();
    }
    
    private void TelefonosMoviles_OnFilter(object sender, FilterEventArgs e)
    {
        if (e.Item is not Telefono telefono) return;
        e.Accepted = telefono.EsMovil ?? false;
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        
            if (this.DataContext == null) return;
        
            Point posicion = e.GetPosition(Mascara);

            Color colorVista = this.GetPixelColor(Vista, posicion);
            Color colorLado = this.GetPixelColor(Lado, posicion);
            Color colorRegionCuerpo = this.GetPixelColor(Region, posicion);
            
            ((EncuadrePreeliminarViewModel) DataContext).ColorRegionCuerpo = colorRegionCuerpo.ToString().Substring(3);
            ((EncuadrePreeliminarViewModel) DataContext).ColorLado = colorLado.ToString().Substring(3);
            ((EncuadrePreeliminarViewModel) DataContext).ColorVista = colorVista.ToString().Substring(3);
            
        
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
    
    public void Search(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox) return;
        var items = comboBox.Items.Cast<dynamic>(); 
        if (!items.Any()) return;
        comboBox.ClearValue(Border.BorderBrushProperty);
        
        var esValido = items.Any(x => x.ToString() == comboBox.Text);

        comboBox.SelectedItem = esValido ? items.First(x => x.ToString() == comboBox.Text) :
                                items.FirstOrDefault(x => x.ToString().Contains(comboBox.Text, StringComparison.OrdinalIgnoreCase));

        if (comboBox.SelectedItem is not null) return;
        comboBox.BorderBrush = new SolidColorBrush(Colors.Orange);
        _snackBarService.Show("No se ha encontrado el valor.", 
                              $"El valor \"{comboBox.Text}\" no se encuentra en la lista de valores posibles.",
                              ControlAppearance.Caution,
                              new SymbolIcon(SymbolRegular.Warning28),
                              new TimeSpan(0,0,10));
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (sender is not DatePicker datePicker) return;
        
    }

    private void Imagenes_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ListView listview) return;
        if (listview.DataContext is not EncuadrePreeliminarViewModel dataContext) return;
        var elements = listview.Items.Cast<dynamic>();
        if (!elements.Any()) return;

        if (e.Key == Key.Delete)
        {
            // Trato de respetar MVVM lo mas posible.
            dataContext.DeleteDesaparecidoImagenCommand.Execute(listview.SelectedItem);
        }
    }

    private void EventSetter_OnHandler(object sender, KeyEventArgs e)
    {
        if (sender is not ComboBox comboBox) return;
        if (e.Key != Key.Space || comboBox.Text is not ("" or " ")) return;
        
        comboBox.Text = string.Empty;
        comboBox.IsDropDownOpen = true;
    }
}