using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console.Table
{
    public class MultiVerticalControl : IConsoleControl
    {
        private class Item
        {
            public IConsoleControl Control { get; set; }
            public int BeginLine { get; set; }
            public int TotalLines { get; set; }
            public int EndLine { get; set; }
        }

        private readonly List<Item> _items;

        public MultiVerticalControl(IEnumerable<IConsoleControl> items)
        {
            _items = items.Select(CreateItem).ToList();
        }

        public void AddWidget(IConsoleControl consoleControl)
        {
            _items.Add(CreateItem(consoleControl));
        }

        public void Adjust(int width)
        {
            var currentLine = 0;
            foreach (var item in _items)
            {
                var height = item.Control.CalculateHeight(width);
                item.BeginLine = currentLine;
                item.TotalLines = height;
                item.EndLine = currentLine + height + 1;

                currentLine += item.EndLine;
            }
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            var elementToDraw = _items.FirstOrDefault(i => i.BeginLine <= line && line < i.EndLine);
            if (elementToDraw == null)
            {
                return;
            }

            elementToDraw.Control.Draw(writer, width, line);
        }

        public int CalculateHeight(int width)
        {
            return _items.Sum(s => s.TotalLines);
        }

        private static Item CreateItem(IConsoleControl consoleControl)
        {
            return new Item
            {
                Control = consoleControl,
                BeginLine = 0,
                EndLine = 0,
                TotalLines = 0
            };
        }
    }
}