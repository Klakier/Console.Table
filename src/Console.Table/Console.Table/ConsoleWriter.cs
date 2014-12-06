using System.ComponentModel;

namespace Console.Table
{
    public static class ConsoleWriter
    {
        private static readonly int Width = System.Console.BufferWidth - 1;

        public static void Write(IConsoleControl control)
        {
            control.Adjust(Width);
            var lines = control.CalculateHeight(Width);
            for (var i = 0; i < lines; i++)
            {
                control.Draw(System.Console.Out, Width, i);
                System.Console.WriteLine();
            }

            System.Console.WriteLine();
        }
    }
}