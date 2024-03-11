using Xunit;

namespace MarkdownConverter
{
    public class MarkdownConverterTests
    {
        [Fact]
        public void TestToHtml_ConvertsBoldCorrectly()
        {
            string markdown = "**bold ** text**";
            string expectedHtml = "<p><b>bold ** text</b></p>";

            string actualHtml = MarkdownConverter.ToHtml(markdown);

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Fact]
        public void TestToHtml_ConvertsItalicCorrectly()
        {
            string markdown = "_italic _ text_";
            string expectedHtml = "<p><i>italic _ text</i></p>";

            string actualHtml = MarkdownConverter.ToHtml(markdown);

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Fact]
        public void TestToHtml_ConvertsCodeCorrectly()
        {
            string markdown = "`code ` text`";
            string expectedHtml = "<p><tt>code ` text</tt></p>";

            string actualHtml = MarkdownConverter.ToHtml(markdown);

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Fact]
        public void TestToHtml_ConvertsPreformattedCorrectly()
        {
            string markdown = "```preformatted_ **text\n\n\n `for` check```";
            string expectedHtml = "<p><pre>preformatted_ **text\n\n\n `for` check</pre></p>";

            string actualHtml = MarkdownConverter.ToHtml(markdown);

            Assert.Equal(expectedHtml, actualHtml);
        }

        [Fact]
        public void TestToHtml_ThrowsExceptionOnUnclosedMarkwonTag()
        {
            string invalidMarkdown = "**bold text";

            Assert.Throws<InvalidMarkdownException>(() => MarkdownConverter.ToHtml(invalidMarkdown));
        }

        [Fact]
        public void TestToHtml_ThrowsExceptionOnConsecutiveMarkdownTags()
        {
            string invalidMarkdown = "**_bold text_**";

            Assert.Throws<InvalidMarkdownException>(() => MarkdownConverter.ToHtml(invalidMarkdown));
        }
    }
}
