namespace EducationalProject.Services
{
    /// <summary>
    /// Программа 2: Конвертер систем счисления
    /// Демонстрирует преобразование чисел между десятичной и двоичной системами счисления
    /// </summary>
    public class BinaryConverter : BaseService
    {
        #region ===== КОНСТАНТЫ И ПОЛЯ =====

        /// <summary>
        /// Максимальное значение для конвертации (8-битное число)
        /// </summary>
        private const int MAX_VALUE = 255;

        /// <summary>
        /// Минимальное значение для конвертации
        /// </summary>
        private const int MIN_VALUE = 0;

        /// <summary>
        /// Количество бит для представления (8 бит = 1 байт)
        /// </summary>
        private const int BITS_COUNT = 8;

        #endregion

        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Название программы для отображения в меню
        /// </summary>
        public override string Name => "Конвертер систем счисления";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Главный метод запуска программы
        /// </summary>
        public override void Run()
        {
            ShowHeader(); // Показать заголовок
            ShowConversionOptions(); // Показать варианты конвертации

            string choice = GetConversionChoice(); // Получить выбор пользователя
            string input = GetNumberInput(choice); // Получить число для конвертации

            string result = PerformConversion(choice, input); // Выполнить конвертацию
            string explanation = GetConversionExplanation(choice, input, result); // Получить объяснение

            ShowConversionResult(input, result, explanation); // Показать результат

            ShowAdditionalInfo(); // Показать дополнительную информацию

            WaitForContinue(); // Ожидать подтверждения
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Показать заголовок программы
        /// </summary>
        private void ShowHeader()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine("        КОНВЕРТЕР СИСТЕМ СЧИСЛЕНИЯ           ");
            Console.WriteLine("═══════════════════════════════════════════════\n");

            Console.WriteLine("Программа демонстрирует преобразование чисел");
            Console.WriteLine("между десятичной и двоичной системами счисления.\n");
        }

        /// <summary>
        /// Показать варианты конвертации
        /// </summary>
        private void ShowConversionOptions()
        {
            Console.WriteLine("════════════ ВАРИАНТЫ КОНВЕРТАЦИИ ════════════");
            Console.WriteLine("1. Десятичное → Двоичное (0-255)");
            Console.WriteLine("2. Двоичное → Десятичное (8 бит)");
            Console.WriteLine("═══════════════════════════════════════════════\n");
        }

        /// <summary>
        /// Получить выбор типа конвертации
        /// </summary>
        private string GetConversionChoice()
        {
            Console.Write(">>> Выберите тип конвертации (1 или 2): ");
            string choice = Console.ReadLine()?.Trim() ?? "1";

            if(choice != "1" && choice != "2")
            {
                Console.WriteLine("Неверный выбор. Используется конвертация 1 (десятичное → двоичное).");
                choice = "1";
            }

            return choice;
        }

        /// <summary>
        /// Получить число для конвертации
        /// </summary>
        private string GetNumberInput(string choice)
        {
            if(choice == "1")
            {
                Console.Write($">>> Введите десятичное число (от {MIN_VALUE} до {MAX_VALUE}): ");
            }
            else
            {
                Console.Write($">>> Введите двоичное число (до {BITS_COUNT} бит): ");
            }

            return Console.ReadLine()?.Trim() ?? "";
        }

        /// <summary>
        /// Показать результат конвертации
        /// </summary>
        private void ShowConversionResult(string input, string result, string explanation)
        {
            Console.WriteLine("\n════════════════ РЕЗУЛЬТАТ ════════════════");

            if(result.StartsWith("Ошибка"))
            {
                ShowErrorMessage(result);
            }
            else
            {
                Console.WriteLine($"Входные данные: {input}");
                Console.WriteLine($"Результат: {result}");

                if(!string.IsNullOrEmpty(explanation))
                {
                    Console.WriteLine($"\n{explanation}");
                }
            }

            Console.WriteLine("═══════════════════════════════════════════════");
        }

        /// <summary>
        /// Показать дополнительную информацию
        /// </summary>
        private void ShowAdditionalInfo()
        {
            Console.WriteLine("\n════════════ ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ ════════════");
            Console.WriteLine($"• Диапазон чисел: {MIN_VALUE} - {MAX_VALUE} (8-битное число)");
            Console.WriteLine($"• Двоичное представление: {BITS_COUNT} бит");
            Console.WriteLine($"• Максимальное значение: {MAX_VALUE} = 11111111₂");
            Console.WriteLine($"• Системы счисления:");
            Console.WriteLine($"  - Десятичная: основание 10 (0-9)");
            Console.WriteLine($"  - Двоичная: основание 2 (0-1)");
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

        #region ===== МЕТОДЫ КОНВЕРТАЦИИ =====

        /// <summary>
        /// Выполнить конвертацию в зависимости от выбора
        /// </summary>
        private string PerformConversion(string choice, string input)
        {
            switch(choice)
            {
                case "1":
                    return ConvertDecimalToBinary(input);
                case "2":
                    return ConvertBinaryToDecimal(input);
                default:
                    return ConvertDecimalToBinary(input);
            }
        }

        /// <summary>
        /// Конвертировать десятичное число в двоичное
        /// </summary>
        private string ConvertDecimalToBinary(string input)
        {
            // Проверка на пустой ввод
            if(string.IsNullOrWhiteSpace(input))
            {
                return "Ошибка: Введите число!";
            }

            // Попытка преобразовать строку в число
            if(!int.TryParse(input, out int number))
            {
                return $"Ошибка: '{input}' не является числом!";
            }

            // Проверка диапазона
            if(number < MIN_VALUE || number > MAX_VALUE)
            {
                return $"Ошибка: Число должно быть от {MIN_VALUE} до {MAX_VALUE}!";
            }

            // Конвертация в двоичную систему
            string binaryString = Convert.ToString(number, 2);

            // Дополнение нулями слева до 8 бит
            return binaryString.PadLeft(BITS_COUNT, '0');
        }

        /// <summary>
        /// Конвертировать двоичное число в десятичное
        /// </summary>
        private string ConvertBinaryToDecimal(string input)
        {
            // Проверка на пустой ввод
            if(string.IsNullOrWhiteSpace(input))
            {
                return "Ошибка: Введите двоичное число!";
            }

            // Проверка на допустимые символы (только 0 и 1)
            foreach(char c in input)
            {
                if(c != '0' && c != '1')
                {
                    return $"Ошибка: '{input}' содержит недопустимые символы! Используйте только 0 и 1.";
                }
            }

            // Проверка длины (не более 8 бит)
            if(input.Length > BITS_COUNT)
            {
                return $"Ошибка: Слишком длинное число! Максимум {BITS_COUNT} бит.";
            }

            try
            {
                // Конвертация из двоичной системы
                int decimalNumber = Convert.ToInt32(input, 2);
                return decimalNumber.ToString();
            }
            catch(FormatException)
            {
                return "Ошибка: Неверный формат двоичного числа!";
            }
            catch(OverflowException)
            {
                return $"Ошибка: Число слишком большое! Максимум {BITS_COUNT} бит.";
            }
            catch
            {
                return "Ошибка: Неизвестная ошибка при конвертации!";
            }
        }

        /// <summary>
        /// Получить объяснение конвертации
        /// </summary>
        private string GetConversionExplanation(string choice, string input, string result)
        {
            if(result.StartsWith("Ошибка"))
            {
                return "";
            }

            if(choice == "1")
            {
                if(int.TryParse(input, out int number))
                {
                    return $"Объяснение: {number}₁₀ = {result}₂";
                }
            }
            else if(choice == "2")
            {
                if(int.TryParse(result, out int decimalResult))
                {
                    return $"Объяснение: {input}₂ = {decimalResult}₁₀";
                }
            }

            return "";
        }

        #endregion
    }
}