using System;
using Console.Table;

namespace TestApplicaiton
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new Table();
            var row = new TableRow();
            row.AddResizableControl(new TextControl("verrrrrrrrrrrrrrrrrrrrrrrryyyyyyyyyyy long text"), 2);
            row.AddResizableControl(new TextControl("fooBar2"), 1);
            row.AddResizableControl(new TextControl("verrrrrrrrrrrrrrrrrrrrrrrryyyyyyyyyyy long text"), 2);
            var row2 = new TableRow();
            row2.AddResizableControl(new TextControl("text labsfa"), 2);
            row2.AddResizableControl(new TextControl("fooBar2"), 1);
            row2.AddResizableControl(new TextControl("asdfas "), 2);

            table.AddWidget(row);
            table.AddWidget(row2);

            ConsoleWriter.Write(table);
            System.Console.ReadKey();
        }
    }
}
