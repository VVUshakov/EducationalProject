// Объявляем пространство имен для сервисов (программ)
namespace EducationalProject.Services
{
    // Класс Калькулятор, наследуется от BaseService
    public class Calculator : BaseService
    {
        // Реализуем свойство Name - возвращаем название программы
        public override string Name => "Калькулятор";  // Стрелочная функция

        // Реализуем метод Run() - основная логика программы
        public override void Run()
        {
            // Очищаем экран и показываем заголовок с названием программы
            ConsoleHelper.ClearAndShowHeader(Name);

            // Массив строк с описанием программы
            string[] infoLines = {
                "Программа выполняет базовые арифметические операции",
                "с двумя числами: сложение, вычитание, умножение,",
                "деление и нахождение остатка от деления."
            };

            // Показываем информационный блок с описанием
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Получаем первое число от пользователя
            double firstNumber = GetNumber("первое");

            // Если пользователь отменил ввод (вернулось NaN), выходим из метода
            if(double.IsNaN(firstNumber)) return;  // return завершает выполнение метода

            // Получаем второе число
            double secondNumber = GetNumber("второе");

            // Если отменили ввод второго числа, тоже выходим
            if(double.IsNaN(secondNumber)) return;

            // Получаем операцию (+, -, *, /, %)
            char operation = GetOperation();

            // Вычисляем результат
            double result = Calculate(firstNumber, secondNumber, operation);

            // Если результат NaN (например, деление на 0), показываем ошибку
            if(double.IsNaN(result))
            {
                ConsoleHelper.ShowError("Ошибка при вычислении!");
            }
            else
            {
                // Иначе показываем результат вычисления
                // :F2 - формат с 2 знаками после запятой
                Console.WriteLine($"\nРезультат: {firstNumber} {operation} {secondNumber} = {result:F2}");

                // Если операция была делением и делитель не ноль
                if(operation == '/' && secondNumber != 0)
                {
                    // Вычисляем остаток от деления
                    double remainder = firstNumber % secondNumber;

                    // Если остаток больше 0.001 (не ноль), показываем его
                    if(Math.Abs(remainder) > 0.001)  // Math.Abs - модуль числа
                    {
                        Console.WriteLine($"Остаток от деления: {remainder:F2}");
                    }
                }
            }

            // Ждем нажатия клавиши для возврата в меню
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        // Вспомогательный метод для получения числа от пользователя
        private double GetNumber(string numberName)
        {
            // Вызываем валидатор, передаем приглашение с названием числа
            return InputValidator.GetValidNumber($">>> Введите {numberName} число: ");
        }

        // Метод для получения операции от пользователя
        private char GetOperation()
        {
            // Массив строк с описанием операций
            string[] operations = {
                "+ : Сложение",
                "- : Вычитание",
                "* : Умножение",
                "/ : Деление",
                "% : Остаток от деления"
            };

            // Показываем меню операций
            ConsoleHelper.ShowMenu("ДОСТУПНЫЕ ОПЕРАЦИИ", operations);

            // Пустая строка для читаемости
            Console.WriteLine();

            // Получаем валидную операцию от пользователя
            return InputValidator.GetValidMathOperation(">>> Выберите операцию (+, -, *, /, %): ");
        }

        // Метод для выполнения вычисления
        private double Calculate(double a, double b, char operation)
        {
            try  // Пытаемся выполнить вычисление
            {
                // Выражение switch - в зависимости от операции выполняем разное
                return operation switch
                {
                    '+' => a + b,                // Сложение
                    '-' => a - b,                // Вычитание
                    '*' => a * b,                // Умножение
                    '/' => b != 0 ? a / b : double.NaN,  // Если b не 0 - делим, иначе NaN
                    '%' => b != 0 ? a % b : double.NaN,  // Если b не 0 - остаток, иначе NaN
                    _ => double.NaN              // Если операция неизвестна - NaN
                };
            }
            catch  // Если произошла ошибка (например, переполнение)
            {
                return double.NaN;  // Возвращаем NaN как признак ошибки
            }
        }
    }
}