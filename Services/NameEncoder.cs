namespace EducationalProject.Services
{
    /// <summary>
    /// Программа "Кодировщик имени" - демонстрирует различные методы кодирования текста.
    /// Позволяет пользователю ввести имя и преобразовать его с помощью выбранного алгоритма кодирования.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Эта программа реализует три метода кодирования текста:
    /// <list type="number">
    /// <item><description><b>Алфавитные позиции</b> - замена каждой буквы её порядковым номером в алфавите (А=1, Б=2, ..., Я=33)</description></item>
    /// <item><description><b>Азбука Морзе</b> - преобразование букв в соответствующие последовательности точек и тире</description></item>
    /// <item><description><b>Простой шифр</b> - вычисление значения по формуле (позиция в алфавите × 3)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <b>Особенности реализации:</b>
    /// <list type="bullet">
    /// <item><description>Поддержка только русских букв для полноценного кодирования</description></item>
    /// <item><description>Автоматическое приведение введенного имени к верхнему регистру</description></item>
    /// <item><description>Валидация ввода с помощью <see cref="InputValidator"/></description></item>
    /// <item><description>Наглядное отображение результатов с использованием <see cref="ConsoleHelper"/></description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример работы программы:
    /// <code>
    /// Введите ваше имя: Иван
    /// 
    /// Доступные методы кодирования:
    /// 1. Алфавитные позиции
    /// 2. Азбука Морзе
    /// 3. Простой шифр
    /// 
    /// Выберите метод: 2
    /// 
    /// Результат:
    /// Исходное имя: ИВАН
    /// Метод кодирования: Азбука Морзе
    /// Закодированное имя: .. .-- .- -.
    /// </code>
    /// </example>
    /// <seealso cref="BaseService"/>
    /// <seealso cref="ConsoleHelper"/>
    /// <seealso cref="InputValidator"/>
    public class NameEncoder : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Получает название программы для отображения в меню.
        /// </summary>
        /// <value>Строка "Кодировщик имени".</value>
        /// <remarks>
        /// Это свойство переопределяет абстрактное свойство <see cref="BaseService.Name"/>.
        /// Используется <see cref="MenuManager"/> для отображения в списке доступных программ.
        /// </remarks>
        public override string Name => "Кодировщик имени";

        #endregion

        #region ===== ТЕКСТОВЫЕ КОНСТАНТЫ =====

        /// <summary>
        /// Заголовок для меню методов кодирования
        /// </summary>
        private const string ENCODING_METHODS_TITLE = "МЕТОДЫ КОДИРОВАНИЯ";

        /// <summary>
        /// Заголовок для отображения результата кодирования
        /// </summary>
        private const string ENCODING_RESULT_TITLE = "РЕЗУЛЬТАТ КОДИРОВАНИЯ";

        /// <summary>
        /// Приглашение для ввода имени
        /// </summary>
        private const string PROMPT_ENTER_NAME = ">>> Введите ваше имя: ";

        /// <summary>
        /// Приглашение для выбора метода кодирования
        /// </summary>
        private const string PROMPT_CHOOSE_METHOD = ">>> Выберите метод кодирования (1-3): ";

        /// <summary>
        /// Метка для исходного имени
        /// </summary>
        private const string LABEL_ORIGINAL_NAME = "Исходное имя";

        /// <summary>
        /// Метка для метода кодирования
        /// </summary>
        private const string LABEL_ENCODING_METHOD = "Метод кодирования";

        /// <summary>
        /// Метка для закодированного имени
        /// </summary>
        private const string LABEL_ENCODED_NAME = "Закодированное имя";

        /// <summary>
        /// Текст для неизвестного метода
        /// </summary>
        private const string UNKNOWN_METHOD = "Неизвестный метод";

        /// <summary>
        /// Текст для латинских букв
        /// </summary>
        private const string LATIN_LETTER_SUFFIX = "?";

        /// <summary>
        /// Текст для пробела
        /// </summary>
        private const string SPACE_REPLACEMENT = "[ПРОБЕЛ]";

        /// <summary>
        /// Разделитель слов в азбуке Морзе
        /// </summary>
        private const string MORSE_WORD_SEPARATOR = "/";

        /// <summary>
        /// Неизвестный символ Морзе
        /// </summary>
        private const string MORSE_UNKNOWN_SYMBOL = "?";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Главный метод запуска программы кодировщика имени.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Последовательность выполнения:
        /// <list type="number">
        /// <item><description>Очистка консоли и отображение заголовка</description></item>
        /// <item><description>Вывод описания программы</description></item>
        /// <item><description>Ввод и валидация имени пользователя</description></item>
        /// <item><description>Отображение доступных методов кодирования</description></item>
        /// <item><description>Выбор метода пользователем</description></item>
        /// <item><description>Кодирование имени и отображение результата</description></item>
        /// <item><description>Ожидание нажатия клавиши для возврата в меню</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Введенное имя автоматически преобразуется к верхнему регистру для единообразия кодирования.
        /// </para>
        /// </remarks>
        /// <exception cref="Exception">
        /// Может возникнуть при ошибках ввода-вывода или некорректной работе методов кодирования.
        /// </exception>
        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует различные методы кодирования текста:",
                "1. Алфавитные позиции (А=1, Б=2, ...)",
                "2. Азбука Морзе",
                "3. Простой шифр (позиция × 3)"
            };

            // Используем константу из базового класса
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            string name = InputValidator.GetValidText(
                PROMPT_ENTER_NAME,
                minLength: 1,
                maxLength: 50,
                allowDigits: false,
                allowSpecialChars: false
            ).ToUpper();

            ShowEncodingMethods();

            int choice = InputValidator.GetValidMenuChoice(1, 3, PROMPT_CHOOSE_METHOD);
            string encodedName = EncodeName(name, choice);
            ShowResult(name, encodedName, choice);

            // Используем константу из базового класса
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        /// <summary>
        /// Получает название метода кодирования по его номеру.
        /// </summary>
        /// <param name="choice">Номер метода (1-3).</param>
        /// <returns>Название метода кодирования.</returns>
        /// <remarks>
        /// Использует конструкцию switch expression для соответствия номера и названия.
        /// </remarks>
        /// <example>
        /// <code>
        /// string method = GetMethodName(2); // Возвращает "Азбука Морзе"
        /// </code>
        /// </example>
        private string GetMethodName(int choice)
        {
            return choice switch
            {
                1 => "Алфавитные позиции",
                2 => "Азбука Морзе",
                3 => "Простой шифр",
                _ => UNKNOWN_METHOD
            };
        }

        /// <summary>
        /// Получает описание метода кодирования по его номеру.
        /// </summary>
        /// <param name="choice">Номер метода (1-3).</param>
        /// <returns>Краткое описание принципа работы метода.</returns>
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
        /// Отображает меню доступных методов кодирования.
        /// </summary>
        /// <remarks>
        /// Использует метод <see cref="ConsoleHelper.ShowMenu"/> для стандартизированного отображения.
        /// </remarks>
        private void ShowEncodingMethods()
        {
            string[] methods = {
                "Алфавитные позиции (А=1, Б=2, ...)",
                "Азбука Морзе",
                "Простой шифр (позиция × 3)"
            };

            // Используем константу
            ConsoleHelper.ShowMenu(ENCODING_METHODS_TITLE, methods);
            Console.WriteLine();
        }

        /// <summary>
        /// Отображает результат кодирования имени.
        /// </summary>
        /// <param name="originalName">Исходное имя.</param>
        /// <param name="encodedName">Закодированное имя.</param>
        /// <param name="methodChoice">Номер выбранного метода.</param>
        /// <remarks>
        /// Форматирует вывод в информационный блок с использованием <see cref="ConsoleHelper.ShowInfoBlock"/>.
        /// </remarks>
        private void ShowResult(string originalName, string encodedName, int methodChoice)
        {
            string methodName = GetMethodName(methodChoice);
            string methodDescription = GetMethodDescription(methodChoice);

            string[] resultLines = {
                $"{LABEL_ORIGINAL_NAME}: {originalName}",
                $"{LABEL_ENCODING_METHOD}: {methodName}",
                "",
                methodDescription,
                "",
                $"{LABEL_ENCODED_NAME}:",
                $"  {encodedName}"
            };

            // Используем константу
            ConsoleHelper.ShowInfoBlock(ENCODING_RESULT_TITLE, resultLines, 50);
        }

        #endregion

        #region ===== МЕТОДЫ КОДИРОВАНИЯ =====

        /// <summary>
        /// Кодирует имя выбранным методом.
        /// </summary>
        /// <param name="name">Имя для кодирования.</param>
        /// <param name="methodChoice">Номер метода кодирования.</param>
        /// <returns>Закодированная строка.</returns>
        /// <remarks>
        /// Делегирует выполнение соответствующим методам кодирования.
        /// При некорректном выборе метода используется кодирование алфавитными позициями.
        /// </remarks>
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
        /// Кодирует имя с использованием алфавитных позиций.
        /// </summary>
        /// <param name="name">Имя для кодирования.</param>
        /// <returns>
        /// Строка, где каждая буква заменена её двузначным номером в алфавите,
        /// разделенным дефисами (например, "И-В-А-Н" → "10-03-01-15").
        /// </returns>
        /// <remarks>
        /// <para>
        /// Правила кодирования:
        /// <list type="bullet">
        /// <item><description>Русские буквы: заменяются номером в алфавите (А=01, Б=02, ..., Я=33)</description></item>
        /// <item><description>Латинские буквы: заменяются символом с вопросительным знаком (A → "A?")</description></item>
        /// <item><description>Пробелы: заменяются на "[ПРОБЕЛ]"</description></item>
        /// <item><description>Прочие символы: выводятся без изменений</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        private string GetAlphabetCode(string name)
        {
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
                    // Для латинских букв - используем константу
                    codes.Add($"{c}{LATIN_LETTER_SUFFIX}");
                }
                else if(char.IsWhiteSpace(c))
                {
                    // Используем константу
                    codes.Add(SPACE_REPLACEMENT);
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join("-", codes);
        }

        /// <summary>
        /// Кодирует имя с использованием азбуки Морзе.
        /// </summary>
        /// <param name="name">Имя для кодирования.</param>
        /// <returns>
        /// Строка с последовательностью символов Морзе, разделенных пробелами.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Особенности:
        /// <list type="bullet">
        /// <item><description>Пробелы заменяются на "/" (стандартный разделитель слов в азбуке Морзе)</description></item>
        /// <item><description>Символы разделяются пробелами</description></item>
        /// <item><description>Для не-русских букв возвращается "?"</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Использует стандартные коды Морзе для русских букв.
        /// </para>
        /// </remarks>
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
                    // Используем константу
                    codes.Add(MORSE_WORD_SEPARATOR);
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
        }

        /// <summary>
        /// Получает символ азбуки Морзе для указанной буквы.
        /// </summary>
        /// <param name="c">Буква для преобразования.</param>
        /// <returns>Строка с символами Морзе (точки и тире).</returns>
        /// <remarks>
        /// <para>
        /// Поддерживает только русские буквы. Для не-русских букв возвращает "?".
        /// </para>
        /// <para>
        /// Обратите внимание: буквы 'Е' и 'Ё' имеют одинаковое кодирование в азбуке Морзе.
        /// </para>
        /// </remarks>
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
                _ => MORSE_UNKNOWN_SYMBOL // Для не-русских букв
            };
        }

        /// <summary>
        /// Кодирует имя с использованием простого шифра (позиция × 3).
        /// </summary>
        /// <param name="name">Имя для кодирования.</param>
        /// <returns>
        /// Строка, где каждая буква заменена результатом вычисления (позиция × 3),
        /// разделенным пробелами.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Формула: encodedValue = (positionInAlphabet) × 3
        /// где positionInAlphabet - порядковый номер буквы в алфавите (А=1, Б=2, ...).
        /// </para>
        /// <para>
        /// Значения форматируются как трехзначные числа (001, 003, ..., 099).
        /// </para>
        /// </remarks>
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
                    // Для латинских букв - используем константу
                    codes.Add($"{c}{LATIN_LETTER_SUFFIX}");
                }
                else if(char.IsWhiteSpace(c))
                {
                    // Используем константу
                    codes.Add(SPACE_REPLACEMENT);
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
        /// Проверяет, является ли символ русской буквой.
        /// </summary>
        /// <param name="c">Проверяемый символ.</param>
        /// <returns>
        /// <c>true</c>, если символ является русской буквой (А-Я, а-я);
        /// иначе <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Проверяет диапазоны кодов Unicode для русских букв.
        /// Учитывает как заглавные, так и строчные буквы.
        /// </remarks>
        private bool IsRussianLetter(char c)
        {
            return (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я');
        }

        #endregion
    }
}