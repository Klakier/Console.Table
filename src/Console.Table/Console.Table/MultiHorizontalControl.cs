using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Console.Table
{
    public class MultiHorizontalControl : IConsoleControl
    {
        protected class Item
        {
            public IConsoleControl Control { get; set; }
            public int WidthPercent { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public MultiHorizontalControl()
        {
            Items = new List<Item>();
        }

        protected List<Item> Items { get; set; }

        public virtual void AddResizableControl(IConsoleControl control, int widthPercent)
        {
            Items.Add(CreateResizableItem(control, widthPercent));
        }

        protected static Item CreateResizableItem(IConsoleControl control, int widthPercent)
        {
            return new Item
            {
                Control = control,
                WidthPercent = widthPercent
            };
        }

        public virtual void AddFixedSizeControl(IConsoleControl control, int width)
        {
            Items.Add(CreateFixedSizeItem(control, width));
        }

        protected static Item CreateFixedSizeItem(IConsoleControl control, int width)
        {
            return new Item
            {
                Control = control,
                Width = width,
                WidthPercent = 0,
                Height = 0
            };
        }

        public int CalculateHeight(int width)
        {
            if (Items.Count == 0)
            {
                return 0;
            }

            return Items.Max(s => s.Height);
        }

        public virtual void Adjust(int width)
        {
            if (Items.Count == 0)
            {
                return;
            }


            var resizable = Items.Where(i => i.WidthPercent != 0).ToArray();
            var fixedSize = Items.Where(i => i.WidthPercent == 0).ToArray();

            var fixedSizeWidth = fixedSize.Sum(s => s.Width);

            var resizableItemsPercents = resizable.Sum(s => s.WidthPercent);
            var resizableItemsSpace = ( width - fixedSizeWidth);
            var unit = ((double)resizableItemsSpace) / resizableItemsPercents;

            foreach (var item in resizable)
            {
                item.Width = (int)Math.Round(item.WidthPercent * unit, 0);
            }

            var totalSumAfterAdjust = resizable.Sum(s => s.Width);

            resizable.Last().Width += resizableItemsSpace - totalSumAfterAdjust;

            foreach (var item in Items)
            {
                item.Control.Adjust(item.Width);
                item.Height = item.Control.CalculateHeight(item.Width);
            }
        }

        public void Draw(TextWriter writer, int width, int line)
        {
            foreach (var item in Items)
            {
                    item.Control.Draw(writer, item.Width, line);
            }
        }
    }
}