using ImageMagick;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Bitte ziehe eine oder mehrere Bilddateien auf das Tool.");
            return;
        }

        foreach (var filePath in args)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".gif" || extension == ".tiff" || extension == ".avif")
            {
                string outputFile = Path.ChangeExtension(filePath, ".png");
                ConvertToPng(filePath, outputFile);
                Console.WriteLine($"Bild {filePath} wurde nach {outputFile} konvertiert.");
            }
            else
            {
                Console.WriteLine($"Bild {filePath} hat ein nicht unterstütztes Format.");
            }
        }
    }

    static void ConvertToPng(string inputFile, string outputFile)
    {
        try
        {
            // Bild mit Magick.NET laden
            using (var image = new MagickImage(inputFile))
            {
                // Setze das Ausgabeformat auf PNG
                image.Format = MagickFormat.Png;

                // Bild speichern
                image.Write(outputFile);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler bei der Konvertierung von {inputFile}: {ex.Message}");
        }
    }
}
