namespace EducationalProject.Services
{
    public class AssignmentDemo : BaseService
    {
        public override string Name => "Демонстрация операций присваивания";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);
            ShowMenu();

            int choice = InputValidator.GetValidMenuChoice(1, 3, ">>> Введите номер демонстрации (1-3): ");
            ExecuteChoice(choice);

            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        private void ShowMenu()
        {
            string[] menuItems = {
                "Базовые операции присваивания",
                "Комбинированные операции",
                "Практический пример (бюджет)"
            };

            ConsoleHelper.ShowMenu("ДОСТУПНЫЕ ДЕМОНСТРАЦИИ", menuItems);
            Console.WriteLine();
        }

        private void ExecuteChoice(int choice)
        {
            switch(choice)
            {
                case 1:
                    ShowBasicOperations();
                    break;
                case 2:
                    ShowCombinedOperations();
                    break;
                case 3:
                    ShowPracticalExample();
                    break;
                default:
                    ConsoleHelper.ShowError("Неверный выбор. Показываем базовые операции...");
                    ShowBasicOperations();
                    break;
            }
        }

        private void ShowBasicOperations()
        {
            ConsoleHelper.ShowHeader("БАЗОВЫЕ ОПЕРАЦИИ ПРИСВАИВАНИЯ");

            string[] infoLines = {
                "Демонстрация составных операторов: +=, -=, *=, /=, %=",
                "А также битовых сдвигов: <<=, >>="
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            int value = 100;
            ConsoleHelper.ShowInfo($"Начальное значение: {value}");

            var operations = new List<(string operation, string description, int result)>
            {
                ("value += 10", "Увеличили на 10", value += 10),
                ("value -= 5",  "Уменьшили на 5", value -= 5),
                ("value *= 2",  "Умножили на 2", value *= 2),
                ("value /= 3",  "Разделили на 3 (целочисленное деление)", value /= 3),
                ("value %= 7",  "Взяли остаток от деления на 7", value %= 7),
                ("value <<= 1", "Сдвиг влево (умножение на 2)", value <<= 1),
                ("value >>= 1", "Сдвиг вправо (деление на 2)", value >>= 1)
            };

            foreach(var op in operations)
            {
                Console.WriteLine($"После '{op.operation}': {op.result,3}  // {op.description}");
            }

            Console.WriteLine(new string('═', 35));
            ConsoleHelper.ShowSuccess($"Итоговое значение: {value}");
        }

        private void ShowCombinedOperations()
        {
            ConsoleHelper.ShowHeader("КОМБИНИРОВАННЫЕ ОПЕРАЦИИ");

            string[] infoLines = {
                "Цепочка операций с несколькими переменными",
                "Показывает взаимодействие переменных через операции"
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            int a = 10, b = 20, c = 30;
            ConsoleHelper.ShowInfo($"Начальные значения: a = {a}, b = {b}, c = {c}");

            Console.WriteLine("\nВыполняем цепочку операций:\n");

            c /= 2;
            Console.WriteLine($"c /= 2    → c = 15\n   Текущие: a={a}, b={b}, c={c}");

            b -= c;
            Console.WriteLine($"b -= c    → b = 5  (20 - 15)\n   Текущие: a={a}, b={b}, c={c}");

            a += b;
            Console.WriteLine($"a += b    → a = 15 (10 + 5)\n   Текущие: a={a}, b={b}, c={c}");

            Console.WriteLine(new string('═', 35));
            ConsoleHelper.ShowSuccess($"Итоговые значения: a = {a}, b = {b}, c = {c}");
        }

        private void ShowPracticalExample()
        {
            const double salaryReceived = 50000;
            const double spendingOnGroceries = 15000;
            const double entertainmentExpenses = 5000;
            const string currency = "руб.";

            ConsoleHelper.ShowHeader("ПРАКТИЧЕСКИЙ ПРИМЕР");

            string[] infoLines = {
                "Управление личным бюджетом",
                "Симуляция финансовых операций\nс использованием операторов присваивания"
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            double budget = 0;
            ConsoleHelper.ShowInfo($"💰 Начальный бюджет: {budget:F2} {currency}");

            Console.WriteLine("\nФинансовые операции:\n");

            budget += salaryReceived;
            Console.WriteLine($"+ Получена зарплата: {salaryReceived,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            budget -= spendingOnGroceries;
            Console.WriteLine($"- Покупка продуктов: {spendingOnGroceries,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            budget -= entertainmentExpenses;
            Console.WriteLine($"- Развлечения: {entertainmentExpenses,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            Console.WriteLine("\nИнвестиционные операции:\n");

            budget *= 1.1;
            Console.WriteLine($"* Инвестиционный доход (+10%): {budget:F2} {currency}");

            budget /= 2;
            Console.WriteLine($"/ Поделили с семьёй (пополам): {budget:F2} {currency}");

            Console.WriteLine(new string('═', 35));
            ConsoleHelper.ShowSuccess($"ИТОГОВЫЙ БЮДЖЕТ: 📊 {budget:F2} {currency}");
        }
    }
}