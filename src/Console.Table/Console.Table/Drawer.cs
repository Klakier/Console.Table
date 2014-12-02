using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console.Table
{
    public class Drawer
    {
        private readonly List<IConsoleControl> _controls;

        public Drawer(IEnumerable<IConsoleControl> controls)
        {
            _controls = controls.ToList();
        }

        public void Draw(TextWriter writer, int width)
        {
            _controls.ForEach(e => e.Adjust(width));
            var totalLines = _controls.Sum(e => e.CalculateHeight(width));
            for (var i = 0; i < totalLines; i++)
            {
                _controls[i].Draw(writer, width, i);
                writer.WriteLine();
            }

            writer.WriteLine();
        }
    }
}