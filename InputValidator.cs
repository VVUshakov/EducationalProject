namespace EducationalProject
{
    public static class InputValidator
    {
        public static int GetValidMenuChoice(int minValue, int maxValue, string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    ConsoleHelper.ShowError($"'{input}' - это не число!");
                    continue;
                }

                if(number < minValue || number > maxValue)
                {
                    ConsoleHelper.ShowError($"Нет пункта № {number}! Доступны номера от {minValue} до {maxValue}.");
                    continue;
                }

                return number;
            }
        }

        public static double GetValidNumber(string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                if(!double.TryParse(input, out double number))
                {
                    ConsoleHelper.ShowError($"'{input}' не является допустимым числом!");
                    continue;
                }

                return number;
            }
        }

        public static int GetValidIntegerInRange(string prompt, int minValue, int maxValue)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    ConsoleHelper.ShowError($"'{input}' не является целым числом!");
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

        public static string GetValidText(string prompt, int minLength = 1, int maxLength = 100)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                string trimmedInput = input.Trim();

                if(trimmedInput.Length < minLength)
                {
                    ConsoleHelper.ShowError($"Текст должен содержать не менее {minLength} символов!");
                    continue;
                }

                if(trimmedInput.Length > maxLength)
                {
                    ConsoleHelper.ShowError($"Текст должен содержать не более {maxLength} символов!");
                    continue;
                }

                return trimmedInput;
            }
        }

        public static string GetValidBinary(string prompt, int maxBits = 8)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                string cleanInput = input.Replace(" ", "");

                if(cleanInput.Length > maxBits)
                {
                    ConsoleHelper.ShowError($"Слишком длинное число! Максимум {maxBits} бит.");
                    continue;
                }

                foreach(char c in cleanInput)
                {
                    if(c != '0' && c != '1')
                    {
                        ConsoleHelper.ShowError($"'{input}' содержит недопустимые символы! Используйте только 0 и 1.");
                        continue;
                    }
                }

                return cleanInput;
            }
        }

        public static char GetValidMathOperation(string prompt)
        {
            char[] validOperations = { '+', '-', '*', '/', '%' };

            while(true)
            {
                Console.Write(prompt);
                char operationChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if(!validOperations.Contains(operationChar))
                {
                    ConsoleHelper.ShowError($"Операция '{operationChar}' не поддерживается! Используйте: +, -, *, /, %");
                    continue;
                }

                return operationChar;
            }
        }

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

        public static string DecimalToBinary(int decimalNumber, int bitsCount = 8)
        {
            try
            {
                string binaryString = Convert.ToString(decimalNumber, 2);
                return binaryString.PadLeft(bitsCount, '0');
            }
            catch
            {
                return "Ошибка";
            }
        }
    }
}