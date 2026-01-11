using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class BinaryConverterView
    {
        public int GetConversionChoice()
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            consoleHelper.ShowMenu(
                AppConfig.BinaryConverterConfig.Messages.Title,
                new string[] {
                    AppConfig.BinaryConverterConfig.Messages.DecimalToBinary,
                    AppConfig.BinaryConverterConfig.Messages.BinaryToDecimal
                }
            );

            return InputValidator.GetValidMenuChoice(
                minValue: 1,
                maxValue: 2,
                prompt: ">>> (1-2): "
            );
        }

        public int GetDecimalInput()
        {
            Console.WriteLine("\nВВОД ДЕСЯТИЧНОГО ЧИСЛА:");
            Console.WriteLine(new string('-', 30));

            return InputValidator.GetValidIntegerInRange(
                AppConfig.BinaryConverterConfig.MinValue,
                AppConfig.BinaryConverterConfig.MaxValue,
                $"Введите число от {AppConfig.BinaryConverterConfig.MinValue} до {AppConfig.BinaryConverterConfig.MaxValue}: "
            );
        }

        public string GetBinaryInput()
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            Console.WriteLine("\nВВОД ДВОИЧНОГО ЧИСЛА:");
            Console.WriteLine(new string('-', 30));

            Console.WriteLine($"Введите двоичное число (до {AppConfig.BinaryConverterConfig.BitsCount} бит):");
            Console.WriteLine("Пример: 10101010 или 1010 1010");

            while(true)
            {
                string input = consoleHelper.GetInput(">>> ");
                string cleanInput = input.Replace(" ", "");

                if(string.IsNullOrWhiteSpace(cleanInput))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(cleanInput.Length > AppConfig.BinaryConverterConfig.BitsCount)
                {
                    consoleHelper.ShowError($"Слишком длинное число! Максимум {AppConfig.BinaryConverterConfig.BitsCount} бит.");
                    continue;
                }

                foreach(char c in cleanInput)
                {
                    if(c != '0' && c != '1')
                    {
                        consoleHelper.ShowError("Двоичное число должно содержать только 0 и 1!");
                        continue;
                    }
                }

                return cleanInput;
            }
        }

        public void ShowResult(BinaryConversion conversion, int choice)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            Console.WriteLine("\n" + new string('=', 40));

            if(!conversion.IsValid)
            {
                consoleHelper.ShowError("Ошибка преобразования!");
                return;
            }

            if(choice == 1) // Decimal → Binary
            {
                Console.WriteLine($"ДЕСЯТИЧНОЕ: {conversion.DecimalValue}");
                Console.WriteLine($"ДВОИЧНОЕ: {conversion.FormattedBinary}");
                Console.WriteLine($"         ({conversion.BinaryValue})");
            }
            else // Binary → Decimal
            {
                Console.WriteLine($"ДВОИЧНОЕ: {conversion.BinaryValue}");
                Console.WriteLine($"ДЕСЯТИЧНОЕ: {conversion.DecimalValue}");
            }

            Console.WriteLine(new string('-', 40));
            consoleHelper.ShowSuccess("Преобразование выполнено успешно!");
            Console.WriteLine(new string('=', 40));
        }

        public void ShowAdditionalInfo()
        {
            Console.WriteLine("\n" + new string('*', 40));
            Console.WriteLine("ПОЛЕЗНАЯ ИНФОРМАЦИЯ:");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("• Десятичная система: цифры 0-9");
            Console.WriteLine("• Двоичная система: цифры 0 и 1");
            Console.WriteLine($"• 1 байт = {AppConfig.BinaryConverterConfig.BitsCount} бит");
            Console.WriteLine($"• Максимальное значение: 11111111 = 255");
            Console.WriteLine("• 1010 1010 = 170 (пример)");
            Console.WriteLine(new string('*', 40));
        }
    }
}