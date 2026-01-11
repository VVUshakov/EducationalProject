namespace EducationalProject.Core
{
    public class ConsoleHelper : IConsoleHelper
    {
        // Очистка экрана и показ заголовка и подзаголовка
        public void ClearAndShowHeader(string title, string subtitle = null)
        {
            Console.Clear();
            ShowHeader(title, subtitle);
        }

        // Показ заголовка с рамкой
        public void ShowHeader(string title, string subtitle = null)
        {
            int width = AppConfig.DisplayConfig.FrameWidth;
            string horizontalLine = new string(AppConfig.DisplayConfig.FrameHorizontal, width);

            // Верхняя рамка
            Console.WriteLine(
                $"{AppConfig.DisplayConfig.FrameTopLeft}" +
                $"{horizontalLine}" +
                $"{AppConfig.DisplayConfig.FrameTopRight}"
            );

            // Заголовок
            string centeredTitle = CenterText(title, width);
            Console.WriteLine(
                $"{AppConfig.DisplayConfig.FrameVertical}" +
                $"{centeredTitle}" +
                $"{AppConfig.DisplayConfig.FrameVertical}"
            );

            // Подзаголовок (если есть)
            if(!string.IsNullOrEmpty(subtitle))
            {
                string centeredSubtitle = CenterText(subtitle, width);
                Console.WriteLine(
                    $"{AppConfig.DisplayConfig.FrameVertical}" +
                    $"{centeredSubtitle}" +
                    $"{AppConfig.DisplayConfig.FrameVertical}"
                );
            }

            // Нижняя рамка
            Console.WriteLine(
                $"{AppConfig.DisplayConfig.FrameBottomLeft}" +
                $"{horizontalLine}" +
                $"{AppConfig.DisplayConfig.FrameBottomRight}"
            );
            Console.WriteLine();
        }

        // Показать меню
        public void ShowMenu(string title, string[] items)
        {
            Console.WriteLine($"\n=== {title.ToUpper()} ===");

            for(int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }

            Console.WriteLine(new string('=', 20));
        }

        // Показать информационный блок
        public void ShowInfoBlock(string title, string[] lines)
        {
            ShowHeader(title);

            foreach(string line in lines)
            {
                Console.WriteLine($"  {line}");
            }

            Console.WriteLine(new string('-', AppConfig.DisplayConfig.FrameWidth));
        }

        // Показать ошибку
        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{AppConfig.MessagesConfig.ErrorTitle}] {message}");
            Console.ResetColor();
        }

        // Показать информацию
        public void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Показать успех
        public void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{AppConfig.MessagesConfig.SuccessTitle}] {message}");
            Console.ResetColor();
        }

        // Ожидание нажатия клавиши
        public void WaitForAnyKey(string message = null)
        {
            Console.WriteLine("\n" + new string('-', AppConfig.DisplayConfig.FrameWidth));
            Console.WriteLine(message ?? AppConfig.MessagesConfig.PressAnyKey);
            Console.ReadKey();
        }

        // Получить ввод от пользователя
        public string GetInput(string prompt = ">>> ")
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        // Выровнять текст по центру
        private string CenterText(string text, int width)
        {
            if(text.Length >= width)
                return text.Substring(0, width);

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        // Показать разделитель
        public void ShowSeparator(int length = 40)
        {
            Console.WriteLine(new string('-', length));
        }

        // Показать заголовок раздела
        public void ShowSectionTitle(string title)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine(new string('-', title.Length));
        }

        // Очистка экрана
        public void ClearScreen()
        {
            Console.Clear();
        }

        // Показать цветной текст
        public void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        // Показать цветную строку
        public void WriteLineColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // Запросить подтверждение
        public bool GetConfirmation(string question)
        {
            Console.Write($"{question} (д/н): ");
            string response = Console.ReadLine()?.Trim().ToLower();

            return response == "д" || response == "да" || response == "y" || response == "yes";
        }

        // Показать прогресс
        public void ShowProgress(int current, int total, string message = "Выполнение")
        {
            int percentage = (int)((double)current / total * 100);
            Console.Write($"\r{message}: [{new string('#', percentage / 2)}{new string('.', 50 - percentage / 2)}] {percentage}%");

            if(current == total)
                Console.WriteLine();
        }
    }
}