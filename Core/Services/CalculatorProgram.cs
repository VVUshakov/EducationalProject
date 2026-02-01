using EducationalProject.Core.Views;

namespace EducationalProject.Core.Services
{
    public class CalculatorProgram : BaseProgram
    {
        public override string Name => "Калькулятор";
        public override string Description => "Простой калькулятор для выполнения базовых операций";

        public CalculatorProgram() : base(new ConsoleView()) { }

        public override void Run()
        {
            ShowHeader();

            double num1 = GetNumber("Введите первое число: ");
            double num2 = GetNumber("Введите второе число: ");

            _view.ShowMessage("\nВыберите операцию:");
            _view.ShowMessage("1. Сложение (+)");
            _view.ShowMessage("2. Вычитание (-)");
            _view.ShowMessage("3. Умножение (*)");
            _view.ShowMessage("4. Деление (/)");

            string operation = _view.ReadInput("Ваш выбор (1-4): ");

            try
            {
                double result = Calculate(num1, num2, operation);
                _view.ShowMessage($"\nРезультат: {result}");
            }
            catch(Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private double GetNumber(string prompt)
        {
            while(true)
            {
                string input = _view.ReadInput(prompt);
                if(double.TryParse(input, out double number))
                    return number;

                _view.ShowError("Введите корректное число!");
            }
        }

        private double Calculate(double a, double b, string operation)
        {
            return operation switch
            {
                "1" => a + b,
                "2" => a - b,
                "3" => a * b,
                "4" => b != 0 ? a / b : throw new DivideByZeroException("Деление на ноль!"),
                _ => throw new ArgumentException("Неверная операция")
            };
        }
    }
}