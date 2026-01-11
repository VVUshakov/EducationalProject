namespace EducationalProject.Core
{
    public static class ConsoleHelper
    {
        // Очистка экрана и показ заголовка
        public static void ClearAndShowHeader(string title)
        {
            Console.Clear();
            ShowHeader(title);
        }

        // Показ заголовка с рамкой
        public static void ShowHeader(string title, string subtitle = null)
        {
            int width = AppConfig.FrameWidth;
            string horizontalLine = new string(AppConfig.FrameHorizontal, width);

            Console.WriteLine($"{AppConfig.FrameTopLeft}{horizontalLine}{AppConfig.FrameTopRight}");

            // Заголовок
            string centeredTitle = CenterText(title, width);
            Console.WriteLine($"{AppConfig.FrameVertical}{centeredTitle}{AppConfig.FrameVertical}");

            // Подзаголовок (если есть)
            if(!string.IsNullOrEmpty(subtitle))
            {
                string centeredSubtitle = CenterText(subtitle, width);
                Console.WriteLine($"{AppConfig.FrameVertical}{centeredSubtitle}{AppConfig.FrameVertical}");
            }

            Console.WriteLine($"{AppConfig.FrameBottomLeft}{horizontalLine}{AppConfig.FrameBottomRight}");
            Console.WriteLine();
        }

        // Показать меню
        public static void ShowMenu(string title, string[] items)
        {
            Console.WriteLine($"\n=== {title.ToUpper()} ===");
            for(int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }
            Console.WriteLine(new string('=', 20));
        }

        // Показать информационный блок
        public static void ShowInfoBlock(string title, string[] lines)
        {
            ShowHeader(title);
            foreach(string line in lines)
            {
                Console.WriteLine($"  {line}");
            }
            Console.WriteLine(new string('-', AppConfig.FrameWidth));
        }

        // Показать ошибку
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{AppConfig.ErrorTitle}] {message}");
            Console.ResetColor();
        }

        // Показать информацию
        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Показать успех
        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{AppConfig.SuccessTitle}] {message}");
            Console.ResetColor();
        }

        // Ожидание нажатия клавиши
        public static void WaitForAnyKey(string message = null)
        {
            Console.WriteLine("\n" + new string('-', AppConfig.FrameWidth));
            Console.WriteLine(message ?? AppConfig.PressAnyKeyMessage);
            Console.ReadKey();
        }

        // Получить ввод от пользователя
        public static string GetInput(string prompt = ">>> ")
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        // Выровнять текст по центру
        private static string CenterText(string text, int width)
        {
            if(text.Length >= width)
                return text.Substring(0, width);

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }
    }
}