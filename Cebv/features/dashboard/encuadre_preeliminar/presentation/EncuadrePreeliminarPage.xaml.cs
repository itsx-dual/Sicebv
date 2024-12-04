using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Cebv.core.util.reporte.viewmodels;
using Cebv.core.util.snackbar;
using Cebv.features.formulario_cebv.senas_particulares.presentation;
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
        if (DataContext == null) return;
        if (sender is not Image image) return;
        if (image.DataContext is not SenasParticularesViewModel viewmodel) return;

        var posicion = e.GetPosition(Mascara);

        var colorVista = this.GetPixelColor(Vista, posicion);
        var colorLado = this.GetPixelColor(Lado, posicion);
        var colorRegionCuerpo = this.GetPixelColor(Region, posicion);

        viewmodel.ColorRegionCuerpo = colorRegionCuerpo.ToString()[3..];
        viewmodel.ColorLado = colorLado.ToString()[3..];
        viewmodel.ColorVista = colorVista.ToString()[3..];
    }

    private Color GetPixelColor(Image image, Point position)
    {
        // Create a RenderTargetBitmap of the same size as the Image
        RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)image.ActualWidth, (int)image.ActualHeight,
            96, 96, PixelFormats.Default);
        renderTargetBitmap.Render(image);

        // Create a CroppedBitmap to get the pixel color at the specified position
        CroppedBitmap croppedBitmap =
            new CroppedBitmap(renderTargetBitmap, new Int32Rect((int)position.X, (int)position.Y, 1, 1));

        // Create a byte array to hold the pixel color
        byte[] pixelColor = new byte[4];

        // Copy the pixel color from the CroppedBitmap to the byte array
        croppedBitmap.CopyPixels(pixelColor, 4, 0);

        // Return the pixel color as a Color object
        return Color.FromRgb(pixelColor[2], pixelColor[1], pixelColor[0]);
    }

    private void Search(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox) return;
        var items = comboBox.Items.Cast<dynamic>();
        if (!items.Any()) return;
        comboBox.ClearValue(Border.BorderBrushProperty);

        var esValido = items.Any(x => x.ToString() == comboBox.Text);

        comboBox.SelectedItem = esValido
            ? items.First(x => x.ToString() == comboBox.Text)
            : items.FirstOrDefault(x => x.ToString().Contains(comboBox.Text, StringComparison.OrdinalIgnoreCase));

        if (comboBox.SelectedItem is not null) return;
        comboBox.BorderBrush = new SolidColorBrush(Colors.Orange);
        _snackBarService.Show("No se ha encontrado el valor.",
            $"El valor \"{comboBox.Text}\" no se encuentra en la lista de valores posibles.",
            ControlAppearance.Caution,
            new SymbolIcon(SymbolRegular.Warning28),
            new TimeSpan(0, 0, 10));
    }

    private void Imagenes_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is not ListView listview) return;
        if (listview.DataContext is not viewmodel.EncuadrePreeliminarViewModel dataContext) return;
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

    private void DatePicker_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (sender is not DatePicker dt) return;

        var justNumbers = new String(dt.Text.Where(Char.IsDigit).ToArray());
        if (justNumbers.Length != 8) return;
        var newDate = justNumbers.Insert(2, "/").Insert(5, "/");
        
        try
        {
            dt.SelectedDate = DateTime.Parse(newDate);
        }
        catch (Exception ex)
        {
            dt.Text = "";
        }
    }

    private void DatePicker_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (sender is not DatePicker datePicker) return;
        var regex = new Regex("[^0-9/]");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void MinDesaparicion_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (MinDesaparicion.Text.Length>1)
        {
            AclaracionFecha.Focus();
        }
        if (MinDesaparicion.Text.Length==0)
        {
            HoraDesaparicion.Focus();
        }
    }

    private void HoraDesaparicion_TextChanged(object sender, RoutedEventArgs routedEventArgs)
    {
        if (HoraDesaparicion.Text.Length>1)
        {
            MinDesaparicion.Focus();
        }
        /* ESTE POR SI SE QUIERE QUE AL BORRAR SE VAYA AL CAMPO ANTERIOR PERO A MI GUSTO ESTA MEH
        if (HoraDesaparicion.Text.Length==0)
        {
            FechaDesaparicion.Focus();
        }*/
        
    }
}