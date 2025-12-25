// Объявляем пространство имен
namespace EducationalProject
{
    // Статический класс - все методы можно вызывать без создания объекта
    public static class ConsoleHelper
    {
        // Константа для стандартной ширины рамки (35 символов)
        private const int DEFAULT_FRAME_WIDTH = 35;

        // Константы для символов рамки (псевдографика)
        private const char FRAME_TOP_LEFT = '╔';      // Левый верхний угол
        private const char FRAME_TOP_RIGHT = '╗';     // Правый верхний угол
        private const char FRAME_BOTTOM_LEFT = '╚';   // Левый нижний угол
        private const char FRAME_BOTTOM_RIGHT = '╝';  // Правый нижний угол
        private const char FRAME_VERTICAL = '║';      // Вертикальная линия
        private const char FRAME_HORIZONTAL = '═';    // Горизонтальная линия

        // Константы для стандартных сообщений
        public const string DEFAULT_INPUT_PROMPT = ">>> ";  // Приглашение для ввода
        public const string DEFAULT_WAIT_MESSAGE = "Нажмите любую клавишу для продолжения...";

        // Метод для очистки экрана и показа заголовка
        public static void ClearAndShowHeader(string title)
        {
            Console.Clear();            // Очищаем консоль
            ShowHeader(title);          // Показываем заголовок
        }

        // Метод для вывода заголовка в рамке
        public static void ShowHeader(string title, string subtitle = null, int width = DEFAULT_FRAME_WIDTH)
        {
            // Верхняя граница рамки: левый угол + горизонтальная линия + правый угол
            Console.WriteLine($"{FRAME_TOP_LEFT}{new string(FRAME_HORIZONTAL, width)}{FRAME_TOP_RIGHT}");

            // Центрируем заголовок и выводим в рамке
            string centeredTitle = CenterText(title, width);                   // Центрируем текст
            Console.WriteLine($"{FRAME_VERTICAL}{centeredTitle}{FRAME_VERTICAL}"); // Выводим в рамке

            // Если есть подзаголовок, выводим и его
            if(!string.IsNullOrEmpty(subtitle))      // Проверяем, не пустой ли подзаголовок
            {
                string centeredSubtitle = CenterText(subtitle, width);            // Центрируем подзаголовок
                Console.WriteLine($"{FRAME_VERTICAL}{centeredSubtitle}{FRAME_VERTICAL}"); // Выводим
            }

            // Нижняя граница рамки
            Console.WriteLine($"{FRAME_BOTTOM_LEFT}{new string(FRAME_HORIZONTAL, width)}{FRAME_BOTTOM_RIGHT}");
        }

        // Метод для вывода меню с пунктами
        public static void ShowMenu(string title, string[] items)
        {
            // Выводим заголовок меню в рамке из символов =
            Console.WriteLine($"\n════════════ {title.ToUpper()} ════════════");

            // Перебираем все пункты меню
            for(int i = 0; i < items.Length; i++)       // i - номер текущего пункта (начинается с 0)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");  // Выводим номер (i+1) и текст пункта
            }
        }

        // Метод для вывода информационного блока
        public static void ShowInfoBlock(string title, string[] lines)
        {
            ShowHeader(title);           // Выводим заголовок в рамке

            // Перебираем все строки информации
            foreach(string line in lines)    // Для каждой строки в массиве lines
            {
                Console.WriteLine($"  {line}");  // Выводим строку с отступом
            }

            // Рисуем разделитель и переходим на новую строку
            Console.WriteLine(new string('═', 35) + "\n");
        }

        // Метод для вывода сообщения об ошибке (красным цветом)
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;  // Меняем цвет текста на красный
            Console.WriteLine($"ОШИБКА: {message}");     // Выводим сообщение
            Console.ResetColor();                        // Возвращаем стандартный цвет
        }

        // Метод для вывода информационного сообщения (голубым цветом)
        public static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;  // Меняем цвет на голубой
            Console.WriteLine(message);                   // Выводим сообщение
            Console.ResetColor();                         // Возвращаем стандартный цвет
        }

        // Метод для вывода сообщения об успехе (зеленым цветом)
        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;  // Меняем цвет на зеленый
            Console.WriteLine($"УСПЕХ: {message}");        // Выводим сообщение
            Console.ResetColor();                          // Возвращаем стандартный цвет
        }

        // Метод для ожидания нажатия любой клавиши
        public static void WaitForAnyKey(string message = DEFAULT_WAIT_MESSAGE)
        {
            Console.WriteLine("\n" + new string('═', 35));  // Рисуем разделитель
            Console.WriteLine(message);                     // Выводим сообщение
            Console.ReadKey();                              // Ждем нажатия любой клавиши
        }

        // Метод для получения ввода от пользователя
        public static string GetInput(string prompt = DEFAULT_INPUT_PROMPT)
        {
            Console.Write(prompt);                     // Выводим приглашение (например ">>> ")
            return Console.ReadLine()?.Trim() ?? "";   // Читаем строку, удаляем пробелы, если пусто - возвращаем ""
        }

        // Вспомогательный метод для центрирования текста
        private static string CenterText(string text, int width)
        {
            // Если текст длиннее ширины - обрезаем его
            if(text.Length >= width)
                return text.Substring(0, width);       // Берем первые width символов

            // Вычисляем сколько пробелов нужно добавить слева
            int padding = (width - text.Length) / 2;   // Разница между шириной и длиной текста, деленная на 2

            // Добавляем пробелы слева и справа
            return text.PadLeft(padding + text.Length).PadRight(width);
        }
    }
}