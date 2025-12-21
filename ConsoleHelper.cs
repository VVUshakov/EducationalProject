namespace EducationalProject
{
    /// <summary>
    /// Утилитарный класс для работы с консолью
    /// Содержит универсальные методы для вывода, форматирования и взаимодействия с пользователем
    /// </summary>
    public static class ConsoleHelper
    {
        #region ===== КОНСТАНТЫ =====

        /// <summary>
        /// Стандартная ширина разделителя
        /// </summary>
        private const int DEFAULT_SEPARATOR_LENGTH = 35;

        /// <summary>
        /// Стандартный символ разделителя
        /// </summary>
        private const char DEFAULT_SEPARATOR_CHAR = '═';

        #endregion

        #region ===== МЕТОДЫ ДЛЯ ВЫВОДА И ФОРМАТИРОВАНИЯ =====

        /// <summary>
        /// Вывести заголовок в рамке (стиль псевдографики)
        /// </summary>
        /// <param name="title">Текст заголовка</param>
        /// <param name="subtitle">Подзаголовок (опционально)</param>
        /// <param name="width">Ширина рамки (по умолчанию 35)</param>
        public static void ShowHeader(string title, string subtitle = null, int width = 35)
        {
            //Console.Clear();

            // Угловые и граничные символы
            const char TOP_LEFT = '╔';
            const char TOP_RIGHT = '╗';
            const char BOTTOM_LEFT = '╚';
            const char BOTTOM_RIGHT = '╝';
            const char VERTICAL = '║';
            const char HORIZONTAL = '═';

            // Верхняя граница
            Console.WriteLine($"{TOP_LEFT}{new string(HORIZONTAL, width)}{TOP_RIGHT}");

            // Центрируем заголовок
            string centeredTitle = CenterText(title, width);
            Console.WriteLine($"{VERTICAL}{centeredTitle}{VERTICAL}");

            // Если есть подзаголовок
            if(!string.IsNullOrEmpty(subtitle))
            {
                string centeredSubtitle = CenterText(subtitle, width);
                Console.WriteLine($"{VERTICAL}{centeredSubtitle}{VERTICAL}");
            }

            // Нижняя граница
            Console.WriteLine($"{BOTTOM_LEFT}{new string(HORIZONTAL, width)}{BOTTOM_RIGHT}");
            //Console.WriteLine();
        }

        /// <summary>
        /// Вывести заголовок с несколькими строками в рамке
        /// </summary>
        /// <param name="lines">Массив строк для отображения</param>
        /// <param name="width">Ширина рамки</param>
        public static void ShowHeader(string[] lines, int width = 35)
        {
            // Console.Clear();

            const char TOP_LEFT = '╔';
            const char TOP_RIGHT = '╗';
            const char BOTTOM_LEFT = '╚';
            const char BOTTOM_RIGHT = '╝';
            const char VERTICAL = '║';
            const char HORIZONTAL = '═';

            Console.WriteLine($"{TOP_LEFT}{new string(HORIZONTAL, width)}{TOP_RIGHT}");

            foreach(string line in lines)
            {
                string centeredLine = CenterText(line, width);
                Console.WriteLine($"{VERTICAL}{centeredLine}{VERTICAL}");
            }

            Console.WriteLine($"{BOTTOM_LEFT}{new string(HORIZONTAL, width)}{BOTTOM_RIGHT}");
            //Console.WriteLine();
        }

        /// <summary>
        /// Вывести разделительную линию
        /// </summary>
        /// <param name="char">Символ разделителя</param>
        /// <param name="length">Длина разделителя</param>
        /// <param name="newLineBefore">Добавить пустую строку перед разделителем</param>
        /// <param name="newLineAfter">Добавить пустую строку после разделителя</param>
        public static void ShowSeparator(char separatorChar = DEFAULT_SEPARATOR_CHAR,
                                         int length = DEFAULT_SEPARATOR_LENGTH,
                                         bool newLineBefore = false,
                                         bool newLineAfter = false)
        {
            if(newLineBefore) Console.WriteLine();
            Console.WriteLine(new string(separatorChar, length));
            if(newLineAfter) Console.WriteLine();
        }

        /// <summary>
        /// Вывести меню с пунктами
        /// </summary>
        /// <param name="title">Заголовок меню (опционально)</param>
        /// <param name="items">Массив пунктов меню</param>
        /// <param name="startFromZero">Начинать нумерацию с 0 (true) или с 1 (false)</param>
        public static void ShowMenu(string title, string[] items, bool startFromZero = false)
        {
            if(!string.IsNullOrEmpty(title))
            {
                Console.WriteLine($"\n════════════ {title.ToUpper()} ════════════");
            }

            int startNumber = startFromZero ? 0 : 1;

            for(int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"{i + startNumber}. {items[i]}");
            }
        }

        /// <summary>
        /// Вывести меню с пунктами и дополнительной информацией
        /// </summary>
        /// <param name="title">Заголовок меню</param>
        /// <param name="items">Список кортежей (номер, текст)</param>
        public static void ShowMenuWithNumbers(string title, List<(int number, string text)> items)
        {
            if(!string.IsNullOrEmpty(title))
            {
                Console.WriteLine();
                Console.WriteLine($"════════════ {title.ToUpper()} ════════════");
            }

            foreach(var item in items)
            {
                Console.WriteLine($"{item.number}. {item.text}");
            }
        }

        /// <summary>
        /// Вывести информационный блок в рамке
        /// </summary>
        public static void ShowInfoBlock(string title, string[] lines, int width = 35)
        {
            //Console.WriteLine();
            ShowHeader(title, width: width);

            foreach(string line in lines)
            {
                Console.WriteLine($"  {line}");
            }

            ShowSeparator('═', width, newLineAfter: true);
        }

        #endregion

        #region ===== МЕТОДЫ ДЛЯ СООБЩЕНИЙ =====

        /// <summary>
        /// Вывести сообщение об ошибке
        /// </summary>
        /// <param name="message">Текст ошибки</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "ОШИБКА")</param>
        public static void ShowError(string message, string prefix = "ОШИБКА")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Red);
        }

        /// <summary>
        /// Вывести сообщение об успехе
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "УСПЕХ")</param>
        public static void ShowSuccess(string message, string prefix = "УСПЕХ")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Green);
        }

        /// <summary>
        /// Вывести информационное сообщение
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "ИНФО")</param>
        public static void ShowInfo(string message, string prefix = "ИНФО")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Вывести предупреждение
        /// </summary>
        /// <param name="message">Текст предупреждения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "ВНИМАНИЕ")</param>
        public static void ShowWarning(string message, string prefix = "ВНИМАНИЕ")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Вывести результат операции в рамке
        /// </summary>
        public static void ShowResult(string title, string content, int width = 40)
        {
            string[] resultLines = {
                title,
                "",
                content
            };

            ShowHeader(resultLines, width);
        }

        /// <summary>
        /// Вывести результат в формате ключ-значение
        /// </summary>
        public static void ShowKeyValueResult(string key, string value, ConsoleColor keyColor = ConsoleColor.Gray)
        {
            Console.ForegroundColor = keyColor;
            Console.Write($"{key}: ");
            Console.ResetColor();
            Console.WriteLine(value);
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Ожидать нажатия любой клавиши
        /// </summary>
        /// <param name="message">Сообщение для пользователя</param>
        public static void WaitForAnyKey(string message = "Нажмите любую клавишу для продолжения...")
        {
            ShowSeparator(newLineBefore: true);
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Получить ввод от пользователя с приглашением
        /// </summary>
        /// <param name="prompt">Приглашение для ввода</param>
        /// <returns>Введенная строка (без начальных и конечных пробелов)</returns>
        public static string GetInput(string prompt = ">>> ")
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? "";
        }

        /// <summary>
        /// Получить числовой ввод от пользователя
        /// </summary>
        /// <param name="prompt">Приглашение для ввода</param>
        /// <param name="defaultValue">Значение по умолчанию при пустом вводе</param>
        /// <returns>Введенное число или значение по умолчанию</returns>
        public static int GetNumberInput(string prompt = ">>> ", int defaultValue = 1)
        {
            string input = GetInput(prompt);

            if(string.IsNullOrWhiteSpace(input))
                return defaultValue;

            return int.TryParse(input, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// Получить выбор из меню
        /// </summary>
        /// <param name="minValue">Минимальное допустимое значение</param>
        /// <param name="maxValue">Максимальное допустимое значение</param>
        /// <param name="prompt">Приглашение для ввода</param>
        /// <returns>Выбранный номер</returns>
        public static int GetMenuChoice(int minValue, int maxValue, string prompt = ">>> Введите номер: ")
        {
            while(true)
            {
                int choice = GetNumberInput(prompt, -1);

                if(choice >= minValue && choice <= maxValue)
                    return choice;

                ShowError($"Введите число от {minValue} до {maxValue}!");
            }
        }

        #endregion

        #region ===== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Центрировать текст в заданной ширине
        /// </summary>
        private static string CenterText(string text, int width)
        {
            if(text.Length >= width)
                return text.Substring(0, width);

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        /// <summary>
        /// Вывести текст указанным цветом
        /// </summary>
        private static void WriteColoredLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Вывести текст указанным цветом (без перевода строки)
        /// </summary>
        private static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Очистить консоль и показать заголовок (перегрузка для одной строки)
        /// </summary>
        public static void ClearAndShowHeader(string title)
        {
            Console.Clear();
            ShowHeader(title);
        }

        /// <summary>
        /// Очистить консоль и показать заголовок (перегрузка для нескольких строк)
        /// </summary>
        public static void ClearAndShowHeader(string[] lines)
        {
            ShowHeader(lines);
        }

        /// <summary>
        /// Анимировать точки ожидания
        /// </summary>
        public static void ShowLoading(string message = "Загрузка", int dotsCount = 3, int delayMs = 300)
        {
            Console.Write(message);

            for(int i = 0; i < dotsCount; i++)
            {
                Console.Write(".");
                Thread.Sleep(delayMs);
            }

            Console.WriteLine();
        }

        #endregion
    }
}