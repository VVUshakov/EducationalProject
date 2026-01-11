using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class AssignmentDemoView
    {
        public int GetDemoChoice()
        {
            ConsoleHelper.ShowMenu(
                "Демонстрация операций присваивания",
                new string[] {
                    "Базовые операции (+=, -=, *=, /=, %=, <<=, >>=)",
                    "Комбинированные операции (пример с тремя переменными)",
                    "Практический пример (управление бюджетом)"
                }
            );

            return InputValidator.GetValidMenuChoice(1, 3, ">>> Выберите демонстрацию (1-3): ");
        }

        public void ShowBasicOperations(AssignmentData data)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("БАЗОВЫЕ ОПЕРАЦИИ ПРИСВАИВАНИЯ");
            Console.WriteLine(new string('=', 60));

            Console.WriteLine("\nНачинаем со значения 100:");

            foreach(var op in data.Operations)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"Операция: {op.Description}");
                Console.WriteLine($"Пример: {op.Example}");
                Console.WriteLine($"Результат: {op.Result}");
            }

            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"\nИтоговое значение после всех операций: {data.FinalValue}");
            Console.WriteLine(new string('=', 60));
        }

        public void ShowCombinedOperations(AssignmentData data)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("КОМБИНИРОВАННЫЕ ОПЕРАЦИИ");
            Console.WriteLine(new string('=', 60));

            Console.WriteLine("\nРаботаем с тремя переменными: a, b, c");

            foreach(var op in data.Operations)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(op.Result);
            }

            Console.WriteLine(new string('=', 60));
            Console.WriteLine($"\nИтоговые значения: {data.Operations[^1].Result}");
            Console.WriteLine(new string('=', 60));
        }

        public void ShowPracticalExample(PracticalExample example)
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ПРАКТИЧЕСКИЙ ПРИМЕР: УПРАВЛЕНИЕ БЮДЖЕТОМ");
            Console.WriteLine(new string('=', 60));

            Console.WriteLine("\nИсходные данные:");
            Console.WriteLine($"• Зарплата: {example.Salary:F2} {example.Currency}");
            Console.WriteLine($"• Продукты: {example.Groceries:F2} {example.Currency}");
            Console.WriteLine($"• Развлечения: {example.Entertainment:F2} {example.Currency}");

            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("Шаги управления бюджетом:");
            Console.WriteLine(new string('-', 50));

            foreach(var step in example.Steps)
            {
                Console.WriteLine(step);
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nИтоговый бюджет: {example.FinalBudget:F2} {example.Currency}");
            Console.WriteLine(new string('=', 60));
        }

        public void ShowAssignmentInfo()
        {
            Console.WriteLine("\n" + new string('*', 50));
            Console.WriteLine("ИНФОРМАЦИЯ ОБ ОПЕРАЦИЯХ ПРИСВАИВАНИЯ");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Операции присваивания в C# позволяют:");
            Console.WriteLine("1. Изменить значение переменной");
            Console.WriteLine("2. Выполнить операцию с текущим значением");
            Console.WriteLine("3. Сохранить результат в ту же переменную");
            Console.WriteLine("\nПримеры:");
            Console.WriteLine("• x += 5  →  x = x + 5");
            Console.WriteLine("• y *= 2  →  y = y * 2");
            Console.WriteLine("• z /= 3  →  z = z / 3");
            Console.WriteLine(new string('*', 50));
        }

        public void ShowExamplesTable()
        {
            Console.WriteLine("\n" + new string('-', 50));
            Console.WriteLine("ТАБЛИЦА ОПЕРАЦИЙ ПРИСВАИВАНИЯ:");
            Console.WriteLine(new string('-', 50));

            var examples = new[]
            {
                ("+=", "a += 10", "Добавить 10 к a"),
                ("-=", "b -= 5", "Вычесть 5 из b"),
                ("*=", "c *= 2", "Умножить c на 2"),
                ("/=", "d /= 3", "Разделить d на 3"),
                ("%=", "e %= 7", "Остаток от деления e на 7"),
                ("<<=", "f <<= 1", "Сдвиг f влево на 1 бит"),
                (">>=", "g >>= 1", "Сдвиг g вправо на 1 бит")
            };

            Console.WriteLine("| Операция | Пример     | Описание          |");
            Console.WriteLine("|----------|------------|-------------------|");

            foreach(var example in examples)
            {
                Console.WriteLine($"| {example.Item1,-8} | {example.Item2,-10} | {example.Item3,-17} |");
            }

            Console.WriteLine(new string('-', 50));
        }
    }
}