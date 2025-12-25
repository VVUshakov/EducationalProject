namespace EducationalProject
{
    public static class ConsoleHelper
    {
        private const int DEFAULT_FRAME_WIDTH = 35;
        private const char FRAME_TOP_LEFT = '╔';
        private const char FRAME_TOP_RIGHT = '╗';
        private const char FRAME_BOTTOM_LEFT = '╚';
        private const char FRAME_BOTTOM_RIGHT = '╝';
        private const char FRAME_VERTICAL = '║';
        private const char FRAME_HORIZONTAL = '═';

        public const string DEFAULT_INPUT_PROMPT = ">>> ";
        public const string DEFAULT_WAIT_MESSAGE = "Нажмите любую клавишу для продолжения...";

        public static void ClearAndShowHeader(string title)
        {
            Console.Clear();
            ShowHeader(title);
        }

        public static void ShowHeader(string title, string subtitle = null, int width = DEFAULT_FRAME_WIDTH)
        {
            Console.WriteLine($"{FRAME_TOP_LEFT}{new string(FRAME_HORIZONTAL, width)}{FRAME_TOP_RIGHT}");

            string centeredTitle = CenterText(title, width);
            Console.WriteLine($"{FRAME_VERTICAL}{centeredTitle}{FRAME_VERTICAL}");

            if(!string.IsNullOrEmpty(subtitle))
            {
                string centeredSubtitle = CenterText(subtitle, width);
                Console.WriteLine($"{FRAME_VERTICAL}{centeredSubtitle}{FRAME_VERTICAL}");
            }

            Console.WriteLine($"{FRAME_BOTTOM_LEFT}{new string(FRAME_HORIZONTAL, width)}{FRAME_BOTTOM_RIGHT}");
        }

        public static void ShowMenu(string title, string[] items)
        {
            Console.WriteLine($"\n════════════ {title.ToUpper()} ════════════");

            for(int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
        }

        public static void ShowInfoBlock(string title, string[] lines)
        {
            ShowHeader(title);

            foreach(string line in lines)
            {
                Console.WriteLine($"  {line}");
            }

            Console.WriteLine(new string('═', 35) + "\n");
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ОШИБКА: {message}");
            Console.ResetColor();
        }

        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"УСПЕХ: {message}");
            Console.ResetColor();
        }

        public static void WaitForAnyKey(string message = DEFAULT_WAIT_MESSAGE)
        {
            Console.WriteLine("\n" + new string('═', 35));
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public static string GetInput(string prompt = DEFAULT_INPUT_PROMPT)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        private static string CenterText(string text, int width)
        {
            if(text.Length >= width)
                return text.Substring(0, width);

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }
    }
}