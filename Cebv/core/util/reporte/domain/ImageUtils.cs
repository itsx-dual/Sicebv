using System.IO;
using System.Windows.Media.Imaging;

namespace Cebv.core.util.reporte.domain;

public abstract class ImageUtils
{
    // Metodo generado con Gemini
    public static Stream BitmapImageToStream(BitmapImage bitmapImage)
    {
        MemoryStream memoryStream = new MemoryStream(); // Use MemoryStream for in-memory operations
        BitmapEncoder encoder = new PngBitmapEncoder(); // Or JpegBitmapEncoder
        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
        encoder.Save(memoryStream);
        memoryStream.Position = 0; // Reset stream position to the beginning for reading
        return memoryStream;
    }

    public static BitmapImage Base64StringToBitmapImage(string base64String)
    {
        try
        {
            // Convert base64 string to byte array
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Create a memory stream from the byte array
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                // Create the BitmapImage
                BitmapImage bitmapImage = new BitmapImage();

                // Important: BeginInit/EndInit for proper image initialization
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption =
                    BitmapCacheOption.OnLoad; // Load image fully into memory for better performance
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Prevent cross-thread access issues in WPF

                return bitmapImage;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., invalid base64, corrupt image data)
            Console.WriteLine($"Error decoding base64 image: {ex.Message}");
            return null; // Or return a default image
        }
    }

    public static string BitmapImageToBase64(BitmapImage bitmapImage)
    {
        if (bitmapImage == null) return null;

        // Encode to the desired format (e.g., Png, Jpeg)
        BitmapEncoder encoder = new PngBitmapEncoder(); // Or JpegBitmapEncoder for JPEG
        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

        // Use MemoryStream to store the encoded data
        using (MemoryStream memoryStream = new MemoryStream())
        {
            encoder.Save(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();

            // Convert bytes to base64 string
            return Convert.ToBase64String(imageBytes);
        }
    }
}