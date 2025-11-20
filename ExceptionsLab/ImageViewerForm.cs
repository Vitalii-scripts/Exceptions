using System.Windows.Forms;
using System.Drawing;

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
        foreach (string file in Directory.GetFiles(dir, "*.gif"))
        {
            PictureBox box = new PictureBox();
            box.Image = Image.FromFile(file);
            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.Width = 150;
            box.Height = 150;
            panel.Controls.Add(box);
        }
    }
}
