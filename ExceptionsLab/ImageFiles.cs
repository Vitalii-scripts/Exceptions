using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace ExceptionsLab;

public class ImageFiles(string path,Regex requiredFiles,string mirroredPath)
{
    public void ReadFiles()
    {
        try
        {
            string[] files = Directory.GetFiles(path);
            CreateMirroredImages(FilterFiles(files));
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access denied to the directory");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO Error: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid path: {ex.Message}");
        }
    }
    public List<string> FilterFiles(string[] files)
    {
        return files.Where(file => requiredFiles.IsMatch(Path.GetFileName(file))).ToList();
    }
    public void CreateMirroredImages(List<string> filteredFiles)
    {
        foreach(string file in filteredFiles)
        {
            try
            {
                Bitmap originalImage = new Bitmap(file);
                Bitmap mirroredImage = (Bitmap)originalImage.Clone();
                mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                string fileName = Path.GetFileNameWithoutExtension(file);
                string outputFile = Path.Combine(mirroredPath, fileName + " -mirrored.gif");
                mirroredImage.Save(outputFile, ImageFormat.Gif);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error processing image {Path.GetFileName(file)}: Invalid image file. {ex.Message}");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine($"Error processing image {Path.GetFileName(file)}: Image file too large or corrupted.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Error processing image {Path.GetFileName(file)}: Access denied.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO Error processing image {Path.GetFileName(file)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error processing image {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }
}
