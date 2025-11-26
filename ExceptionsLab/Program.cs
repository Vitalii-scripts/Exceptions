using System;
using System.Data.SqlTypes;
using ExceptionsLab;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExceptionsLab
{
    class Lab
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Console.WriteLine("Block to run:");
                int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    try
                    {
                        Block1();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Block1: {ex.Message}");
                    }
                    break;
                case 2:
                    try
                    {
                        Block2();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Block2: {ex.Message}");
                    }
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input: Please enter a number (1 or 2).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        static void Block1()
        {
            string[] requiredFiles=new string[] {"10.txt","11.txt","12.txt","13.txt","14.txt","15.txt","16.txt","17.txt","18.txt","19.txt","20.txt","21.txt","22.txt","23.txt","24.txt","25.txt","26.txt","27.txt","28.txt","29.txt"};
            string path=@"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\TxtFiles";
            TxtFiles txtFiles=new TxtFiles(path,requiredFiles);
            txtFiles.ReadFiles();
        }
        static void Block2()
        {
            ImageFiles imageFiles= new ImageFiles(@"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\ImgFiles",
            new Regex(@"\.(jpg|jpeg|png|gif|bmp|webp|tiff)$",RegexOptions.IgnoreCase),
            @"C:\VSProjects\Labs\Grade - 2\Git\Exceptions\ExceptionsLab\MirroredImgs");
            imageFiles.ReadFiles();
            Application.EnableVisualStyles();
            Application.Run(new ImageViewerForm());
        }
    }
}
