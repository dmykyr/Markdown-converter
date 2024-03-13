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


        [Fact]
        public void TestToAnsi_ConvertsBoldCorrectly()
        {
            string markdown = "**bold ** text**";
            string expected = "\u001b[1mbold ** text\u001b[22m";

            string actual = MarkdownConverter.ToAnsi(markdown);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToAnsi_ConvertsItalicCorrectly()
        {
            string markdown = "_italic _ text_";
            string expected = "\u001b[3mitalic _ text\u001b[23m";

            string actual = MarkdownConverter.ToAnsi(markdown);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToAnsi_ConvertsCodeCorrectly()
        {
            string markdown = "`code ` text`";
            string expected = "\u001b[7mcode ` text\u001b[27m";

            string actual = MarkdownConverter.ToAnsi(markdown);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToAnsi_ConvertsPreformattedCorrectly()
        {
            string markdown = "```preformatted_ **text\n\n\n `for` check```";
            string expected = "\u001b[7mpreformatted_ **text\n\n\n `for` check\u001b[27m";

            string actual = MarkdownConverter.ToAnsi(markdown);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestToSpecifiedFormat_ThrowsArgumentExceptionOnFormat()
        {
            string markdown = "text";
            string invalidFormat = "invalid format";

            Assert.Throws<ArgumentException>(() => MarkdownConverter.ToSpecifiedFormat(invalidFormat, markdown));
        }

        [Fact]
        public void TestToSpecifiedFormat_SelectCorrectFormat()
        {
            string markdown = "**bold** text for _test_";
            string expectedAnsi = "\u001b[1mbold\u001b[22m text for \u001b[3mtest\u001b[23m";
            string expectedHtml = "<p><b>bold</b> text for <i>test</i></p>";

            string actualAnsi = MarkdownConverter.ToSpecifiedFormat("ansi", markdown);
            string actualHtml = MarkdownConverter.ToSpecifiedFormat("html", markdown);
            Assert.Equal(expectedAnsi, actualAnsi);
            Assert.Equal(expectedHtml, actualHtml);
        }
    }
}