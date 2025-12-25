namespace EducationalProject.Services
{
    public class BinaryConverter : BaseService
    {
        private const int MAX_VALUE = 255;
        private const int MIN_VALUE = 0;
        private const int BITS_COUNT = 8;

        public override string Name => "Конвертер систем счисления";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует преобразование чисел",
                "между десятичной и двоичной системами счисления.",
                $"Диапазон: {MIN_VALUE} - {MAX_VALUE} (8-битные числа)"
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            ShowConversionOptions();

            int choice = InputValidator.GetValidMenuChoice(1, 2, ">>> Выберите тип конвертации (1 или 2): ");

            string input = choice == 1
                ? InputValidator.GetValidIntegerInRange(
                    $">>> Введите десятичное число (от {MIN_VALUE} до {MAX_VALUE}): ",
                    MIN_VALUE, MAX_VALUE).ToString()
                : InputValidator.GetValidBinary($">>> Введите двоичное число (до {BITS_COUNT} бит): ");

            string result = PerformConversion(choice, input);

            Console.WriteLine($"\nВходные данные: {input}");
            Console.WriteLine($"Результат: {result}");

            ShowAdditionalInfo();

            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        private void ShowConversionOptions()
        {
            string[] options = {
                "Десятичное → Двоичное (0-255)",
                "Двоичное → Десятичное (8 бит)"
            };

            ConsoleHelper.ShowMenu("ВАРИАНТЫ КОНВЕРТАЦИИ", options);
            Console.WriteLine();
        }

        private string PerformConversion(int choice, string input)
        {
            if(choice == 1)
            {
                if(int.TryParse(input, out int number))
                {
                    var binaryResult = InputValidator.DecimalToBinary(number, BITS_COUNT);
                    if(binaryResult != "Ошибка")
                    {
                        return $"{binaryResult.Substring(0, 4)} {binaryResult.Substring(4)} ({binaryResult}₂)";
                    }
                }
                return "Ошибка конвертации";
            }
            else
            {
                var decimalResult = InputValidator.BinaryToDecimal(input);
                if(decimalResult >= 0)
                {
                    return $"{decimalResult} ({decimalResult}₁₀)";
                }
                return "Ошибка конвертации";
            }
        }

        private void ShowAdditionalInfo()
        {
            string[] infoLines = {
                $"• Диапазон чисел: {MIN_VALUE} - {MAX_VALUE} (8-битное число)",
                $"• Двоичное представление: {BITS_COUNT} бит",
                $"• Максимальное значение: {MAX_VALUE} = 11111111₂",
                $"• Системы счисления:",
                $"  - Десятичная: основание 10 (0-9)",
                $"  - Двоичная: основание 2 (0-1)"
            };

            ConsoleHelper.ShowInfoBlock("ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ", infoLines);
        }
    }
}