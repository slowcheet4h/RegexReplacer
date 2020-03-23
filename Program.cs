using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RegexReplacer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter Regex: ");
            string pattern = Console.ReadLine();
            Console.Write("Enter Path: ");
            string path = Console.ReadLine();
            Console.Write("Search Pattern (put * for all files): ");
            string searchPattern = Console.ReadLine();
            Console.Write("Replace (if you want to remove only press enter): ");
            string replace = Console.ReadLine();
            
            Regex regex = new Regex(pattern);
            
            string[] files = Directory.GetFileSystemEntries(path, searchPattern, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string read = File.ReadAllText(file);
                if (regex.IsMatch(read))
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        File.WriteAllText(file, regex.Replace(read, replace));
                        Console.WriteLine("{OK} - Matched (" + file.Replace(path, "") + ")");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{ERROR} - " + file.Replace(path, "") + " Error: " + ex.Message);
                    }
                }
                else
                {
                    
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("{IGNORE} - No Match found (" + file.Replace(path, "") + ")");
                }
            }
            

        }
    }
}