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
            return choice switch
            {
                1 => ConvertDecimalToBinary(input),
                2 => ConvertBinaryToDecimal(input),
                _ => ConvertDecimalToBinary(input)
            };
        }

        /// <summary>
        /// Конвертировать десятичное число в двоичное
        /// </summary>
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
        /// Конвертировать двоичное число в десятичное
        /// </summary>
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
        /// Получить объяснение конвертации
        /// </summary>
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