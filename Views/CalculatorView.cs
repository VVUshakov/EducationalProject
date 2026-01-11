using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class CalculatorView
    {
        // Получить данные от пользователя
        public CalculationData GetInput()
        {
            ConsoleHelper.ShowInfoBlock(
                "ИНСТРУКЦИЯ",
                new string[] {
                    "Калькулятор выполняет основные операции:",
                    "- Сложение (+)",
                    "- Вычитание (-)",
                    "- Умножение (*)",
                    "- Деление (/)",
                    "- Остаток от деления (%)"
                }
            );

            var data = new CalculationData();

            Console.WriteLine("\nВВОД ДАННЫХ:");
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
                ConsoleHelper.ShowError("Ошибка вычисления!");
            }
            else
            {
                Console.WriteLine($"ВЫРАЖЕНИЕ: {data.Description}");
                Console.WriteLine($"РЕЗУЛЬТАТ: {data.Result:F2}");

                if(data.Remainder.HasValue)
                {
                    Console.WriteLine($"ОСТАТОК: {data.Remainder:F2}");
                }

                ConsoleHelper.ShowSuccess("Вычисление завершено успешно!");
            }

            Console.WriteLine(new string('=', 35));
        }

        // Показать меню операций
        public void ShowOperationsMenu()
        {
            ConsoleHelper.ShowMenu(
                "Выберите операцию",
                AppConfig.CalculatorConfig.OperationsNames
            );
        }
    }
}