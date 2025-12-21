namespace EducationalProject.Services
{
    /// <summary>
    /// Программа 3: Кодировщик имени
    /// Демонстрирует различные методы кодирования текста
    /// </summary>
    public class NameEncoder : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Название программы для отображения в меню
        /// </summary>
        public override string Name => "Кодировщик имени";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Главный метод запуска программы
        /// </summary>
        public override void Run()
        {
            ShowHeader(); // Показать заголовок

            string name = GetUserName(); // Получить имя пользователя

            if(!ValidateName(name)) // Если имя не валидно...
            {
                ShowErrorMessage("Имя не может быть пустым!");
                WaitForContinue();
                return;
            }

            ShowEncodingMethods(); // Показать доступные методы кодирования

            string choice = GetUserChoice(); // Получить выбор метода

            string encodedName = EncodeName(name, choice); // Закодировать имя

            ShowResult(name, encodedName, choice); // Показать результат

            WaitForContinue(); // Ожидать подтверждения
        }

        /// <summary>
        /// Получить название метода по его номеру
        /// </summary>
        private string GetMethodName(string choice)
        {
            switch(choice)
            {
                case "1": return "Алфавитные позиции";
                case "2": return "Азбука Морзе";
                case "3": return "Простой шифр";
                default: return "Неизвестный метод";
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Показать заголовок программы
        /// </summary>
        private void ShowHeader()
        {
            Console.Clear(); // Очистить консоль
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("              КОДИРОВЩИК ИМЕНИ                 ");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine();
        }

        /// <summary>
        /// Показать доступные методы кодирования
        /// </summary>
        private void ShowEncodingMethods()
        {
            Console.WriteLine("\n════════════ МЕТОДЫ КОДИРОВАНИЯ ════════════");
            Console.WriteLine("1. Алфавитные позиции (А=1, Б=2, ...)");
            Console.WriteLine("2. Азбука Морзе");
            Console.WriteLine("3. Простой шифр (позиция × 3)");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine();
        }

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        private string GetUserName()
        {
            Console.Write(">>> Введите ваше имя: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            return input.ToUpper(); // Приводим к верхнему регистру
        }

        /// <summary>
        /// Получить выбор метода кодирования
        /// </summary>
        private string GetUserChoice()
        {
            Console.Write(">>> Выберите метод кодирования (1-3): ");
            string choice = Console.ReadLine()?.Trim() ?? "1";

            // Проверяем валидность выбора
            if(choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Неверный выбор. Используем метод 1.");
                choice = "1";
            }

            return choice;
        }

        /// <summary>
        /// Показать результат кодирования
        /// </summary>
        private void ShowResult(string originalName, string encodedName, string methodChoice)
        {
            Console.WriteLine("\n════════════════ РЕЗУЛЬТАТ ════════════════");
            Console.WriteLine($"Исходное имя: {originalName}");

            string methodName = GetMethodName(methodChoice);
            Console.WriteLine($"Метод кодирования: {methodName}");
            Console.WriteLine($"Закодированное имя: {encodedName}");
            Console.WriteLine("═══════════════════════════════════════════════");
        }

        /// <summary>
        /// Показать сообщение об ошибке
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ОШИБКА: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Ожидать нажатия клавиши для продолжения
        /// </summary>
        private void WaitForContinue()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        #endregion

        #region ===== МЕТОДЫ КОДИРОВАНИЯ =====

        /// <summary>
        /// Закодировать имя выбранным методом
        /// </summary>
        private string EncodeName(string name, string methodChoice)
        {
            switch(methodChoice)
            {
                case "1":
                    return GetAlphabetCode(name);
                case "2":
                    return GetMorseCode(name);
                case "3":
                    return GetCipherCode(name);
                default:
                    return GetAlphabetCode(name);
            }
        }

        /// <summary>
        /// Кодирование алфавитными позициями (А=1, Б=2, ...)
        /// </summary>
        private string GetAlphabetCode(string name)
        {
            string result = "";

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    // Для русских букв вычисляем позицию в алфавите
                    int position = c - 'А' + 1;
                    result += $"{position}-";
                }
                else if(char.IsLetter(c))
                {
                    // Для латинских букв
                    result += $"{char.ToUpper(c)}? ";
                }
            }

            // Убираем последний дефис, если он есть
            return CleanResult(result, '-');
        }

        /// <summary>
        /// Кодирование азбукой Морзе
        /// </summary>
        private string GetMorseCode(string name)
        {
            string result = "";

            foreach(char c in name)
            {
                if(char.IsLetter(c))
                {
                    string morseSymbol = GetMorseSymbol(c);
                    result += $"{morseSymbol} ";
                }
            }

            // Убираем последний пробел, если он есть
            return CleanResult(result, ' ');
        }

        /// <summary>
        /// Получить символ азбуки Морзе для буквы
        /// </summary>
        private string GetMorseSymbol(char c)
        {
            // Приводим к верхнему регистру для единообразия
            char upperC = char.ToUpper(c);

            // Русский алфавит в азбуке Морзе
            switch(upperC)
            {
                case 'А': return ".-";
                case 'Б': return "-...";
                case 'В': return ".--";
                case 'Г': return "--.";
                case 'Д': return "-..";
                case 'Е': return ".";
                case 'Ё': return ".";
                case 'Ж': return "...-";
                case 'З': return "--..";
                case 'И': return "..";
                case 'Й': return ".---";
                case 'К': return "-.-";
                case 'Л': return ".-..";
                case 'М': return "--";
                case 'Н': return "-.";
                case 'О': return "---";
                case 'П': return ".--.";
                case 'Р': return ".-.";
                case 'С': return "...";
                case 'Т': return "-";
                case 'У': return "..-";
                case 'Ф': return "..-.";
                case 'Х': return "....";
                case 'Ц': return "-.-.";
                case 'Ч': return "---.";
                case 'Ш': return "----";
                case 'Щ': return "--.-";
                case 'Ъ': return "--.--";
                case 'Ы': return "-.--";
                case 'Ь': return "-..-";
                case 'Э': return "..-..";
                case 'Ю': return "..--";
                case 'Я': return ".-.-";
                default: return "?"; // Для не-русских букв
            }
        }

        /// <summary>
        /// Кодирование простым шифром (позиция × 3)
        /// </summary>
        private string GetCipherCode(string name)
        {
            string result = "";

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    // (позиция в алфавите) × 3
                    int encodedValue = (c - 'А' + 1) * 3;
                    result += $"{encodedValue} ";
                }
                else if(char.IsLetter(c))
                {
                    // Для латинских букв
                    result += $"{char.ToUpper(c)}? ";
                }
            }

            // Убираем последний пробел, если он есть
            return CleanResult(result, ' ');
        }

        /// <summary>
        /// Очистить результат от последнего разделителя
        /// </summary>
        private string CleanResult(string result, char separator)
        {
            if(string.IsNullOrEmpty(result))
            {
                return "Пустой результат";
            }

            if(result.EndsWith($"{separator}"))
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        #endregion

        #region ===== МЕТОДЫ-ЧЕКЕРЫ =====

        /// <summary>
        /// Проверить валидность имени
        /// </summary>
        private bool ValidateName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            // Проверяем, содержит ли имя хотя бы одну букву
            bool hasLetters = false;
            foreach(char c in name)
            {
                if(char.IsLetter(c))
                {
                    hasLetters = true;
                    break;
                }
            }

            return hasLetters;
        }

        /// <summary>
        /// Проверить, является ли буква русской
        /// </summary>
        private bool IsRussianLetter(char c)
        {
            return (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я');
        }

        #endregion
    }
}