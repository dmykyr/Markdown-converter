using System.Text.RegularExpressions;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) throw new Exception("any args was passed");

            string readingFilePath = args[0];
            if (!File.Exists(readingFilePath)) throw new Exception("incorrect reading file path was passed");

            string fileContent = File.ReadAllText(readingFilePath);
            Console.WriteLine(fileContent);
        }
    }
}
