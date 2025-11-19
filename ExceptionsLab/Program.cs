using System;
using System.Data.SqlTypes;
using ExceptionsLab;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExceptionsLab
{
    class Lab
    {
        static void Main()
        {
            // string[] requiredFiles=new string[] {"10.txt","11.txt","12.txt","13.txt","14.txt","15.txt","16.txt","17.txt","18.txt","19.txt","20.txt","21.txt","22.txt","23.txt","24.txt","25.txt","26.txt","27.txt","28.txt","29.txt"};
            // string path=@"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\TxtFiles";
            // TxtFiles txtFiles=new TxtFiles(path,requiredFiles);
            // txtFiles.ReadFiles();
            FlowLayoutPanel fp = new FlowLayoutPanel();
            ImageFiles imageFiles= new ImageFiles(@"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\ImgFiles",
            new Regex(@"\.(jpg|jpeg|png|gif|bmp|webp|tiff)$",RegexOptions.IgnoreCase),
            @"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\MirroredImgs",
            fp);
            imageFiles.ReadFiles();
        }
    }
}