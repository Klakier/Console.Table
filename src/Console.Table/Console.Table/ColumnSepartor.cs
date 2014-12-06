using System.IO;

namespace Console.Table
{
    public class ColumnSepartor : IConsoleControl
    {
        public static readonly ColumnSepartor Default = new ColumnSepartor();
        public const int Width = 1;

        private ColumnSepartor() { }

        public int CalculateHeight(int width)
        {
            return Width;
        }

        public void Adjust(int widht)
        {
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            writer.Write("|");
        }
    }
}