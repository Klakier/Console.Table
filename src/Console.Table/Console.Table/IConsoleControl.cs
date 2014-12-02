using System.IO;

namespace Console.Table
{
    public interface IConsoleControl
    {
        int CalculateHeight(int width);

        void Adjust(int widht);

        void Draw(TextWriter writer, int width, int line);
    }
}