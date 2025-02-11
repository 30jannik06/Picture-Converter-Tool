using ImageMagick;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Bitte ziehe Bilddateien auf das Tool.");
            return;
        }

        int totalFiles = args.Length;
        int processedFiles = 0;

        foreach (var filePath in args)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            if (extension == ".jpg" || extension == ".jpeg" || extension == ".bmp" || extension == ".gif" || extension == ".tiff" || extension == ".avif")
            {
                string outputFile = Path.ChangeExtension(filePath, ".png");
                ConvertToPng(filePath, outputFile);
                processedFiles++;
                ShowProgress(processedFiles, totalFiles);
            }
            else
            {
                Console.WriteLine($"Dateiformat {extension} wird nicht unterstützt.");
            }

            await Task.Delay(100);  // Simuliert eine kurze Verzögerung für das Konvertieren
        }

        Console.WriteLine("\nAlle Bilder wurden konvertiert.");
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

    static void ShowProgress(int processedFiles, int totalFiles)
    {
        double progress = (double)processedFiles / totalFiles;
        int progressBarWidth = 50;
        int progressChars = (int)(progress * progressBarWidth);

        string progressBar = new string('=', progressChars) + new string('-', progressBarWidth - progressChars);
        Console.Write($"\r[{progressBar}] {processedFiles}/{totalFiles} abgeschlossen");
    }
}
