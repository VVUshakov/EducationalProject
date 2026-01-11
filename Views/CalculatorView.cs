using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class CalculatorView
    {
        private readonly IConsoleHelper _consoleHelper;

        public CalculatorView(IConsoleHelper consoleHelper)
        {
            _consoleHelper = consoleHelper;
        }

        // Получить данные от пользователя
        public CalculationData GetInput()
        {
            _consoleHelper.ShowInfoBlock(
                "КАЛЬКУЛЯТОР",
                new string[] {
                    "Поддерживаемые операции:",
                    "- Сложение (+)",
                    "- Вычитание (-)",
                    "- Умножение (*)",
                    "- Деление (/)",
                    "- Остаток (%)"
                }
            );

            var data = new CalculationData();

            Console.WriteLine("\nВведите числа:");
            Console.WriteLine(new string('-', 20));

            data.FirstNumber = InputValidator.GetValidNumber("Первое число: ");
            data.SecondNumber = InputValidator.GetValidNumber("Второе число: ");
            data.Operation = InputValidator.GetValidMathOperation();

            return data;
        }

        // Показать результат
        public void ShowResult(CalculationData data)
        {
            Console.WriteLine("\n" + new string('=', 35));

            if(!data.IsValid)
            {
                _consoleHelper.ShowError("Ошибка вычисления!");
            }
            else
            {
                Console.WriteLine($"ВЫРАЖЕНИЕ: {data.Description}");
                Console.WriteLine($"РЕЗУЛЬТАТ: {data.Result:F2}");

                if(data.Remainder.HasValue)
                {
                    Console.WriteLine($"ОСТАТОК: {data.Remainder:F2}");
                }

                _consoleHelper.ShowSuccess("Вычисление завершено успешно!");
            }

            Console.WriteLine(new string('=', 35));
        }

        // Показать меню операций
        public void ShowOperationsMenu()
        {
            _consoleHelper.ShowMenu(
                "Выберите операцию",
                AppConfig.CalculatorConfig.OperationsNames
            );
        }
    }
}