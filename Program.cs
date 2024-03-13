namespace MarkdownConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) throw new ArgumentNullException("any args was passed");

            string readingFilePath = args[0];
            if (!File.Exists(readingFilePath)) throw new ArgumentException("incorrect reading file path was passed");

            string outFilePath = String.Empty;
            string format = String.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--out" && i + 1 < args.Length)
                {
                    outFilePath = args[i + 1];
                }
                if (args[i] == "--format" && i + 1 < args.Length)
                {
                    format = args[i + 1];
                }
            }

            string fileContent = File.ReadAllText(readingFilePath);


            if (String.IsNullOrEmpty(outFilePath))
            {
                Console.WriteLine(
                    MarkdownConverter.ToSpecifiedFormat(String.IsNullOrEmpty(format) ? "ansi" : format, fileContent));
            }
            else
            {
                File.WriteAllText(
                    outFilePath,
                    MarkdownConverter.ToSpecifiedFormat(String.IsNullOrEmpty(format) ? "html" : format, fileContent));
            }
        }
    }
}