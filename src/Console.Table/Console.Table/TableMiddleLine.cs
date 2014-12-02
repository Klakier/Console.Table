using System;
using System.IO;

namespace Console.Table
{
    public class TableMiddleLine : IConsoleControl
    {
        public int CalculateHeight(int width)
        {
            return 1;
        }

        public void Adjust(int widht)
        {
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            writer.Write('├');
            writer.Write(new string('─', Math.Max(0, width - 2)));
            writer.Write('┤');
        }
    }
}