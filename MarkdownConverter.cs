using System.Text.RegularExpressions;

namespace MarkdownConverter
{
    public class MarkdownConverter
    {
        private static string PartToHtml(string markdown)
        {
            MarkdownPartCheck(markdown);

            string html = markdown;

            html = Regex.Replace(html, @"\*\*([^\s,.:;].+?[^\s,.:;])\*\*", "<b>$1</b>");

            html = Regex.Replace(html, @"_([^\s,.:;].+?[^\s,.:;])_", "<i>$1</i>");

            html = Regex.Replace(html, @"`([^\s,.:;].+?[^\s,.:;])`", "<tt>$1</tt>");

            html = Regex.Replace(html, @"(\r\n){2,}", "</p>\n<p>");

            return html;
        }

        private static void MarkdownPartCheck(string markdown)
        {
            List<string> list = new List<string>() { @"\*\*", "_", "`" };
            var matches = Regex.Matches(markdown, $"({string.Join('|', list)})" + "{2,}").ToList();
            if (!matches.All(match => match.Value.Length == 3 && match.Value.StartsWith("```")))
            {
                throw new Exception("Invalid markdown");
            }
        }

        public static string ToHtml(string markdownContent)
        {
            string html = String.Empty;
            var preformattedParts = Regex.Matches(markdownContent, @"```(.+?)```", RegexOptions.Singleline).ToList();

            int startIndex = 0;
            foreach (var preformattedPart in preformattedParts)
            {
                string convertingPart = markdownContent.Substring(startIndex, preformattedPart.Index - startIndex);
                html += PartToHtml(convertingPart);
                html += "<pre>" + preformattedPart.Groups[1].Value + "</pre>";
                startIndex = preformattedPart.Index + preformattedPart.Length;
            }
            html += PartToHtml(markdownContent.Substring(startIndex));

            return $"<p>{html}</p>";
        }
    }
}
