namespace EducationalProject.Services
{
    /// <summary>
    /// Программа конвертера систем счисления.
    /// Предоставляет функциональность для преобразования чисел между десятичной и двоичной системами счисления.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс <see cref="BinaryConverter"/> демонстрирует:
    /// <list type="bullet">
    /// <item><description>Конвертацию десятичных чисел в двоичные (0-255)</description></item>
    /// <item><description>Конвертацию двоичных чисел в десятичные (8-битные)</description></item>
    /// <item><description>Валидацию пользовательского ввода</description></item>
    /// <item><description>Наглядное представление результатов с объяснениями</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Особенности реализации:</strong>
    /// <list type="bullet">
    /// <item><description>Ограничение диапазона чисел (0-255) для 8-битного представления</description></item>
    /// <item><description>Использование префиксов систем счисления (₂, ₁₀)</description></item>
    /// <item><description>Форматирование двоичных чисел с пробелами для читаемости</description></item>
    /// <item><description>Подробные объяснения процесса конвертации</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример использования программы:
    /// <code>
    /// // 1. Десятичное → Двоичное
    /// Вход: 42 (десятичное)
    /// Выход: 0010 1010 (00101010₂)
    /// Объяснение: 42₁₀ → 00101010₂
    /// 
    /// // 2. Двоичное → Десятичное
    /// Вход: 11001100 (двоичное)
    /// Выход: 204 (204₁₀)
    /// Объяснение: 11001100₂ → 204₁₀
    /// </code>
    /// </example>
    /// <seealso cref="BaseService"/>
    /// <seealso cref="InputValidator"/>
    public class BinaryConverter : BaseService
    {
        #region ===== КОНСТАНТЫ И ПОЛЯ =====

        /// <summary>
        /// Максимальное значение для конвертации (8-битное число)
        /// </summary>
        /// <value>255 (максимальное значение для unsigned byte)</value>
        private const int MAX_VALUE = 255;

        /// <summary>
        /// Минимальное значение для конвертации
        /// </summary>
        /// <value>0 (минимальное значение для unsigned byte)</value>
        private const int MIN_VALUE = 0;

        /// <summary>
        /// Количество бит для представления (8 бит = 1 байт)
        /// </summary>
        /// <value>8 бит</value>
        private const int BITS_COUNT = 8;

        #endregion

        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Получает название программы для отображения в меню
        /// </summary>
        /// <value>Строка "Конвертер систем счисления"</value>
        public override string Name => "Конвертер систем счисления";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Запускает программу конвертера систем счисления
        /// </summary>
        /// <remarks>
        /// <para>Метод выполняет следующий алгоритм:</para>
        /// <list type="number">
        /// <item><description>Очищает консоль и отображает заголовок программы</description></item>
        /// <item><description>Выводит описание программы и диапазон значений</description></item>
        /// <item><description>Предлагает выбор типа конвертации</description></item>
        /// <item><description>Запрашивает и валидирует ввод пользователя</description></item>
        /// <item><description>Выполняет конвертацию и показывает результат</description></item>
        /// <item><description>Выводит дополнительную информацию о системах счисления</description></item>
        /// <item><description>Ожидает нажатия клавиши для возврата в меню</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Запуск программы
        /// BinaryConverter converter = new BinaryConverter();
        /// converter.Run();
        /// 
        /// // Пример последовательности выполнения:
        /// // 1. Показывается заголовок "Конвертер систем счисления"
        /// // 2. Выводится описание программы
        /// // 3. Предлагается выбор: "Десятичное → Двоичное" или "Двоичное → Десятичное"
        /// // 4. Пользователь вводит число
        /// // 5. Отображается результат конвертации
        /// // 6. Выводится дополнительная информация
        /// </code>
        /// </example>
        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует преобразование чисел",
                "между десятичной и двоичной системами счисления.",
                $"Диапазон: {MIN_VALUE} - {MAX_VALUE} (8-битные числа)"
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            ShowConversionOptions();

            int choice = InputValidator.GetValidMenuChoice(1, 2, ">>> Выберите тип конвертации (1 или 2): ");

            string input = choice == 1
                ? InputValidator.GetValidIntegerInRange($">>> Введите десятичное число (от {MIN_VALUE} до {MAX_VALUE}): ",
                    MIN_VALUE, MAX_VALUE, "Десятичное число").ToString()
                : InputValidator.GetValidBinary($">>> Введите двоичное число (до {BITS_COUNT} бит): ", BITS_COUNT);

            string result = PerformConversion(choice, input);
            string explanation = GetConversionExplanation(choice, input, result);

            ShowConversionResult(input, result, explanation);
            ShowAdditionalInfo();

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Отображает варианты конвертации для пользователя
        /// </summary>
        /// <remarks>
        /// Использует <see cref="ConsoleHelper.ShowMenu"/> для форматированного вывода меню с заголовком.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Вывод в консоль:
        /// ╔══════════════════════════════════════╗
        /// ║       ВАРИАНТЫ КОНВЕРТАЦИИ           ║
        /// ╠══════════════════════════════════════╣
        /// ║ 1. Десятичное → Двоичное (0-255)     ║
        /// ║ 2. Двоичное → Десятичное (8 бит)     ║
        /// ╚══════════════════════════════════════╝
        /// </code>
        /// </example>
        private void ShowConversionOptions()
        {
            string[] options = {
                "Десятичное → Двоичное (0-255)",
                "Двоичное → Десятичное (8 бит)"
            };

            ConsoleHelper.ShowMenu("ВАРИАНТЫ КОНВЕРТАЦИИ", options);
            Console.WriteLine();
        }

        /// <summary>
        /// Отображает результат конвертации с форматированием
        /// </summary>
        /// <param name="input">Входные данные (число для конвертации)</param>
        /// <param name="result">Результат конвертации</param>
        /// <param name="explanation">Объяснение процесса конвертации</param>
        /// <remarks>
        /// <para>Метод обрабатывает два типа результатов:</para>
        /// <list type="bullet">
        /// <item><description><strong>Успешная конвертация:</strong> Форматирует результат как уравнение</description></item>
        /// <item><description><strong>Ошибка:</strong> Выводит сообщение об ошибке через <see cref="ConsoleHelper.ShowError"/></description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Успешная конвертация:
        /// ShowConversionResult("42", "0010 1010 (00101010₂)", "42₁₀ → 00101010₂");
        /// 
        /// // Вывод:
        /// // Входные данные: 42
        /// // Результат: 0010 1010 (00101010₂)
        /// // 
        /// // 42₁₀ → 00101010₂
        /// 
        /// // Ошибка:
        /// ShowConversionResult("999", "Ошибка: Число вне диапазона", "");
        /// 
        /// // Вывод красным цветом:
        /// // [ ОШИБКА ] Число вне диапазона
        /// </code>
        /// </example>
        private void ShowConversionResult(string input, string result, string explanation)
        {
            Console.WriteLine();

            if(result.StartsWith("Ошибка"))
            {
                ConsoleHelper.ShowError(result.Substring(7)); // Убираем "Ошибка: "
                return;
            }

            string[] resultLines;

            if(explanation.Contains("→"))
            {
                // Форматируем как уравнение
                string[] parts = explanation.Split(" = ");
                resultLines = new string[] {
                    $"Входные данные: {input}",
                    $"Результат: {result}",
                    "",
                    parts[0],
                    parts[1]
                };
            }
            else
            {
                resultLines = new string[] {
                    $"Входные данные: {input}",
                    $"Результат: {result}",
                    "",
                    explanation
                };
            }

            ConsoleHelper.ShowInfoBlock("РЕЗУЛЬТАТ КОНВЕРТАЦИИ", resultLines, 45);
        }

        /// <summary>
        /// Отображает дополнительную информацию о системах счисления
        /// </summary>
        /// <remarks>
        /// Выводит информацию через <see cref="ConsoleHelper.ShowInfoBlock"/> с заголовком "ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ".
        /// Содержит технические детали о диапазонах, представлениях и обозначениях.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Вывод в консоль:
        /// ╔══════════════════════════════════════════════════════╗
        /// ║          ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ                   ║
        /// ╠══════════════════════════════════════════════════════╣
        /// ║ • Диапазон чисел: 0 - 255 (8-битное число)           ║
        /// ║ • Двоичное представление: 8 бит                      ║
        /// ║ • Максимальное значение: 255 = 11111111₂             ║
        /// ║ • Системы счисления:                                 ║
        /// ║   - Десятичная: основание 10 (0-9)                   ║
        /// ║   - Двоичная: основание 2 (0-1)                      ║
        /// ║ • Префиксы:                                          ║
        /// ║   - ₂ - двоичная система                             ║
        /// ║   - ₁₀ - десятичная система                          ║
        /// ╚══════════════════════════════════════════════════════╝
        /// </code>
        /// </example>
        private void ShowAdditionalInfo()
        {
            string[] infoLines = {
                $"• Диапазон чисел: {MIN_VALUE} - {MAX_VALUE} (8-битное число)",
                $"• Двоичное представление: {BITS_COUNT} бит",
                $"• Максимальное значение: {MAX_VALUE} = 11111111₂",
                $"• Системы счисления:",
                $"  - Десятичная: основание 10 (0-9)",
                $"  - Двоичная: основание 2 (0-1)",
                $"• Префиксы:",
                $"  - ₂ - двоичная система",
                $"  - ₁₀ - десятичная система"
            };

            ConsoleHelper.ShowInfoBlock("ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ", infoLines);
        }

        #endregion

        #region ===== МЕТОДЫ КОНВЕРТАЦИИ =====

        /// <summary>
        /// Выполняет конвертацию в зависимости от выбора пользователя
        /// </summary>
        /// <param name="choice">Тип конвертации: 1 - десятичное→двоичное, 2 - двоичное→десятичное</param>
        /// <param name="input">Входные данные для конвертации</param>
        /// <returns>Результат конвертации в виде строки или сообщение об ошибке</returns>
        /// <remarks>
        /// <para>Использует switch expression для выбора метода конвертации:</para>
        /// <list type="bullet">
        /// <item><description>1 → <see cref="ConvertDecimalToBinary"/></description></item>
        /// <item><description>2 → <see cref="ConvertBinaryToDecimal"/></description></item>
        /// <item><description>default → <see cref="ConvertDecimalToBinary"/> (резервный вариант)</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// string result1 = PerformConversion(1, "42"); 
        /// // Возвращает: "0010 1010 (00101010₂)"
        /// 
        /// string result2 = PerformConversion(2, "11001100"); 
        /// // Возвращает: "204 (204₁₀)"
        /// 
        /// string error = PerformConversion(1, "999"); 
        /// // Возвращает: "Ошибка: Число вне диапазона (0-255)"
        /// </code>
        /// </example>
        private string PerformConversion(int choice, string input)
        {
            return choice switch
            {
                1 => ConvertDecimalToBinary(input),
                2 => ConvertBinaryToDecimal(input),
                _ => ConvertDecimalToBinary(input)
            };
        }

        /// <summary>
        /// Конвертирует десятичное число в двоичное представление
        /// </summary>
        /// <param name="input">Строка, содержащая десятичное число</param>
        /// <returns>Форматированное двоичное представление или сообщение об ошибке</returns>
        /// <remarks>
        /// <para>Алгоритм работы:</para>
        /// <list type="number">
        /// <item><description>Пытается преобразовать строку в число типа int</description></item>
        /// <item><description>Использует <see cref="InputValidator.DecimalToBinary"/> для конвертации</description></item>
        /// <item><description>При успехе форматирует результат с пробелами для читаемости</description></item>
        /// <item><description>При ошибке возвращает сообщение об ошибке</description></item>
        /// </list>
        /// <para>Формат результата: "XXXX XXXX (XXXXXXXX₂)" где X - биты (0 или 1)</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string result = ConvertDecimalToBinary("42");
        /// // Возвращает: "0010 1010 (00101010₂)"
        /// 
        /// string error = ConvertDecimalToBinary("abc");
        /// // Возвращает: "Ошибка: Не удалось преобразовать 'abc' в число"
        /// </code>
        /// </example>
        private string ConvertDecimalToBinary(string input)
        {
            if(int.TryParse(input, out int number))
            {
                var binaryResult = InputValidator.DecimalToBinary(number, BITS_COUNT);
                if(binaryResult.IsValid)
                {
                    string binaryString = (string)binaryResult.Value;
                    // Форматируем с разделителями для наглядности
                    string formattedBinary = $"{binaryString.Substring(0, 4)} {binaryString.Substring(4)} ({binaryString}₂)";
                    return formattedBinary;
                }
                return binaryResult.ErrorMessage;
            }

            return $"Ошибка: Не удалось преобразовать '{input}' в число";
        }

        /// <summary>
        /// Конвертирует двоичное число в десятичное представление
        /// </summary>
        /// <param name="input">Строка, содержащая двоичное число (только 0 и 1)</param>
        /// <returns>Десятичное число с указанием системы счисления или сообщение об ошибке</returns>
        /// <remarks>
        /// <para>Использует <see cref="InputValidator.BinaryToDecimal"/> для конвертации.</para>
        /// <para>Формат результата: "ЧИСЛО (ЧИСЛО₁₀)"</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string result = ConvertBinaryToDecimal("11001100");
        /// // Возвращает: "204 (204₁₀)"
        /// 
        /// string error = ConvertBinaryToDecimal("10201");
        /// // Возвращает: "Ошибка: Двоичное число должно содержать только 0 и 1"
        /// </code>
        /// </example>
        private string ConvertBinaryToDecimal(string input)
        {
            var decimalResult = InputValidator.BinaryToDecimal(input);
            if(decimalResult.IsValid)
            {
                int decimalNumber = (int)decimalResult.Value;
                return $"{decimalNumber} ({decimalNumber}₁₀)";
            }
            return decimalResult.ErrorMessage;
        }

        /// <summary>
        /// Генерирует объяснение процесса конвертации
        /// </summary>
        /// <param name="choice">Тип конвертации (1 или 2)</param>
        /// <param name="input">Входные данные</param>
        /// <param name="result">Результат конвертации</param>
        /// <returns>Строка с объяснением конвертации в формате "ЧИСЛО₁₀ → ЧИСЛО₂"</returns>
        /// <remarks>
        /// <para>Метод анализирует результат конвертации и генерирует понятное объяснение:</para>
        /// <list type="bullet">
        /// <item><description>Для choice=1: "десятичное₁₀ → двоичное₂"</description></item>
        /// <item><description>Для choice=2: "двоичное₂ → десятичное₁₀"</description></item>
        /// </list>
        /// <para>Возвращает пустую строку, если результат содержит ошибку.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// string explanation1 = GetConversionExplanation(1, "42", "0010 1010 (00101010₂)");
        /// // Возвращает: "42₁₀ → 00101010₂"
        /// 
        /// string explanation2 = GetConversionExplanation(2, "11001100", "204 (204₁₀)");
        /// // Возвращает: "11001100₂ → 204₁₀"
        /// 
        /// string empty = GetConversionExplanation(1, "999", "Ошибка: ...");
        /// // Возвращает: "" (пустая строка)
        /// </code>
        /// </example>
        private string GetConversionExplanation(int choice, string input, string result)
        {
            if(result.StartsWith("Ошибка"))
            {
                return "";
            }

            if(choice == 1 && int.TryParse(input, out int decimalNumber))
            {
                string binaryResult = result.Split(' ')[0].Replace(" ", "");
                return $"{decimalNumber}₁₀ → {binaryResult}₂";
            }
            else if(choice == 2)
            {
                string decimalStr = result.Split(' ')[0];
                if(int.TryParse(decimalStr, out int decimalResult))
                {
                    return $"{input}₂ → {decimalResult}₁₀";
                }
            }

            return "";
        }

        #endregion
    }
}