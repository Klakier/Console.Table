using System.Collections.Generic;
using System.Linq;
using Console.Table.Extensions;

namespace Console.Table
{
    public class Table : MultiVerticalControl
    {
        public override void Adjust(int widht)
        {
            Items = GetItemsWithSepartors(Items).ToList();
            base.Adjust(widht);
        }

        private static IEnumerable<Item> GetItemsWithSepartors(IEnumerable<Item> items)
        {
            yield return CreateItem(new TableTopLine());

            foreach (var item in items.Separte(() => CreateItem(new TableMiddleLine())))
            {
                yield return item;
            }

            yield return CreateItem(new TableBottomLine());
        }
    }
}