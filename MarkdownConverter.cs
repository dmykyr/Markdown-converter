using System.Text.RegularExpressions;

namespace MarkdownConverter
{
    public class MarkdownConverter
    {
        private static string PartToHtml(string markdownContent)
        {
            MarkdownPartCheck(markdownContent);

            string html = Regex.Replace(markdownContent, @"\*\*([^\s,.:;].+?[^\s,.:;])\*\*", "<b>$1</b>");

            html = Regex.Replace(html, @"_([^\s,.:;].+?[^\s,.:;])_", "<i>$1</i>");

            html = Regex.Replace(html, @"`([^\s,.:;].+?[^\s,.:;])`", "<tt>$1</tt>");

            html = Regex.Replace(html, @"(\r\n){2,}", "</p>\n<p>");

            return html;
        }

        private static void MarkdownPartCheck(string markdownContent)
        {
            CheckConsecutiveMarkdownTags(markdownContent);
            CheckUnclosedMarkdownTags(markdownContent);
        }

        private static void CheckUnclosedMarkdownTags(string markdownContent)
        {
            List<string> tags = new List<string>() { @"\*\*", "_", "`" };
            foreach (var tag in tags)
            {
                var openTagMatches = Regex.Matches(markdownContent, $"({tag}[^\\s,.:;])");
                var closeTagMatches = Regex.Matches(markdownContent, $"([^\\s,.:;]{tag})");
                if (openTagMatches.Count != closeTagMatches.Count) throw new InvalidMarkdownException("Invalid markdown: unclosed tag was found");
            }
        }
        private static void CheckConsecutiveMarkdownTags(string markdownContent)
        {
            List<string> tags = new List<string>() { @"\*\*", "_", "`" };
            var matches = Regex.Matches(markdownContent, $"({string.Join('|', tags)})" + "{2,}");
            if (matches.Count != 0) throw new InvalidMarkdownException("Invalid markdown: consecutive markdown tags was found");
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
