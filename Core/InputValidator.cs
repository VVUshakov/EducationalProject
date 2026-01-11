namespace EducationalProject.Core
{
    public static class InputValidator
    {
        // Проверка выбора в меню
        public static int GetValidMenuChoice(
            int minValue,
            int maxValue,
            string prompt,
            IConsoleHelper consoleHelper = null)
        {
            consoleHelper ??= new ConsoleHelper(); // Используем экземпляр по умолчанию

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    consoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                if(number < minValue || number > maxValue)
                {
                    consoleHelper.ShowError($"Неверный выбор {number}! Допустимо от {minValue} до {maxValue}.");
                    continue;
                }

                return number;
            }
        }

        // Проверка числа
        public static double GetValidNumber(string prompt = "Введите число: ")
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!double.TryParse(input, out double number))
                {
                    consoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                return number;
            }
        }

        // Проверка целого числа в диапазоне
        public static int GetValidIntegerInRange(int minValue, int maxValue, string prompt)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(!int.TryParse(input, out int number))
                {
                    consoleHelper.ShowError($"'{input}' - не число!");
                    continue;
                }

                if(number < minValue || number > maxValue)
                {
                    consoleHelper.ShowError($"Число должно быть от {minValue} до {maxValue}!");
                    continue;
                }

                return number;
            }
        }

        // Проверка текста
        public static string GetValidText(string prompt, int minLength = 1, int maxLength = 100)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);
                string trimmedInput = input.Trim();

                if(string.IsNullOrWhiteSpace(trimmedInput))
                {
                    consoleHelper.ShowError("Пожалуйста, введите текст!");
                    continue;
                }

                if(trimmedInput.Length < minLength)
                {
                    consoleHelper.ShowError($"Текст должен быть не менее {minLength} символов!");
                    continue;
                }

                if(trimmedInput.Length > maxLength)
                {
                    consoleHelper.ShowError($"Текст должен быть не более {maxLength} символов!");
                    continue;
                }

                return trimmedInput;
            }
        }

        // Проверка математической операции
        public static char GetValidMathOperation(string prompt = "Выберите операцию (+, -, *, /, %): ")
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            char[] validOperations = { '+', '-', '*', '/', '%' };

            while(true)
            {
                Console.Write(prompt);
                char operationChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if(!validOperations.Contains(operationChar))
                {
                    consoleHelper.ShowError($"Недопустимая операция '{operationChar}'! Допустимо: +, -, *, /, %");
                    continue;
                }

                return operationChar;
            }
        }

        // Проверка двоичного числа
        public static string GetValidBinary(string prompt = "Введите двоичное число: ", int maxBits = 8)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                string cleanInput = input.Replace(" ", ""); // Удаляем пробелы

                if(cleanInput.Length > maxBits)
                {
                    consoleHelper.ShowError($"Слишком длинное число! Максимум {maxBits} бит.");
                    continue;
                }

                bool isValid = true;
                foreach(char c in cleanInput)
                {
                    if(c != '0' && c != '1')
                    {
                        consoleHelper.ShowError($"Двоичное число должно содержать только 0 и 1!");
                        isValid = false;
                        break;
                    }
                }

                if(!isValid)
                    continue;

                return cleanInput;
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
                return -1; // Ошибка преобразования
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

        // Проверка пароля на соответствие требованиям
        public static bool IsPasswordValid(string password, int minLength = 8)
        {
            if(string.IsNullOrEmpty(password) || password.Length < minLength)
                return false;

            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach(char c in password)
            {
                if(char.IsLower(c)) hasLower = true;
                else if(char.IsUpper(c)) hasUpper = true;
                else if(char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true;

                if(hasLower && hasUpper && hasDigit && hasSpecial)
                    break;
            }

            return hasLower && hasUpper && hasDigit;
        }

        // Проверка email
        public static bool IsValidEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Проверка возраста
        public static int GetValidAge(string prompt = "Введите возраст: ", int minAge = 0, int maxAge = 150)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(!int.TryParse(input, out int age))
                {
                    consoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(age < minAge || age > maxAge)
                {
                    consoleHelper.ShowError($"Возраст должен быть от {minAge} до {maxAge} лет!");
                    continue;
                }

                return age;
            }
        }

        // Проверка даты
        public static DateTime GetValidDate(string prompt = "Введите дату (дд.мм.гггг): ")
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt);

                if(DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }

                consoleHelper.ShowError("Неверный формат даты! Используйте дд.мм.гггг");
            }
        }

        // Получить ответ Да/Нет
        public static bool GetYesNoAnswer(string prompt = "Выберите (д/н): ")
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                string input = consoleHelper.GetInput(prompt).ToLower();

                if(input == "д" || input == "да" || input == "y" || input == "yes")
                    return true;

                if(input == "н" || input == "нет" || input == "n" || input == "no")
                    return false;

                consoleHelper.ShowError("Пожалуйста, введите 'д' или 'н'!");
            }
        }

        // Проверка диапазона чисел
        public static (double, double) GetValidRange(string prompt, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            IConsoleHelper consoleHelper = new ConsoleHelper(); // TO DO

            while(true)
            {
                Console.WriteLine(prompt);
                Console.WriteLine("Введите два числа через пробел:");

                string input = consoleHelper.GetInput();
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if(parts.Length != 2)
                {
                    consoleHelper.ShowError("Нужно ввести два числа через пробел!");
                    continue;
                }

                if(!double.TryParse(parts[0], out double num1) || !double.TryParse(parts[1], out double num2))
                {
                    consoleHelper.ShowError("Оба значения должны быть числами!");
                    continue;
                }

                if(num1 >= num2)
                {
                    consoleHelper.ShowError("Первое число должно быть меньше второго!");
                    continue;
                }

                if(num1 < minValue || num2 > maxValue)
                {
                    consoleHelper.ShowError($"Числа должны быть в диапазоне от {minValue} до {maxValue}!");
                    continue;
                }

                return (num1, num2);
            }
        }

        // Генерация случайного числа в диапазоне
        public static int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        // Форматирование числа
        public static string FormatNumber(double number, int decimals = 2)
        {
            return number.ToString($"F{decimals}");
        }

        // Очистка строки от лишних пробелов
        public static string CleanString(string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            return string.Join(" ", input.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}