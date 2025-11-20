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
            string[] files=Directory.GetFiles(path);
            CreateMirroredImages(FilterFiles(files));
        }
        catch(DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found");
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
                Bitmap originalImage=new Bitmap(file);
                Bitmap mirroredImage=(Bitmap)originalImage.Clone();
                mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                string fileName=Path.GetFileNameWithoutExtension(file);
                string outputFile=Path.Combine(mirroredPath,fileName+ " -mirrored.gif");
                mirroredImage.Save(outputFile,ImageFormat.Gif);
            }
    }
}
