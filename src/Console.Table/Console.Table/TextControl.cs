
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
            throw new NotImplementedException();
        }

        public void GetDrawer(TextWriter textWriter, int width, int line)
        {
            var start = width*line;
            var end = start + width;

            if (start >= _text.Length)
            {
                return;
            }

            end = Math.Min(_text.Length, end);

            var textToDrawn = _text.Substring(start, end - start);

            textWriter.Write(textToDrawn);
        }
    }
}