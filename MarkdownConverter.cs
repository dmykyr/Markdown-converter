using System.Text.RegularExpressions;

namespace MarkdownConverter
{
    public class MarkdownConverter
    {
        private static readonly Dictionary<string, Func<string, string>> _formatMethods =
            new Dictionary<string, Func<string, string>>
            {
                { "ansi", ToAnsi },
                { "html", ToHtml }
            };

        private static readonly Dictionary<string, string> _markdownlToAnsiDict = new Dictionary<string, string>()
        {
            { @"\*\*([^\s,.:;].+?[^\s,.:;])\*\*", "\u001b[1m$1\u001b[22m" },
            { @"_([^\s,.:;].+?[^\s,.:;])_", "\u001b[3m$1\u001b[23m" },
            { @"`([^\s,.:;].+?[^\s,.:;])`", "\u001b[7m$1\u001b[27m" },
        };

        private static readonly Dictionary<string, string> _markdownToHtmlDict = new Dictionary<string, string>()
        {
            { @"\*\*([^\s,.:;].+?[^\s,.:;])\*\*", "<start bold tag>$1<end bold tag>" },
            { @"_([^\s,.:;].+?[^\s,.:;])_", "<start italic tag>$1<end italic tag>" },
            { @"`([^\s,.:;].+?[^\s,.:;])`", "<sart code tag>$1<end code tag>" },
            { @"(\r\n){2,}", "<end of p>\n<start of p>" },
        };

        private static readonly List<string> _tags = new List<string>() { @"\*\*", "_", "`" };

        private static string ConvertPart(string markdownContent, Dictionary<string, string> markdownToReplacementDict)
        {
            MarkdownPartCheck(markdownContent);

            string convertedPart = markdownContent;
            foreach (var key in markdownToReplacementDict.Keys)
            {
                convertedPart = Regex.Replace(convertedPart, key, markdownToReplacementDict[key]);
            }
            return convertedPart;
        }

        private static void MarkdownPartCheck(string markdownContent)
        {
            CheckConsecutiveMarkdownTags(markdownContent);
            CheckUnclosedMarkdownTags(markdownContent);
        }

        private static void CheckUnclosedMarkdownTags(string markdownContent)
        {
            foreach (var tag in _tags)
            {
                var openTagMatches = Regex.Matches(markdownContent, $"({tag}[^\\s,.:;])");
                var closeTagMatches = Regex.Matches(markdownContent, $"([^\\s,.:;]{tag})");
                if (openTagMatches.Count != closeTagMatches.Count) throw new InvalidMarkdownException("Invalid markdown: unclosed tag was found");
            }
        }
        private static void CheckConsecutiveMarkdownTags(string markdownContent)
        {
            var matches = Regex.Matches(markdownContent, $"({string.Join('|', _tags)})" + "{2,}");
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
                html += ConvertPart(convertingPart, _markdownToHtmlDict);
                html += "<pre>" + preformattedPart.Groups[1].Value + "</pre>";
                startIndex = preformattedPart.Index + preformattedPart.Length;
            }
            html += ConvertPart(markdownContent.Substring(startIndex), _markdownToHtmlDict);

            return $"<p>{html}</p>";
        }

        public static string ToAnsi(string markdownContent)
        {
            string ansi = String.Empty;
            var preformattedParts = Regex.Matches(markdownContent, @"```(.+?)```", RegexOptions.Singleline).ToList();

            int startIndex = 0;
            foreach (var preformattedPart in preformattedParts)
            {
                string convertingPart = markdownContent.Substring(startIndex, preformattedPart.Index - startIndex);
                ansi += ConvertPart(convertingPart, _markdownlToAnsiDict);
                ansi += "\u001b[7m" + preformattedPart.Groups[1].Value + "\u001b[27m";
                startIndex = preformattedPart.Index + preformattedPart.Length;
            }
            ansi += ConvertPart(markdownContent.Substring(startIndex), _markdownlToAnsiDict);

            return ansi;
        }

        public static string ToSpecifiedFormat(string format, string markdownContent)
        {
            if (_formatMethods.TryGetValue(format, out var formatMethod))
            {
                return formatMethod(markdownContent);
            }

            throw new ArgumentException($"Unsupported format: {format}");
        }
    }
}