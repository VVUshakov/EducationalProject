namespace EducationalProject.Utils
{
    public static class ConsoleHelper
    {
        public static void PrintColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void PrintSuccess(string message)
        {
            PrintColored(message, ConsoleColor.Green);
        }

        public static void PrintWarning(string message)
        {
            PrintColored(message, ConsoleColor.Yellow);
        }

        public static void PrintError(string message)
        {
            PrintColored(message, ConsoleColor.Red);
        }

        public static void PrintHeader(string title)
        {
            Console.WriteLine(new string('=', title.Length + 4));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('=', title.Length + 4));
        }
    }
}