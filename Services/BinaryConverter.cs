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
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует преобразование чисел",
                "между десятичной и двоичной системами счисления.",
                $"Диапазон: {MIN_VALUE} - {MAX_VALUE} (8-битные числа)"
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            ShowConversionOptions();

            int choice = ConsoleHelper.GetMenuChoice(1, 2, ">>> Выберите тип конвертации (1 или 2): ");
            string input = GetNumberInput(choice);
            string result = PerformConversion(choice, input);
            string explanation = GetConversionExplanation(choice, input, result);

            ShowConversionResult(input, result, explanation);
            ShowAdditionalInfo();

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Показать варианты конвертации
        /// </summary>
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
        /// Получить число для конвертации
        /// </summary>
        private string GetNumberInput(int choice)
        {
            if(choice == 1)
            {
                return ConsoleHelper.GetInput($">>> Введите десятичное число (от {MIN_VALUE} до {MAX_VALUE}): ");
            }
            else
            {
                return ConsoleHelper.GetInput($">>> Введите двоичное число (до {BITS_COUNT} бит): ");
            }
        }

        /// <summary>
        /// Показать результат конвертации
        /// </summary>
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
        /// Показать дополнительную информацию
        /// </summary>
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
        /// Выполнить конвертацию в зависимости от выбора
        /// </summary>
        private string PerformConversion(int choice, string input)
        {
            switch(choice)
            {
                case 1:
                    return ConvertDecimalToBinary(input);
                case 2:
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
            string paddedBinary = binaryString.PadLeft(BITS_COUNT, '0');

            // Форматируем с разделителями для наглядности
            return $"{paddedBinary.Substring(0, 4)} {paddedBinary.Substring(4)} ({paddedBinary}₂)";
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

            // Убираем пробелы для проверки
            string cleanInput = input.Replace(" ", "");

            // Проверка на допустимые символы (только 0 и 1)
            foreach(char c in cleanInput)
            {
                if(c != '0' && c != '1')
                {
                    return $"Ошибка: '{input}' содержит недопустимые символы! Используйте только 0 и 1.";
                }
            }

            // Проверка длины (не более 8 бит)
            if(cleanInput.Length > BITS_COUNT)
            {
                return $"Ошибка: Слишком длинное число! Максимум {BITS_COUNT} бит.";
            }

            try
            {
                // Конвертация из двоичной системы
                int decimalNumber = Convert.ToInt32(cleanInput, 2);
                return $"{decimalNumber} ({decimalNumber}₁₀)";
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
        private string GetConversionExplanation(int choice, string input, string result)
        {
            if(result.StartsWith("Ошибка"))
            {
                return "";
            }

            if(choice == 1)
            {
                if(int.TryParse(input, out int number))
                {
                    string binaryResult = result.Split(' ')[0].Replace(" ", "");
                    return $"{number}₁₀ → {binaryResult}₂";
                }
            }
            else if(choice == 2)
            {
                // Извлекаем десятичное число из результата
                string decimalStr = result.Split(' ')[0];
                if(int.TryParse(decimalStr, out int decimalResult))
                {
                    string cleanInput = input.Replace(" ", "");
                    return $"{cleanInput}₂ → {decimalResult}₁₀";
                }
            }

            return "";
        }

        #endregion
    }
}