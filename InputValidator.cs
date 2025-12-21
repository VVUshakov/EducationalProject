namespace EducationalProject
{
    /// <summary>
    /// Класс для валидации пользовательского ввода
    /// Содержит универсальные методы проверки различных типов данных
    /// </summary>
    public static class InputValidator
    {
        #region ===== ОСНОВНЫЕ СТРУКТУРЫ ДАННЫХ =====

        /// <summary>
        /// Результат валидации
        /// </summary>
        public struct ValidationResult
        {
            /// <summary>
            /// Успешна ли валидация
            /// </summary>
            public bool IsValid { get; set; }

            /// <summary>
            /// Сообщение об ошибке (если есть)
            /// </summary>
            public string ErrorMessage { get; set; }

            /// <summary>
            /// Проверенное значение
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// Создать успешный результат
            /// </summary>
            public static ValidationResult Success(object value)
            {
                return new ValidationResult
                {
                    IsValid = true,
                    Value = value,
                    ErrorMessage = string.Empty
                };
            }

            /// <summary>
            /// Создать результат с ошибкой
            /// </summary>
            public static ValidationResult Error(string message)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = message,
                    Value = null
                };
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ МЕНЮ =====

        /// <summary>
        /// Проверить выбор из меню
        /// </summary>
        /// <param name="input">Введенная строка</param>
        /// <param name="minValue">Минимальное допустимое значение</param>
        /// <param name="maxValue">Максимальное допустимое значение</param>
        /// <returns>Результат валидации</returns>
        public static ValidationResult ValidateMenuChoice(string input, int minValue, int maxValue)
        {
            // Проверка на пустой ввод
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            // Проверка на число
            if(!int.TryParse(input, out int number))
            {
                return ValidationResult.Error($"'{input}' - это не число! Пожалуйста, введите номер цифрами.");
            }

            // Проверка диапазона
            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"Нет пункта № {number}! Доступны номера от {minValue} до {maxValue}.");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Получить валидный выбор из меню с повторными попытками
        /// </summary>
        /// <param name="minValue">Минимальное значение</param>
        /// <param name="maxValue">Максимальное значение</param>
        /// <param name="prompt">Приглашение для ввода</param>
        /// <returns>Валидный выбор</returns>
        public static int GetValidMenuChoice(int minValue, int maxValue, string prompt = ">>> Введите номер: ")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateMenuChoice(input, minValue, maxValue);

                if(result.IsValid)
                {
                    return (int)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ЧИСЕЛ =====

        /// <summary>
        /// Проверить число
        /// </summary>
        /// <param name="input">Введенная строка</param>
        /// <param name="allowNegative">Разрешить отрицательные числа</param>
        /// <param name="allowZero">Разрешить ноль</param>
        /// <returns>Результат валидации</returns>
        public static ValidationResult ValidateNumber(string input, bool allowNegative = true, bool allowZero = true)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            if(!double.TryParse(input, out double number))
            {
                return ValidationResult.Error($"'{input}' не является допустимым числом!");
            }

            if(!allowNegative && number < 0)
            {
                return ValidationResult.Error("Отрицательные числа не разрешены!");
            }

            if(!allowZero && Math.Abs(number) < double.Epsilon)
            {
                return ValidationResult.Error("Ноль не разрешен!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Проверить число в диапазоне
        /// </summary>
        public static ValidationResult ValidateNumberInRange(string input, double minValue, double maxValue, string valueName = "Число")
        {
            var basicResult = ValidateNumber(input);
            if(!basicResult.IsValid)
            {
                return basicResult;
            }

            double number = (double)basicResult.Value;

            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"{valueName} должно быть от {minValue} до {maxValue}!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Проверить целое число в диапазоне
        /// </summary>
        public static ValidationResult ValidateIntegerInRange(string input, int minValue, int maxValue, string valueName = "Число")
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            if(!int.TryParse(input, out int number))
            {
                return ValidationResult.Error($"'{input}' не является целым числом!");
            }

            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"{valueName} должно быть от {minValue} до {maxValue}!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Получить валидное число с повторными попытками
        /// </summary>
        public static double GetValidNumber(string prompt, bool allowNegative = true, bool allowZero = true)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateNumber(input, allowNegative, allowZero);

                if(result.IsValid)
                {
                    return (double)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получить валидное число в диапазоне
        /// </summary>
        public static double GetValidNumberInRange(string prompt, double minValue, double maxValue, string valueName = "Число")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateNumberInRange(input, minValue, maxValue, valueName);

                if(result.IsValid)
                {
                    return (double)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получить валидное целое число в диапазоне
        /// </summary>
        public static int GetValidIntegerInRange(string prompt, int minValue, int maxValue, string valueName = "Число")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateIntegerInRange(input, minValue, maxValue, valueName);

                if(result.IsValid)
                {
                    return (int)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ТЕКСТА =====

        /// <summary>
        /// Проверить текстовый ввод
        /// </summary>
        public static ValidationResult ValidateText(string input, int minLength = 1, int maxLength = 100,
                                                    bool allowDigits = true, bool allowSpecialChars = true)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            string trimmedInput = input.Trim();

            if(trimmedInput.Length < minLength)
            {
                return ValidationResult.Error($"Текст должен содержать не менее {minLength} символов!");
            }

            if(trimmedInput.Length > maxLength)
            {
                return ValidationResult.Error($"Текст должен содержать не более {maxLength} символов!");
            }

            // Проверка на наличие хотя бы одной буквы
            bool hasLetters = false;
            foreach(char c in trimmedInput)
            {
                if(char.IsLetter(c))
                {
                    hasLetters = true;
                    break;
                }
            }

            if(!hasLetters)
            {
                return ValidationResult.Error("Текст должен содержать хотя бы одну букву!");
            }

            // Проверка разрешенных символов
            foreach(char c in trimmedInput)
            {
                if(char.IsLetter(c)) continue;
                if(allowDigits && char.IsDigit(c)) continue;
                if(allowSpecialChars && char.IsWhiteSpace(c)) continue;
                if(allowSpecialChars && char.IsPunctuation(c)) continue;

                return ValidationResult.Error($"Символ '{c}' не разрешен!");
            }

            return ValidationResult.Success(trimmedInput);
        }

        /// <summary>
        /// Проверить двоичное число
        /// </summary>
        public static ValidationResult ValidateBinary(string input, int maxBits = 8)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            // Убираем пробелы для проверки
            string cleanInput = input.Replace(" ", "");

            if(cleanInput.Length > maxBits)
            {
                return ValidationResult.Error($"Слишком длинное число! Максимум {maxBits} бит.");
            }

            // Проверка на допустимые символы (только 0 и 1)
            foreach(char c in cleanInput)
            {
                if(c != '0' && c != '1')
                {
                    return ValidationResult.Error($"'{input}' содержит недопустимые символы! Используйте только 0 и 1.");
                }
            }

            return ValidationResult.Success(cleanInput);
        }

        /// <summary>
        /// Получить валидный текст
        /// </summary>
        public static string GetValidText(string prompt, int minLength = 1, int maxLength = 100,
                                         bool allowDigits = true, bool allowSpecialChars = true)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateText(input, minLength, maxLength, allowDigits, allowSpecialChars);

                if(result.IsValid)
                {
                    return (string)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получить валидное двоичное число
        /// </summary>
        public static string GetValidBinary(string prompt, int maxBits = 8)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateBinary(input, maxBits);

                if(result.IsValid)
                {
                    return (string)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ВЫБОРА ОПЕРАЦИЙ =====

        /// <summary>
        /// Проверить математическую операцию
        /// </summary>
        public static ValidationResult ValidateMathOperation(char operationChar)
        {
            char[] validOperations = { '+', '-', '*', '/', '%' };

            if(Array.IndexOf(validOperations, operationChar) == -1)
            {
                return ValidationResult.Error($"Операция '{operationChar}' не поддерживается! Используйте: +, -, *, /, %");
            }

            return ValidationResult.Success(operationChar);
        }

        /// <summary>
        /// Получить валидную математическую операцию
        /// </summary>
        public static char GetValidMathOperation(string prompt = ">>> Выберите операцию (+, -, *, /, %): ")
        {
            while(true)
            {
                Console.Write(prompt);
                char operationChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                var result = ValidateMathOperation(operationChar);

                if(result.IsValid)
                {
                    return (char)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Преобразовать двоичную строку в десятичное число
        /// </summary>
        public static ValidationResult BinaryToDecimal(string binaryString)
        {
            try
            {
                int decimalNumber = Convert.ToInt32(binaryString, 2);
                return ValidationResult.Success(decimalNumber);
            }
            catch(FormatException)
            {
                return ValidationResult.Error("Неверный формат двоичного числа!");
            }
            catch(OverflowException)
            {
                return ValidationResult.Error("Число слишком большое!");
            }
            catch(Exception ex)
            {
                return ValidationResult.Error($"Ошибка при конвертации: {ex.Message}");
            }
        }

        /// <summary>
        /// Преобразовать десятичное число в двоичную строку
        /// </summary>
        public static ValidationResult DecimalToBinary(int decimalNumber, int bitsCount = 8)
        {
            try
            {
                string binaryString = Convert.ToString(decimalNumber, 2);
                string paddedBinary = binaryString.PadLeft(bitsCount, '0');
                return ValidationResult.Success(paddedBinary);
            }
            catch(Exception ex)
            {
                return ValidationResult.Error($"Ошибка при конвертации: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверить деление на ноль
        /// </summary>
        public static ValidationResult ValidateDivision(double divisor, string operationName = "деление")
        {
            if(Math.Abs(divisor) < double.Epsilon)
            {
                return ValidationResult.Error($"{operationName.FirstCharToUpper()} на ноль невозможно!");
            }

            return ValidationResult.Success(true);
        }

        #endregion
    }

    /// <summary>
    /// Методы расширения для работы со строками
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Сделать первую букву строки заглавной
        /// </summary>
        public static string FirstCharToUpper(this string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}