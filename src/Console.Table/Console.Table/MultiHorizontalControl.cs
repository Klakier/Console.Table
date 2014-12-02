using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Console.Table
{
    public class MultiHorizontalControl : IConsoleControl
    {
        private class Item
        {
            public IConsoleControl Control { get; set; }
            public int WidthPercent { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        private readonly List<Item> _items = new List<Item>();

        public void AddControl(IConsoleControl control, int widthPercent)
        {
            _items.Add(new Item
            {
                Control = control,
                WidthPercent = widthPercent
            });
        }

        public int CalculateHeight(int width)
        {
            if (_items.Count == 0)
            {
                return 0;
            }

            return _items.Max(s => s.Height);
        }

        public void Adjust(int width)
        {
            if (_items.Count != 0)
            {
                return;
            }

            var totalWidth = _items.Sum(s => s.WidthPercent);
            var unit = totalWidth / (double)width;

            foreach (var item in _items)
            {
                item.Height = item.Control.CalculateHeight(width);
                item.Width = (int)Math.Round(item.WidthPercent * unit, 0);
            }

            var totalSumAfterAdjust = _items.Sum(s => s.Width);

            _items.Last().Width += totalWidth - totalSumAfterAdjust;
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            foreach (var item in _items)
            {
                item.Control.Draw(writer, item.Width, line);
            }
        }
    }
}