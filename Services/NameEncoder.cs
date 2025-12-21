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
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует различные методы кодирования текста:",
                "1. Алфавитные позиции (А=1, Б=2, ...)",
                "2. Азбука Морзе",
                "3. Простой шифр (позиция × 3)"
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            string name = GetUserName();

            if(!ValidateName(name))
            {
                ConsoleHelper.ShowError("Имя не может быть пустым и должно содержать хотя бы одну букву!");
                ConsoleHelper.WaitForAnyKey();
                return;
            }

            ShowEncodingMethods();
            int choice = ConsoleHelper.GetMenuChoice(1, 3, ">>> Выберите метод кодирования (1-3): ");
            string encodedName = EncodeName(name, choice);
            ShowResult(name, encodedName, choice);

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        /// <summary>
        /// Получить название метода по его номеру
        /// </summary>
        private string GetMethodName(int choice)
        {
            return choice switch
            {
                1 => "Алфавитные позиции",
                2 => "Азбука Морзе",
                3 => "Простой шифр",
                _ => "Неизвестный метод"
            };
        }

        /// <summary>
        /// Получить описание метода по его номеру
        /// </summary>
        private string GetMethodDescription(int choice)
        {
            return choice switch
            {
                1 => "Каждая буква заменяется её номером в алфавите (А=1, Б=2, ..., Я=33)",
                2 => "Каждая буква заменяется соответствующей последовательностью точек и тире",
                3 => "Каждая буква заменяется значением (позиция × 3)",
                _ => "Неизвестный метод кодирования"
            };
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Показать доступные методы кодирования
        /// </summary>
        private void ShowEncodingMethods()
        {
            string[] methods = {
                "Алфавитные позиции (А=1, Б=2, ...)",
                "Азбука Морзе",
                "Простой шифр (позиция × 3)"
            };

            ConsoleHelper.ShowMenu("МЕТОДЫ КОДИРОВАНИЯ", methods);
            Console.WriteLine();
        }

        /// <summary>
        /// Получить имя пользователя
        /// </summary>
        private string GetUserName()
        {
            string input = ConsoleHelper.GetInput(">>> Введите ваше имя: ");
            return input.ToUpper(); // Приводим к верхнему регистру
        }

        /// <summary>
        /// Показать результат кодирования
        /// </summary>
        private void ShowResult(string originalName, string encodedName, int methodChoice)
        {
            string methodName = GetMethodName(methodChoice);
            string methodDescription = GetMethodDescription(methodChoice);

            string[] resultLines = {
                $"Исходное имя: {originalName}",
                $"Метод кодирования: {methodName}",
                "",
                methodDescription,
                "",
                $"Закодированное имя:",
                $"  {encodedName}"
            };

            ConsoleHelper.ShowInfoBlock("РЕЗУЛЬТАТ КОДИРОВАНИЯ", resultLines, 50);
        }

        #endregion

        #region ===== МЕТОДЫ КОДИРОВАНИЯ =====

        /// <summary>
        /// Закодировать имя выбранным методом
        /// </summary>
        private string EncodeName(string name, int methodChoice)
        {
            return methodChoice switch
            {
                1 => GetAlphabetCode(name),
                2 => GetMorseCode(name),
                3 => GetCipherCode(name),
                _ => GetAlphabetCode(name)
            };
        }

        /// <summary>
        /// Кодирование алфавитными позициями (А=1, Б=2, ...)
        /// </summary>
        private string GetAlphabetCode(string name)
        {
            string result = "";
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    // Для русских букв вычисляем позицию в алфавите
                    int position = c - 'А' + 1;
                    codes.Add($"{position:00}");
                }
                else if(char.IsLetter(c))
                {
                    // Для латинских букв
                    codes.Add($"{c}?");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[ПРОБЕЛ]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join("-", codes);
        }

        /// <summary>
        /// Кодирование азбукой Морзе
        /// </summary>
        private string GetMorseCode(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c))
                {
                    string morseSymbol = GetMorseSymbol(c);
                    codes.Add(morseSymbol);
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("/"); // Разделитель слов в азбуке Морзе
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
        }

        /// <summary>
        /// Получить символ азбуки Морзе для буквы
        /// </summary>
        private string GetMorseSymbol(char c)
        {
            // Приводим к верхнему регистру для единообразия
            char upperC = char.ToUpper(c);

            // Русский алфавит в азбуке Морзе
            return upperC switch
            {
                'А' => ".-",
                'Б' => "-...",
                'В' => ".--",
                'Г' => "--.",
                'Д' => "-..",
                'Е' => ".",
                'Ё' => ".",
                'Ж' => "...-",
                'З' => "--..",
                'И' => "..",
                'Й' => ".---",
                'К' => "-.-",
                'Л' => ".-..",
                'М' => "--",
                'Н' => "-.",
                'О' => "---",
                'П' => ".--.",
                'Р' => ".-.",
                'С' => "...",
                'Т' => "-",
                'У' => "..-",
                'Ф' => "..-.",
                'Х' => "....",
                'Ц' => "-.-.",
                'Ч' => "---.",
                'Ш' => "----",
                'Щ' => "--.-",
                'Ъ' => "--.--",
                'Ы' => "-.--",
                'Ь' => "-..-",
                'Э' => "..-..",
                'Ю' => "..--",
                'Я' => ".-.-",
                _ => "?" // Для не-русских букв
            };
        }

        /// <summary>
        /// Кодирование простым шифром (позиция × 3)
        /// </summary>
        private string GetCipherCode(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    // (позиция в алфавите) × 3
                    int encodedValue = (c - 'А' + 1) * 3;
                    codes.Add($"{encodedValue:000}");
                }
                else if(char.IsLetter(c))
                {
                    // Для латинских букв
                    codes.Add($"{c}?");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[ПРОБЕЛ]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
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