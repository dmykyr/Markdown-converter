namespace MarkdownConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) throw new Exception("any args was passed");

            string readingFilePath = args[0];
            string outFilePath = null;
            if (!File.Exists(readingFilePath)) throw new Exception("incorrect reading file path was passed");

            string fileContent = File.ReadAllText(readingFilePath);
            string html = MarkdownConverter.ToHtml(fileContent);

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--out" && i + 1 < args.Length)
                {
                    outFilePath = args[i + 1];
                    break;
                }
            }

            if (outFilePath == null)
            {
                Console.WriteLine(html);
            }
            else
            {
                File.WriteAllText(outFilePath, html);
            }
        }
    }
}