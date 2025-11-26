using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    try
                    {
                        int product = int.Parse(lines[0]) * int.Parse(lines[1]);
                        sum += product;
                        Console.WriteLine($"Product of file {filePath} is {product}");
                    }
                    catch (IndexOutOfRangeException)
                    {
                        WriteBadOverflowFiles(errorLog, filePath, "IndexOutOfRangeException");
                    }
                    catch (OverflowException)
                    {
                        WriteBadOverflowFiles(overflowLog, filePath, "OverflowException");
                    }
                    catch (FormatException)
                    {
                        WriteBadOverflowFiles(errorLog, filePath, "FormatException");
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"File {Path.GetFileName(filePath)} not found for reading.");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"Access denied to file {Path.GetFileName(filePath)}.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"IO Error reading file {Path.GetFileName(filePath)}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    WriteBadOverflowFiles(errorLog, filePath, $"Other error: {ex.Message}");
                }
            }
            Console.WriteLine($"Overall sum: {sum}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Error: The directory was not found");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: Access denied to the txt files directory");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO Error accessing txt files: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid path for txt files: {ex.Message}");
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
