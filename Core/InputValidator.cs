namespace EducationalProject.Core
{
    public static class InputValidator
    {
        // Проверка выбора в меню
        public static int GetValidMenuChoice(int minValue, int maxValue, string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    ConsoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                if(number < minValue || number > maxValue)
                {
                    ConsoleHelper.ShowError($"Неверный выбор {number}! Допустимо от {minValue} до {maxValue}.");
                    continue;
                }

                return number;
            }
        }

        // Проверка числа
        public static double GetValidNumber(string prompt = "Введите число: ")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!double.TryParse(input, out double number))
                {
                    ConsoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                return number;
            }
        }

        // Проверка целого числа в диапазоне
        public static int GetValidIntegerInRange(int minValue, int maxValue, string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    ConsoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                if(number < minValue || number > maxValue)
                {
                    ConsoleHelper.ShowError($"Число должно быть от {minValue} до {maxValue}!");
                    continue;
                }

                return number;
            }
        }

        // Проверка текста
        public static string GetValidText(string prompt, int minLength = 1, int maxLength = 100)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                string trimmedInput = input.Trim();

                if(string.IsNullOrWhiteSpace(trimmedInput))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите текст!");
                    continue;
                }

                if(trimmedInput.Length < minLength)
                {
                    ConsoleHelper.ShowError($"Текст должен быть не менее {minLength} символов!");
                    continue;
                }

                if(trimmedInput.Length > maxLength)
                {
                    ConsoleHelper.ShowError($"Текст должен быть не более {maxLength} символов!");
                    continue;
                }

                return trimmedInput;
            }
        }

        // Проверка математической операции
        public static char GetValidMathOperation(string prompt = "Выберите операцию (+, -, *, /, %): ")
        {
            char[] validOperations = { '+', '-', '*', '/', '%' };

            while(true)
            {
                Console.Write(prompt);
                char operationChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if(!validOperations.Contains(operationChar))
                {
                    ConsoleHelper.ShowError($"Недопустимая операция '{operationChar}'! Допустимо: +, -, *, /, %");
                    continue;
                }

                return operationChar;
            }
        }

        // Преобразование двоичного в десятичное
        public static int BinaryToDecimal(string binaryString)
        {
            try
            {
                return Convert.ToInt32(binaryString, 2);
            }
            catch
            {
                return -1;
            }
        }

        // Преобразование десятичного в двоичное
        public static string DecimalToBinary(int decimalNumber, int bitsCount = 8)
        {
            try
            {
                string binaryString = Convert.ToString(decimalNumber, 2);
                return binaryString.PadLeft(bitsCount, '0');
            }
            catch
            {
                return "";
            }
        }
    }
}