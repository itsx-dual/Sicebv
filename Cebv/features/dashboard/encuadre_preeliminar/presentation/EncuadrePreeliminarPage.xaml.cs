using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Controls;
using Image = Wpf.Ui.Controls.Image;
using ListView = Wpf.Ui.Controls.ListView;

namespace Cebv.features.dashboard.encuadre_preeliminar.presentation;

public partial class EncuadrePreeliminarPage : Page
{
    private ISnackbarService _notification = App.Current.Services.GetService<ISnackbarService>()!;
    
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
    
    public void Search(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox) return;
        var items = comboBox.Items.Cast<dynamic>(); 
        if (!items.Any()) return;
        
        var esValido = items.Any(x => x.ToString() == comboBox.Text);

        if (esValido)
        {
            comboBox.SelectedItem = items.First(x => x.ToString() == comboBox.Text);
        }
        else
        {
            comboBox.SelectedItem = items.FirstOrDefault(x => x.ToString().Contains(comboBox.Text, StringComparison.OrdinalIgnoreCase));

            if (comboBox.SelectedItem is not null) return;
            if (items.Any(x => x.ToString().Contains("no especifica", StringComparison.OrdinalIgnoreCase)))
            {
                comboBox.SelectedItem = items.FirstOrDefault(x => x.ToString().Contains("no especifica", StringComparison.OrdinalIgnoreCase));
                _notification.Show("No se encontro el elemento.",
                    $"El valor: \"{comboBox.Text}\" no se encuentra en la lista de elementos seleccionables, se uso el valor de \"No especifica\" por defecto.",
                    ControlAppearance.Secondary,
                    new SymbolIcon(SymbolRegular.TextBulletListSquareWarning24),
                    TimeSpan.FromSeconds(3));
            }
            
            if (comboBox.SelectedItem is not null) return;
            _notification.Show("Se escribio un valor invalido.",
                $"El valor: \"{comboBox.Text}\" no se encuentra en la lista de elementos seleccionables",
                ControlAppearance.Secondary,
                new SymbolIcon(SymbolRegular.TextBulletListSquareWarning24),
                TimeSpan.FromSeconds(3));
            comboBox.Text = null;
        }
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (sender is not DatePicker datePicker) return;
        
    }

    private void Imagenes_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ListView listview) return;
        if (listview.DataContext is not EncuadrePreeliminarViewModel dataContext) return;
        if (!listview.Items.Cast<dynamic>().Any()) return;

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