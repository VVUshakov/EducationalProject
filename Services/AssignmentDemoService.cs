using EducationalProject.Models;

namespace EducationalProject.Services
{
    public class AssignmentDemoService
    {
        public AssignmentData GetBasicOperations()
        {
            var data = new AssignmentData { Value = 100 };

            // Начальное значение
            data.Operations.Add(new OperationInfo
            {
                Description = "Начальное значение",
                Example = "value = 100",
                Result = "100"
            });

            // Операции
            int value = 100;

            var operations = new List<(string op, string desc, Func<int, int> func)>
            {
                ("+=", "Добавить 10", v => v + 10),
                ("-=", "Вычесть 5", v => v - 5),
                ("*=", "Умножить на 2", v => v * 2),
                ("/=", "Разделить на 3 (целочисленно)", v => v / 3),
                ("%=", "Остаток от деления на 7", v => v % 7),
                ("<<=", "Сдвиг влево на 1 (умножить на 2)", v => v << 1),
                (">>=", "Сдвиг вправо на 1 (разделить на 2)", v => v >> 1)
            };

            foreach(var op in operations)
            {
                value = op.func(value);
                data.Operations.Add(new OperationInfo
                {
                    Description = op.desc,
                    Example = $"value {op.op} ...",
                    Result = value.ToString()
                });
            }

            data.FinalValue = value;
            return data;
        }

        public AssignmentData GetCombinedOperations()
        {
            var data = new AssignmentData();

            int a = 10, b = 20, c = 30;

            // Начальные значения
            data.Operations.Add(new OperationInfo
            {
                Description = "Начальные значения",
                Example = "a = 10, b = 20, c = 30",
                Result = $"a={a}, b={b}, c={c}"
            });

            // 1. c = c / 2
            c /= 2;
            data.Operations.Add(new OperationInfo
            {
                Description = "Делим c на 2",
                Example = "c /= 2",
                Result = $"c = 15\nТеперь: a={a}, b={b}, c={c}"
            });

            // 2. b = b - c
            b -= c;
            data.Operations.Add(new OperationInfo
            {
                Description = "Вычитаем c из b",
                Example = "b -= c",
                Result = $"b = 5 (20 - 15)\nТеперь: a={a}, b={b}, c={c}"
            });

            // 3. a = a + b
            a += b;
            data.Operations.Add(new OperationInfo
            {
                Description = "Добавляем b к a",
                Example = "a += b",
                Result = $"a = 15 (10 + 5)\nТеперь: a={a}, b={b}, c={c}"
            });

            data.FinalValue = a;
            return data;
        }

        public PracticalExample GetPracticalExample()
        {
            var example = new PracticalExample
            {
                Salary = 50000,
                Groceries = 15000,
                Entertainment = 5000,
                Currency = "руб."
            };

            double budget = 0;

            // Начальный бюджет
            example.Steps.Add($"Начальный бюджет: {budget:F2} {example.Currency}");

            // 1. Получение зарплаты
            budget += example.Salary;
            example.Steps.Add($"+ Зарплата: {example.Salary,8:F2} {example.Currency}");
            example.Steps.Add($"  Бюджет: {budget,8:F2} {example.Currency}");

            // 2. Покупка продуктов
            budget -= example.Groceries;
            example.Steps.Add($"- Продукты: {example.Groceries,8:F2} {example.Currency}");
            example.Steps.Add($"  Бюджет: {budget,8:F2} {example.Currency}");

            // 3. Развлечения
            budget -= example.Entertainment;
            example.Steps.Add($"- Развлечения: {example.Entertainment,8:F2} {example.Currency}");
            example.Steps.Add($"  Бюджет: {budget,8:F2} {example.Currency}");

            // 4. Премия (+10%)
            budget *= 1.1;
            example.Steps.Add($"* Премия (+10%): {budget:F2} {example.Currency}");

            // 5. Разделить на 2 (например, на семью)
            budget /= 2;
            example.Steps.Add($"/ Разделить на 2 (на семью): {budget:F2} {example.Currency}");

            example.FinalBudget = budget;
            return example;
        }

        public string GetAssignmentDescription()
        {
            return "Операции присваивания в C#:\n" +
                   "• += : добавить и присвоить\n" +
                   "• -= : вычесть и присвоить\n" +
                   "• *= : умножить и присвоить\n" +
                   "• /= : разделить и присвоить\n" +
                   "• %= : остаток от деления и присвоить\n" +
                   "• <<= : сдвиг влево и присвоить\n" +
                   "• >>= : сдвиг вправо и присвоить";
        }
    }
}