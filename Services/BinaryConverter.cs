// Объявляем пространство имен для сервисов
namespace EducationalProject.Services
{
    // Класс Конвертер систем счисления, наследуется от BaseService
    public class BinaryConverter : BaseService
    {
        // Константы для ограничений
        private const int MAX_VALUE = 255;     // Максимальное значение (8 бит)
        private const int MIN_VALUE = 0;       // Минимальное значение
        private const int BITS_COUNT = 8;      // Количество бит для вывода

        // Реализуем свойство Name
        public override string Name => "Конвертер систем счисления";

        // Реализуем метод Run() - основная логика программы
        public override void Run()
        {
            // Очищаем экран и показываем заголовок
            ConsoleHelper.ClearAndShowHeader(Name);

            // Массив строк с описанием программы
            string[] infoLines = {
                "Программа демонстрирует преобразование чисел",
                "между десятичной и двоичной системами счисления.",
                $"Диапазон: {MIN_VALUE} - {MAX_VALUE} (8-битные числа)"
            };

            // Показываем информационный блок
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Показываем варианты конвертации
            ShowConversionOptions();

            // Получаем выбор пользователя (1 или 2)
            int choice = InputValidator.GetValidMenuChoice(1, 2, ">>> Выберите тип конвертации (1 или 2): ");

            // В зависимости от выбора получаем разный ввод
            string input = choice == 1  // Если выбрали 1 (десятичное → двоичное)
                ? InputValidator.GetValidIntegerInRange(  // Получаем число в диапазоне
                    $">>> Введите десятичное число (от {MIN_VALUE} до {MAX_VALUE}): ",
                    MIN_VALUE, MAX_VALUE).ToString()      // Преобразуем в строку
                : InputValidator.GetValidBinary(          // Иначе (двоичное → десятичное)
                    $">>> Введите двоичное число (до {BITS_COUNT} бит): ");

            // Выполняем конвертацию
            string result = PerformConversion(choice, input);

            // Показываем результат
            Console.WriteLine($"\nВходные данные: {input}");
            Console.WriteLine($"Результат: {result}");

            // Показываем дополнительную информацию
            ShowAdditionalInfo();

            // Ждем нажатия клавиши
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        // Метод для показа вариантов конвертации
        private void ShowConversionOptions()
        {
            // Массив вариантов
            string[] options = {
                "Десятичное → Двоичное (0-255)",
                "Двоичное → Десятичное (8 бит)"
            };

            // Показываем меню
            ConsoleHelper.ShowMenu("ВАРИАНТЫ КОНВЕРТАЦИИ", options);

            // Пустая строка
            Console.WriteLine();
        }

        // Метод для выполнения конвертации
        private string PerformConversion(int choice, string input)
        {
            // Если выбрали десятичное → двоичное
            if(choice == 1)
            {
                // Пытаемся преобразовать строку в число
                if(int.TryParse(input, out int number))  // Если получилось
                {
                    // Конвертируем в двоичное
                    var binaryResult = InputValidator.DecimalToBinary(number, BITS_COUNT);

                    // Если не ошибка
                    if(binaryResult != "Ошибка")
                    {
                        // Форматируем: первые 4 бита, пробел, остальные 4 бита, и в скобках полная запись
                        return $"{binaryResult.Substring(0, 4)} {binaryResult.Substring(4)} ({binaryResult}₂)";
                    }
                }
                return "Ошибка конвертации";  // Если что-то пошло не так
            }
            else  // Если выбрали двоичное → десятичное
            {
                // Конвертируем двоичное в десятичное
                var decimalResult = InputValidator.BinaryToDecimal(input);

                // Если результат не -1 (не ошибка)
                if(decimalResult >= 0)
                {
                    // Возвращаем в формате "число (число₁₀)"
                    return $"{decimalResult} ({decimalResult}₁₀)";
                }
                return "Ошибка конвертации";  // Если ошибка
            }
        }

        // Метод для показа дополнительной информации
        private void ShowAdditionalInfo()
        {
            // Массив строк с информацией
            string[] infoLines = {
                $"• Диапазон чисел: {MIN_VALUE} - {MAX_VALUE} (8-битное число)",
                $"• Двоичное представление: {BITS_COUNT} бит",
                $"• Максимальное значение: {MAX_VALUE} = 11111111₂",
                $"• Системы счисления:",
                $"  - Десятичная: основание 10 (0-9)",
                $"  - Двоичная: основание 2 (0-1)"
            };

            // Показываем информационный блок
            ConsoleHelper.ShowInfoBlock("ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ", infoLines);
        }
    }
}