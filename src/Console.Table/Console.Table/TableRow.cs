using System.Collections.Generic;
using System.Linq;

namespace Console.Table
{
    public class TableRow : MultiHorizontalControl
    {
        public override void Adjust(int width)
        {
            if (Items.Count > 0)
            {
                Items = InsertSeparators(Items).ToList();
            }

            base.Adjust(width);
        }

        private static IEnumerable<Item> InsertSeparators(IEnumerable<Item> items)
        {
            yield return CreateSepartor();
            foreach (var item in items)
            {
                yield return item;
                yield return CreateSepartor();
            }
        }

        private static Item CreateSepartor()
        {
            return CreateFixedSizeItem(ColumnSepartor.Default, ColumnSepartor.Width);
        }
    }
}