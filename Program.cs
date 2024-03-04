using System.Linq;
using System.Text.RegularExpressions;

namespace Lab1
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

            string html = ConvertMarkdownContentToHtml(fileContent);

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

        static string ConvertMarkdownPartToHtml(string markdown)
        {
            MarkdownPartCheck(markdown);

            string html = markdown;

            html = Regex.Replace(html, @"\*\*(.+?)\*\*", "<b>$1</b>");

            html = Regex.Replace(html, @"_(.+?)_", "<i>$1</i>");

            html = Regex.Replace(html, @"`(.+?)`", "<tt>$1</tt>");

            html = Regex.Replace(html, @"(\r\n){2,}", "</p>\n<p>");

            return html;
        }

        static void MarkdownPartCheck (string markdown)
        {
            List<string> list = new List<string>() { @"\*\*", "_", "`" };
            var matches = Regex.Matches(markdown, $"({string.Join('|', list)})" + "{2,}").ToList();
            if (!matches.All(match => match.Value.Length == 3 && match.Value.StartsWith("```")))
            {
                throw new Exception("Invalid markdown");
            }
        }
        static string ConvertMarkdownContentToHtml(string markdownContent)
        {
            string html = String.Empty;
            var preformattedParts = Regex.Matches(markdownContent, @"```(.+?)```", RegexOptions.Singleline).ToList();

            int startIndex = 0;
            foreach (var preformattedPart in preformattedParts)
            {
                string convertingPart = markdownContent.Substring(startIndex, preformattedPart.Index - startIndex);
                html += ConvertMarkdownPartToHtml(convertingPart);
                html += "<pre>" + preformattedPart.Groups[1].Value + "</pre>";
                startIndex = preformattedPart.Index + preformattedPart.Length;
            }
            html += ConvertMarkdownPartToHtml(markdownContent.Substring(startIndex));

            return $"<p>{html}</p>";
        }
    }
}