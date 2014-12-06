using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console.Table
{
    public class MultiVerticalControl : IConsoleControl
    {
        protected class Item
        {
            public IConsoleControl Control { get; set; }
            public int BeginLine { get; set; }
            public int TotalLines { get; set; }
            public int EndLine { get; set; }
        }

    ;

        public MultiVerticalControl()
            : this(Enumerable.Empty<IConsoleControl>())
        {
        }

        public MultiVerticalControl(IEnumerable<IConsoleControl> items)
        {
            Items = items.Select(CreateItem).ToList();
        }

        protected List<Item> Items { get; set; }

        public void AddWidget(IConsoleControl consoleControl)
        {
            Items.Add(CreateItem(consoleControl));
        }

        public virtual void Adjust(int width)
        {
            var currentLine = 0;
            foreach (var item in Items)
            {
                item.Control.Adjust(width);
                var height = item.Control.CalculateHeight(width);
                item.BeginLine = currentLine;
                item.TotalLines = height;
                item.EndLine = currentLine + height;

                currentLine = item.EndLine;
            }
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            var elementToDraw = Items.FirstOrDefault(i => i.BeginLine <= line && line < i.EndLine);
            if (elementToDraw == null)
            {
                return;
            }

            elementToDraw.Control.Draw(writer, width, line - elementToDraw.BeginLine);
        }

        public int CalculateHeight(int width)
        {
            return Items.Sum(s => s.TotalLines);
        }

        protected static Item CreateItem(IConsoleControl consoleControl)
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