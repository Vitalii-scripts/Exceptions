using System.Windows.Forms;
using System.Drawing;
using System.IO;

public class ImageViewerForm : Form
{
    private FlowLayoutPanel panel = new FlowLayoutPanel();

    public ImageViewerForm()
    {
        this.Text = "Images";
        this.Width = 800;
        this.Height = 600;

        panel.Dock = DockStyle.Fill;
        this.Controls.Add(panel);

        LoadImagesFromDirectory(@"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\MirroredImgs");
    }

    private void LoadImagesFromDirectory(string dir)
    {
        try
        {
            foreach (string file in Directory.GetFiles(dir, "*.gif"))
            {
                try
                {
                    PictureBox box = new PictureBox();
                    box.Image = Image.FromFile(file);
                    box.SizeMode = PictureBoxSizeMode.Zoom;
                    box.Width = 150;
                    box.Height = 150;
                    panel.Controls.Add(box);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Image file {Path.GetFileName(file)} not found.");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine($"Image file {Path.GetFileName(file)} is too large or corrupted.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Invalid image file {Path.GetFileName(file)}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not load image {Path.GetFileName(file)}: {ex.Message}");
                }
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: Mirrored images directory not found.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access denied to mirrored images directory.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading images: {ex.Message}");
        }
    }
}
