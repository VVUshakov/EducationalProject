namespace EducationalProject
{
    /// <summary>
    /// Утилитарный класс для работы с консолью
    /// Содержит универсальные методы для вывода, форматирования и взаимодействия с пользователем
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс предоставляет удобные статические методы для:
    /// <list type="bullet">
    /// <item><description>Форматированного вывода информации (заголовки, меню, разделители)</description></item>
    /// <item><description>Отображения различных типов сообщений (ошибки, предупреждения, успех)</description></item>
    /// <item><description>Взаимодействия с пользователем (ввод данных, выбор из меню)</description></item>
    /// <item><description>Визуального оформления консольного интерфейса</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Все методы являются статическими и потокобезопасными для использования из любого места приложения.
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример использования основных методов:
    /// <code>
    /// // Вывод заголовка
    /// ConsoleHelper.ShowHeader("Главное меню", "Выберите действие");
    /// 
    /// // Вывод меню
    /// string[] menuItems = { "Опция 1", "Опция 2", "Выход" };
    /// ConsoleHelper.ShowMenu("Меню действий", menuItems);
    /// 
    /// // Получение выбора пользователя
    /// int choice = ConsoleHelper.GetMenuChoice(1, 3);
    /// 
    /// // Вывод сообщения об ошибке
    /// ConsoleHelper.ShowError("Неверный ввод");
    /// </code>
    /// </example>
    /// <seealso cref="InputValidator"/>
    /// <seealso cref="MenuManager"/>
    public static class ConsoleHelper
    {
        #region ===== КОНСТАНТЫ =====

        /// <summary>
        /// Стандартная ширина разделителя
        /// </summary>
        /// <value>35 символов</value>
        private const int DEFAULT_SEPARATOR_LENGTH = 35;

        /// <summary>
        /// Стандартный символ разделителя
        /// </summary>
        /// <value>Символ '═'</value>
        private const char DEFAULT_SEPARATOR_CHAR = '═';

        #endregion

        #region ===== МЕТОДЫ ДЛЯ ВЫВОДА И ФОРМАТИРОВАНИЯ =====

        /// <summary>
        /// Вывести заголовок в рамке (стиль псевдографики)
        /// </summary>
        /// <param name="title">Текст заголовка</param>
        /// <param name="subtitle">Подзаголовок (опционально)</param>
        /// <param name="width">Ширина рамки (по умолчанию 35 символов)</param>
        /// <example>
        /// <code>
        /// // Вывод простого заголовка
        /// ConsoleHelper.ShowHeader("Моя программа");
        /// 
        /// // Вывод заголовка с подзаголовком
        /// ConsoleHelper.ShowHeader("Калькулятор", "Версия 1.0", 40);
        /// </code>
        /// </example>
        /// <remarks>
        /// Использует символы псевдографики для создания рамки:
        /// ╔══════════════════════════════════╗
        /// ║         Центрированный текст     ║
        /// ╚══════════════════════════════════╝
        /// </remarks>
        public static void ShowHeader(string title, string subtitle = null, int width = 35)
        {
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
        }

        /// <summary>
        /// Вывести заголовок с несколькими строками в рамке
        /// </summary>
        /// <param name="lines">Массив строк для отображения</param>
        /// <param name="width">Ширина рамки</param>
        /// <example>
        /// <code>
        /// string[] headerLines = {
        ///     "Образовательный проект",
        ///     "----------------------",
        ///     "Главное меню"
        /// };
        /// ConsoleHelper.ShowHeader(headerLines, 45);
        /// </code>
        /// </example>
        public static void ShowHeader(string[] lines, int width = 35)
        {
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
        }

        /// <summary>
        /// Вывести разделительную линию
        /// </summary>
        /// <param name="separatorChar">Символ разделителя (по умолчанию '═')</param>
        /// <param name="length">Длина разделителя (по умолчанию 35 символов)</param>
        /// <param name="newLineBefore">Добавить пустую строку перед разделителем</param>
        /// <param name="newLineAfter">Добавить пустую строку после разделителя</param>
        /// <example>
        /// <code>
        /// // Простой разделитель
        /// ConsoleHelper.ShowSeparator();
        /// 
        /// // Разделитель с настройками
        /// ConsoleHelper.ShowSeparator('─', 50, true, true);
        /// </code>
        /// </example>
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
        /// <example>
        /// <code>
        /// string[] menuItems = { "Создать", "Редактировать", "Удалить", "Выйти" };
        /// ConsoleHelper.ShowMenu("Действия", menuItems);
        /// // Результат:
        /// // ════════════ ДЕЙСТВИЯ ════════════
        /// // 1. Создать
        /// // 2. Редактировать
        /// // 3. Удалить
        /// // 4. Выйти
        /// </code>
        /// </example>
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
        /// <example>
        /// <code>
        /// var menuItems = new List<(int, string)> {
        ///     (1, "Основные настройки"),
        ///     (5, "Расширенные параметры"),
        ///     (9, "Выход в главное меню")
        /// };
        /// ConsoleHelper.ShowMenuWithNumbers("Настройки", menuItems);
        /// </code>
        /// </example>
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
        /// <param name="title">Заголовок блока</param>
        /// <param name="lines">Массив строк содержимого</param>
        /// <param name="width">Ширина рамки</param>
        /// <example>
        /// <code>
        /// string[] infoLines = {
        ///     "Программа предназначена для обучения",
        ///     "Основам программирования на C#",
        ///     "Версия: 1.0.0",
        ///     "Автор: Образовательный проект"
        /// };
        /// ConsoleHelper.ShowInfoBlock("О программе", infoLines);
        /// </code>
        /// </example>
        public static void ShowInfoBlock(string title, string[] lines, int width = 35)
        {
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
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowError("Файл не найден");
        /// // Вывод: ОШИБКА: Файл не найден (красным цветом)
        /// </code>
        /// </example>
        public static void ShowError(string message, string prefix = "ОШИБКА")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Red);
        }

        /// <summary>
        /// Вывести сообщение об успехе
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "УСПЕХ")</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowSuccess("Данные сохранены");
        /// // Вывод: УСПЕХ: Данные сохранены (зеленым цветом)
        /// </code>
        /// </example>
        public static void ShowSuccess(string message, string prefix = "УСПЕХ")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Green);
        }

        /// <summary>
        /// Вывести информационное сообщение
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "ИНФО")</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowInfo("Программа завершена");
        /// // Вывод: ИНФО: Программа завершена (голубым цветом)
        /// </code>
        /// </example>
        public static void ShowInfo(string message, string prefix = "ИНФО")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Cyan);
        }

        /// <summary>
        /// Вывести предупреждение
        /// </summary>
        /// <param name="message">Текст предупреждения</param>
        /// <param name="prefix">Префикс сообщения (по умолчанию "ВНИМАНИЕ")</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowWarning("Данные не сохранены");
        /// // Вывод: ВНИМАНИЕ: Данные не сохранены (желтым цветом)
        /// </code>
        /// </example>
        public static void ShowWarning(string message, string prefix = "ВНИМАНИЕ")
        {
            WriteColoredLine($"{prefix}: {message}", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Вывести результат операции в рамке
        /// </summary>
        /// <param name="title">Заголовок результата</param>
        /// <param name="content">Содержимое результата</param>
        /// <param name="width">Ширина рамки</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowResult("Результат вычислений", "Сумма: 42");
        /// </code>
        /// </example>
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
        /// <param name="key">Ключ (отображается цветом)</param>
        /// <param name="value">Значение</param>
        /// <param name="keyColor">Цвет для отображения ключа (по умолчанию серый)</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowKeyValueResult("Имя пользователя", "JohnDoe", ConsoleColor.Blue);
        /// // Вывод: Имя пользователя: JohnDoe
        /// // где "Имя пользователя:" синего цвета
        /// </code>
        /// </example>
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
        /// <param name="message">Сообщение для пользователя (по умолчанию "Нажмите любую клавишу для продолжения...")</param>
        /// <example>
        /// <code>
        /// // Стандартное сообщение
        /// ConsoleHelper.WaitForAnyKey();
        /// 
        /// // Кастомное сообщение
        /// ConsoleHelper.WaitForAnyKey("Нажмите Enter для продолжения...");
        /// </code>
        /// </example>
        public static void WaitForAnyKey(string message = "Нажмите любую клавишу для продолжения...")
        {
            ShowSeparator(newLineBefore: true);
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Получить ввод от пользователя с приглашением
        /// </summary>
        /// <param name="prompt">Приглашение для ввода (по умолчанию ">>> ")</param>
        /// <returns>Введенная строка (без начальных и конечных пробелов) или пустая строка</returns>
        /// <example>
        /// <code>
        /// string name = ConsoleHelper.GetInput("Введите ваше имя: ");
        /// </code>
        /// </example>
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
        /// <example>
        /// <code>
        /// // С приглашением и значением по умолчанию
        /// int age = ConsoleHelper.GetNumberInput("Введите возраст: ", 18);
        /// </code>
        /// </example>
        public static int GetNumberInput(string prompt = ">>> ", int defaultValue = 1)
        {
            string input = GetInput(prompt);

            if(string.IsNullOrWhiteSpace(input))
                return defaultValue;

            return int.TryParse(input, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// Получить выбор из меню с валидацией диапазона
        /// </summary>
        /// <param name="minValue">Минимальное допустимое значение</param>
        /// <param name="maxValue">Максимальное допустимое значение</param>
        /// <param name="prompt">Приглашение для ввода</param>
        /// <returns>Выбранный номер из допустимого диапазона</returns>
        /// <example>
        /// <code>
        /// // После вывода меню с пунктами 1-4:
        /// int choice = ConsoleHelper.GetMenuChoice(1, 4);
        /// // Будет запрашивать ввод до тех пор, пока не будет введено число от 1 до 4
        /// </code>
        /// </example>
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
        /// <param name="text">Текст для центрирования</param>
        /// <param name="width">Общая ширина строки</param>
        /// <returns>Отцентрированная строка</returns>
        /// <remarks>
        /// Если текст длиннее указанной ширины, он обрезается.
        /// </remarks>
        private static string CenterText(string text, int width)
        {
            if(text.Length >= width)
                return text.Substring(0, width);

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        /// <summary>
        /// Вывести текст указанным цветом с переводом строки
        /// </summary>
        /// <param name="text">Текст для вывода</param>
        /// <param name="color">Цвет текста</param>
        private static void WriteColoredLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Вывести текст указанным цветом без перевода строки
        /// </summary>
        /// <param name="text">Текст для вывода</param>
        /// <param name="color">Цвет текста</param>
        private static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Очистить консоль и показать заголовок
        /// </summary>
        /// <param name="title">Заголовок для отображения</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ClearAndShowHeader("Новый экран");
        /// </code>
        /// </example>
        public static void ClearAndShowHeader(string title)
        {
            Console.Clear();
            ShowHeader(title);
        }

        /// <summary>
        /// Очистить консоль и показать заголовок из нескольких строк
        /// </summary>
        /// <param name="lines">Массив строк заголовка</param>
        public static void ClearAndShowHeader(string[] lines)
        {
            Console.Clear();
            ShowHeader(lines);
        }

        /// <summary>
        /// Анимировать точки ожидания (загрузки)
        /// </summary>
        /// <param name="message">Базовое сообщение</param>
        /// <param name="dotsCount">Количество точек для анимации</param>
        /// <param name="delayMs">Задержка между точками в миллисекундах</param>
        /// <example>
        /// <code>
        /// ConsoleHelper.ShowLoading("Обработка данных", 5, 200);
        /// // Выводит: Обработка данных..... с задержкой 200мс между точками
        /// </code>
        /// </example>
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