
using System;
using System.IO;

namespace Console.Table
{
    public class TextControl : IConsoleControl
    {
        private readonly string _text;

        public TextControl(string text)
        {
            _text = text;
        }

        public int CalculateHeight(int width)
        {
            return _text.Length / width + 1;
        }

        public void Adjust(int widht)
        {
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            var textToDrawn = GetTextToDraw(width, line);

            var stringFormat = "{0," + width + "}";
            writer.Write(stringFormat, textToDrawn);
        }

        private string GetTextToDraw(int width, int line)
        {
            var start = width * line;
            var end = start + width;

            if (start >= _text.Length)
            {
                return string.Empty;
            }

            end = Math.Min(_text.Length, end);

            return _text.Substring(start, end - start);
        }
    }
}