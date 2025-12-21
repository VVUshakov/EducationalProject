namespace EducationalProject.Services
{
    /// <summary>
    /// Программа 4: Демонстрация операций присваивания
    /// Демонстрирует различные типы операторов присваивания в языке C#
    /// </summary>
    public class AssignmentDemo : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Название программы для отображения в меню
        /// </summary>
        public override string Name => "Демонстрация операций присваивания";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        // Главный метод запуска программы
        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);
            ShowMenu();

            int choice = ConsoleHelper.GetMenuChoice(1, 3, ">>> Введите номер демонстрации (1-3): ");
            ExecuteChoice(choice);

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        #endregion

        #region ===== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =====

        // Показать меню выбора демонстраций        
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

        /// Выполнить выбранный вариант демонстрации
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
                    ConsoleHelper.ShowWarning("Неверный выбор. Показываем базовые операции...");
                    ShowBasicOperations();
                    break;
            }
        }

        #endregion

        #region ===== ДЕМОНСТРАЦИОННЫЕ МЕТОДЫ =====

        /// Демонстрация базовых операций присваивания
        private void ShowBasicOperations()
        {
            ConsoleHelper.ShowHeader("БАЗОВЫЕ ОПЕРАЦИИ ПРИСВАИВАНИЯ");

            string[] infoLines = {
                "Демонстрация составных операторов: +=, -=, *=, /=, %=",
                "А также битовых сдвигов: <<=, >>="
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            int value = 100;
            ConsoleHelper.ShowKeyValueResult("Начальное значение", value.ToString(), ConsoleColor.Yellow);
            Console.WriteLine();

            // Демонстрация различных операторов присваивания
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
                ConsoleHelper.ShowKeyValueResult($"После '{op.operation}'",
                    $"{op.result,3}  // {op.description}");
            }

            ConsoleHelper.ShowSeparator(newLineAfter: true);
            ConsoleHelper.ShowKeyValueResult("Итоговое значение", value.ToString(), ConsoleColor.Green);
        }

        /// Демонстрация комбинированных операций
        private void ShowCombinedOperations()
        {
            ConsoleHelper.ShowHeader("КОМБИНИРОВАННЫЕ ОПЕРАЦИИ");

            string[] infoLines = {
                "Цепочка операций с несколькими переменными",
                "Показывает взаимодействие переменных через операции"
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            // Инициализация переменных
            int a = 10, b = 20, c = 30;

            ConsoleHelper.ShowKeyValueResult("Начальные значения", $"a = {a}, b = {b}, c = {c}", ConsoleColor.Yellow);
            Console.WriteLine();

            // Цепочка операций присваивания
            var steps = new List<(string operation, string explanation)>
            {
                ("c /= 2", $"c = 30 / 2 = 15\n   Текущие: a={a}, b={b}, c={c/=2}"),
                ("b -= c", $"b = 20 - 15 = 5\n   Текущие: a={a}, b={b-=c}, c={c}"),
                ("a += b", $"a = 10 + 5 = 15\n   Текущие: a={a+=b}, b={b}, c={c}")
            };

            ConsoleHelper.ShowInfo("Выполняем цепочку операций:");
            Console.WriteLine();

            foreach(var step in steps)
            {
                ConsoleHelper.ShowKeyValueResult($"Шаг: {step.operation}", step.explanation);
                Console.WriteLine();
            }

            ConsoleHelper.ShowSeparator(newLineAfter: true);
            ConsoleHelper.ShowKeyValueResult("Итоговые значения", $"a = {a}, b = {b}, c = {c}", ConsoleColor.Green);
        }

        /// Практический пример использования операций присваивания
        private void ShowPracticalExample()
        {
            const string currency = "руб.";
            const double salaryReceived = 50000;
            const double spendingOnGroceries = 15000;
            const double entertainmentExpenses = 5000;

            ConsoleHelper.ShowHeader("ПРАКТИЧЕСКИЙ ПРИМЕР");

            string[] infoLines = {
                "Управление личным бюджетом",
                "Симуляция финансовых операций\nс использованием операторов присваивания"
            };

            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            double budget = 0;
            ConsoleHelper.ShowKeyValueResult("💰 Начальный бюджет", $"{budget:F2} {currency}", ConsoleColor.Yellow);
            Console.WriteLine();

            var transactions = new List<(string operation, double amount, string description)>
            {
                ("+", salaryReceived, "Получена зарплата"),
                ("-", spendingOnGroceries, "Покупка продуктов"),
                ("-", entertainmentExpenses, "Развлечения")
            };

            ConsoleHelper.ShowInfo("Финансовые операции:");
            Console.WriteLine();

            foreach(var trans in transactions)
            {
                budget = trans.operation == "+" ? budget + trans.amount : budget - trans.amount;
                ConsoleHelper.ShowKeyValueResult($"{trans.operation} {trans.description}",
                    $"{trans.amount,8:F2} {currency}");
                ConsoleHelper.ShowKeyValueResult("  Текущий бюджет", $"{budget,8:F2} {currency}");
                Console.WriteLine();
            }

            // Инвестиции
            ConsoleHelper.ShowInfo("Инвестиционные операции:");
            Console.WriteLine();

            budget *= 1.1;
            ConsoleHelper.ShowKeyValueResult("* Инвестиционный доход (+10%)", $"{budget:F2} {currency}");
            Console.WriteLine();

            // Деление с семьей
            budget /= 2;
            ConsoleHelper.ShowKeyValueResult("/ Поделили с семьёй (пополам)", $"{budget:F2} {currency}");
            Console.WriteLine();

            ConsoleHelper.ShowResult("ИТОГОВЫЙ БЮДЖЕТ", $"📊 {budget:F2} {currency}", 35);
        }

        #endregion
    }
}