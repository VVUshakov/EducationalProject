namespace EducationalProject.Services
{
    public class Calculator : BaseService
    {
        public override string Name => "Калькулятор";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа выполняет базовые арифметические операции",
                "с двумя числами: сложение, вычитание, умножение,",
                "деление и нахождение остатка от деления."
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            double firstNumber = GetNumber("первое");
            if(double.IsNaN(firstNumber)) return;

            double secondNumber = GetNumber("второе");
            if(double.IsNaN(secondNumber)) return;

            char operation = GetOperation();

            double result = Calculate(firstNumber, secondNumber, operation);

            if(double.IsNaN(result))
            {
                ConsoleHelper.ShowError("Ошибка при вычислении!");
            }
            else
            {
                Console.WriteLine($"\nРезультат: {firstNumber} {operation} {secondNumber} = {result:F2}");

                if(operation == '/' && secondNumber != 0)
                {
                    double remainder = firstNumber % secondNumber;
                    if(Math.Abs(remainder) > 0.001)
                    {
                        Console.WriteLine($"Остаток от деления: {remainder:F2}");
                    }
                }
            }

            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        private double GetNumber(string numberName)
        {
            return InputValidator.GetValidNumber($">>> Введите {numberName} число: ");
        }

        private char GetOperation()
        {
            string[] operations = {
                "+ : Сложение",
                "- : Вычитание",
                "* : Умножение",
                "/ : Деление",
                "% : Остаток от деления"
            };

            ConsoleHelper.ShowMenu("ДОСТУПНЫЕ ОПЕРАЦИИ", operations);
            Console.WriteLine();

            return InputValidator.GetValidMathOperation(">>> Выберите операцию (+, -, *, /, %): ");
        }

        private double Calculate(double a, double b, char operation)
        {
            try
            {
                return operation switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => b != 0 ? a / b : double.NaN,
                    '%' => b != 0 ? a % b : double.NaN,
                    _ => double.NaN
                };
            }
            catch
            {
                return double.NaN;
            }
        }
    }
}