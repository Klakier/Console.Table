using System.IO;

namespace Console.Table
{
    public interface IDrawer
    {
        void Draw(TextWriter writer, int width, int line);
    }
}