// Объявляем пространство имен
namespace EducationalProject
{
    // Статический класс для проверки правильности ввода
    public static class InputValidator
    {
        // Метод для получения правильного выбора из меню
        public static int GetValidMenuChoice(int minValue, int maxValue, string prompt)
        {
            // Бесконечный цикл, пока не получим правильный ввод
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);  // Получаем ввод пользователя

                // Проверяем, не пустая ли строка
                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");  // Сообщаем об ошибке
                    continue;  // Переходим к следующей итерации цикла
                }

                // Пытаемся преобразовать строку в число
                if(!int.TryParse(input, out int number))  // Если не получилось
                {
                    ConsoleHelper.ShowError($"'{input}' - это не число!");  // Сообщаем об ошибке
                    continue;  // Продолжаем цикл
                }

                // Проверяем, попадает ли число в нужный диапазон
                if(number < minValue || number > maxValue)
                {
                    // Сообщаем об ошибке с указанием допустимых значений
                    ConsoleHelper.ShowError($"Нет пункта № {number}! Доступны номера от {minValue} до {maxValue}.");
                    continue;  // Продолжаем цикл
                }

                return number;  // Возвращаем правильное число
            }
        }

        // Метод для получения правильного числа (дробного)
        public static double GetValidNumber(string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);  // Получаем ввод

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели!");
                    continue;
                }

                // Пытаемся преобразовать в дробное число
                if(!double.TryParse(input, out double number))
                {
                    ConsoleHelper.ShowError($"'{input}' не является допустимым числом!");
                    continue;
                }

                return number;  // Возвращаем число
            }
        }

        // Метод для получения целого числа в заданном диапазоне
        public static int GetValidIntegerInRange(int minValue, int maxValue, string prompt)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);

                if(string.IsNullOrWhiteSpace(input))
                {
                    ConsoleHelper.ShowError("Вы ничего не ввели! Пожалуйста, введите число!");
                    continue;
                }

                // Пытаемся преобразовать в целое число
                if(!int.TryParse(input, out int number))
                {
                    ConsoleHelper.ShowError($"'{input}' не является целым числом!");
                    continue;
                }

                // Проверяем диапазон
                if(number < minValue || number > maxValue)
                {
                    ConsoleHelper.ShowError($"Число должно быть от {minValue} до {maxValue}!");
                    continue;
                }

                return number;
            }
        }

        // Метод для получения правильного текста
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

                string trimmedInput = input.Trim();  // Удаляем пробелы в начале и конце

                // Проверяем минимальную длину
                if(trimmedInput.Length < minLength)
                {
                    ConsoleHelper.ShowError($"Текст должен содержать не менее {minLength} символов!");
                    continue;
                }

                // Проверяем максимальную длину
                if(trimmedInput.Length > maxLength)
                {
                    ConsoleHelper.ShowError($"Текст должен содержать не более {maxLength} символов!");
                    continue;
                }

                return trimmedInput;  // Возвращаем очищенный текст
            }
        }

        // Метод для получения правильного двоичного числа
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

                string cleanInput = input.Replace(" ", "");  // Удаляем все пробелы

                // Проверяем длину
                if(cleanInput.Length > maxBits)
                {
                    ConsoleHelper.ShowError($"Слишком длинное число! Максимум {maxBits} бит.");
                    continue;
                }

                // Проверяем каждый символ - должен быть 0 или 1
                foreach(char c in cleanInput)  // Для каждого символа в строке
                {
                    if(c != '0' && c != '1')  // Если символ не 0 и не 1
                    {
                        ConsoleHelper.ShowError($"'{input}' содержит недопустимые символы! Используйте только 0 и 1.");
                        continue;  // Переходим к следующей итерации внешнего цикла
                    }
                }

                return cleanInput;  // Возвращаем чистое двоичное число
            }
        }

        // Метод для получения правильной математической операции
        public static char GetValidMathOperation(string prompt = "Введите операцию (+, -, *, /, %): ")
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

        // Метод для преобразования двоичного числа в десятичное
        public static int BinaryToDecimal(string binaryString)
        {
            try
            {
                // Convert.ToInt32 с параметром 2 означает "из двоичной системы"
                return Convert.ToInt32(binaryString, 2);
            }
            catch  // Если что-то пошло не так (например, слишком большое число)
            {
                return -1;  // Возвращаем -1 как признак ошибки
            }
        }

        // Метод для преобразования десятичного числа в двоичное
        public static string DecimalToBinary(int decimalNumber, int bitsCount = 8)
        {
            try
            {
                // Convert.ToString с параметром 2 означает "в двоичную систему"
                string binaryString = Convert.ToString(decimalNumber, 2);
                // Добавляем нули слева, чтобы было нужное количество бит
                return binaryString.PadLeft(bitsCount, '0');
            }
            catch
            {
                return "Ошибка";  // Возвращаем строку с ошибкой
            }
        }
    }
}