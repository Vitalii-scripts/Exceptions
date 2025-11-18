using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ExceptionsLab;

public class TxtFiles(string folderPath, string[] requiredFiles)
{

    public void ReadFiles()
    {
        try
        {
            string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");
            var exisitingNames = new HashSet<string>(txtFiles.Select(Path.GetFileName), StringComparer.OrdinalIgnoreCase);
            var missingFiles = requiredFiles.Where(required => !exisitingNames.Contains(required)).ToList();
            switch (missingFiles)
            {
                case { Count: 0 }:
                    //nothing
                    break;
                case { Count : 20}:
                    Console.WriteLine("All files are missing");
                    return;
                default:
                    WriteMissingFiles(folderPath, missingFiles);
                    break;

            }
            string errorLog=Path.Combine(folderPath,"bad_data.txt");
            string overflowLog=Path.Combine(folderPath,"overflow.txt");
            int sum=0;
            foreach (string filePath in txtFiles)
            {
                    string[] lines = File.ReadAllLines(filePath);
                    try
                    {
                        int product = int.Parse(lines[0]) * int.Parse(lines[1]);
                        sum+=product;
                        Console.WriteLine($"Product of file {filePath} is {product}");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        WriteBadOverflowFiles(errorLog,filePath,"IndexOutOfRangeException");
                    }
                    catch (OverflowException)
                    {
                        WriteBadOverflowFiles(overflowLog,filePath,"OverflowException");
                    }
                    catch (FormatException)
                    {
                        WriteBadOverflowFiles(errorLog,filePath,"FormatException");
                    }
            }
            Console.WriteLine($"Overall sum: {sum}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found");
        }
    }
    static int WriteMissingFiles(string folderPath, IEnumerable<string> missing)
    {
        try
        {
        string reportFile = Path.Combine(folderPath, "no_file.txt");
        File.WriteAllLines(reportFile, missing);
        }
        catch(UnauthorizedAccessException)
        {
            Console.WriteLine("Can't acces file");
        }
        catch(IOException)
        {
            Console.WriteLine("Some input-output error");
        }
        return 1;
    }
     static int WriteBadOverflowFiles(string logFile, string filePath, string errorType)
    {
        try
        {
            string fileName = Path.GetFileName(filePath);
            File.AppendAllText(logFile, $"{errorType}: {fileName} ");
        }
        catch(UnauthorizedAccessException)
        {
            Console.WriteLine("Can't acces file");
        }
        catch(IOException)
        {
            Console.WriteLine("Some input-output errors");
        }
        return 1;
    }
}
