namespace LähetysSeurantaConsole.View
{
    internal static class Style
    {
        private static void WriteLineColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void WriteError(string message)
        {
            Console.Beep();
            WriteLineColored(message, ConsoleColor.Red);
            Thread.Sleep(100);
            Console.Beep();
        }

        internal static void WriteInfo(string message) => WriteLineColored(message, ConsoleColor.Cyan);
        internal static void WriteSuccess(string message) => WriteLineColored(message, ConsoleColor.Green);
        internal static void WriteWarning(string message) => WriteLineColored(message, ConsoleColor.Yellow);
        internal static void WriteHint(string message) => WriteLineColored(message, ConsoleColor.DarkGray);
    }
}