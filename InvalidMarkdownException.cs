namespace MarkdownConverter
{
    public class InvalidMarkdownException : Exception
    {
        public InvalidMarkdownException() { }

        public InvalidMarkdownException(string message) : base(message) { }

        public InvalidMarkdownException(string message, Exception inner) : base(message, inner) { }
    }
}
