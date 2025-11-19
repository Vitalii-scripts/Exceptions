using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExceptionsLab;

public class ImageFiles(string path,Regex requiredFiles,string mirroredPath, FlowLayoutPanel flowPanel)
{
    private readonly FlowLayoutPanel _flowPanel = flowPanel;
    public void ReadFiles()
    {
        // Regex imgRegex=new Regex(@"\.(jpg|jpeg|png|gif|bmp|webp|tiff)$",RegexOptions.IgnoreCase);
        try
        {
            string[] files=Directory.GetFiles(path);
            var imgFiles=files.Where(file => requiredFiles.IsMatch(Path.GetFileName(file))).ToList();
            foreach(string file in imgFiles)
            {
                Bitmap originalImage=new Bitmap(file);
                Bitmap mirroredImage=(Bitmap)originalImage.Clone();
                mirroredImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                mirroredImage.Save(mirroredPath,ImageFormat.Gif);
                originalImage.Dispose();
                mirroredImage.Dispose();
            }
            var mirroredImages=Directory.GetFiles(mirroredPath);
            foreach(var file in mirroredImages)
            {
                PictureBox pb = new PictureBox();
                pb.Image = Image.FromFile(file);
                pb.SizeMode=PictureBoxSizeMode.Zoom;
                pb.Width=150;
                pb.Height=150;
                _flowPanel.Controls.Add(pb);
            }
        }
        catch(DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found");
        }
    }
}
