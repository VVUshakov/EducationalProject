namespace EducationalProject.Services
{
    /// <summary>
    /// Программа демонстрации операций присваивания в языке C#.
    /// Предоставляет наглядные примеры использования различных операторов присваивания с пояснениями.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс <see cref="AssignmentDemo"/> демонстрирует:
    /// <list type="bullet">
    /// <item><description>Базовые операторы присваивания (<c>+=</c>, <c>-=</c>, <c>*=</c>, <c>/=</c>, <c>%=</c>)</description></item>
    /// <item><description>Битовые операторы присваивания (<c>&lt;&lt;=</c>, <c>&gt;&gt;=</c>)</description></item>
    /// <item><description>Цепочки операций с несколькими переменными</description></item>
    /// <item><description>Практическое применение в управлении бюджетом</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Цели программы:</strong>
    /// <list type="bullet">
    /// <item><description>Понять разницу между простым и составным присваиванием</description></item>
    /// <item><description>Научиться использовать операторы присваивания для упрощения кода</description></item>
    /// <item><description>Увидеть практическое применение в реальных сценариях</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример использования программы:
    /// <code>
    /// // Выбор демонстрации 1:
    /// Базовые операции присваивания:
    /// value += 10  // Увеличили на 10
    /// value -= 5   // Уменьшили на 5
    /// 
    /// // Выбор демонстрации 3:
    /// Практический пример (бюджет):
    /// + Получена зарплата: 50000 руб.
    /// - Покупка продуктов: 15000 руб.
    /// </code>
    /// </example>
    /// <seealso cref="BaseService"/>
    public class AssignmentDemo : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Получает название программы для отображения в меню
        /// </summary>
        /// <value>Строка "Демонстрация операций присваивания"</value>
        public override string Name => "Демонстрация операций присваивания";

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Запускает программу демонстрации операций присваивания
        /// </summary>
        /// <remarks>
        /// <para>Метод выполняет следующий алгоритм:</para>
        /// <list type="number">
        /// <item><description>Очищает консоль и отображает заголовок программы</description></item>
        /// <item><description>Отображает меню доступных демонстраций</description></item>
        /// <item><description>Запрашивает выбор пользователя (1-3)</description></item>
        /// <item><description>Выполняет выбранную демонстрацию</description></item>
        /// <item><description>Ожидает нажатия клавиши для возврата в меню</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Запуск программы
        /// AssignmentDemo demo = new AssignmentDemo();
        /// demo.Run();
        /// 
        /// // Последовательность работы:
        /// // 1. Показывается заголовок "Демонстрация операций присваивания"
        /// // 2. Отображается меню с тремя вариантами демонстраций
        /// // 3. Пользователь выбирает 1, 2 или 3
        /// // 4. Выполняется соответствующая демонстрация
        /// // 5. Ожидается нажатие клавиши для возврата
        /// </code>
        /// </example>
        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);
            ShowMenu();

            int choice = InputValidator.GetValidMenuChoice(1, 3, ">>> Введите номер демонстрации (1-3): ");
            ExecuteChoice(choice);

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        #endregion

        #region ===== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Отображает меню доступных демонстраций
        /// </summary>
        /// <remarks>
        /// Использует <see cref="ConsoleHelper.ShowMenu"/> для отображения трех вариантов демонстраций.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Вывод в консоль:
        /// ╔══════════════════════════════════════╗
        /// ║     ДОСТУПНЫЕ ДЕМОНСТРАЦИИ           ║
        /// ╠══════════════════════════════════════╣
        /// ║ 1. Базовые операции присваивания     ║
        /// ║ 2. Комбинированные операции          ║
        /// ║ 3. Практический пример (бюджет)      ║
        /// ╚══════════════════════════════════════╝
        /// </code>
        /// </example>
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

        /// <summary>
        /// Выполняет выбранную пользователем демонстрацию
        /// </summary>
        /// <param name="choice">Номер выбранной демонстрации (1-3)</param>
        /// <remarks>
        /// <para>В зависимости от выбора пользователя вызывает соответствующий метод:</para>
        /// <list type="bullet">
        /// <item><description>1 → <see cref="ShowBasicOperations"/></description></item>
        /// <item><description>2 → <see cref="ShowCombinedOperations"/></description></item>
        /// <item><description>3 → <see cref="ShowPracticalExample"/></description></item>
        /// </list>
        /// <para>При некорректном выборе показывает предупреждение и выполняет базовые операции.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// ExecuteChoice(1); // Выполняет ShowBasicOperations()
        /// ExecuteChoice(2); // Выполняет ShowCombinedOperations()
        /// ExecuteChoice(3); // Выполняет ShowPracticalExample()
        /// ExecuteChoice(5); // Показывает предупреждение и выполняет ShowBasicOperations()
        /// </code>
        /// </example>
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

        /// <summary>
        /// Демонстрирует базовые операции присваивания с одной переменной
        /// </summary>
        /// <remarks>
        /// <para>Метод показывает использование составных операторов присваивания:</para>
        /// <list type="table">
        /// <listheader>
        /// <term>Оператор</term>
        /// <description>Эквивалент</description>
        /// <description>Описание</description>
        /// </listheader>
        /// <item>
        /// <term><c>value += 10</c></term>
        /// <description><c>value = value + 10</c></description>
        /// <description>Увеличивает значение на 10</description>
        /// </item>
        /// <item>
        /// <term><c>value -= 5</c></term>
        /// <description><c>value = value - 5</c></description>
        /// <description>Уменьшает значение на 5</description>
        /// </item>
        /// <item>
        /// <term><c>value *= 2</c></term>
        /// <description><c>value = value * 2</c></description>
        /// <description>Умножает значение на 2</description>
        /// </item>
        /// <item>
        /// <term><c>value /= 3</c></term>
        /// <description><c>value = value / 3</c></description>
        /// <description>Делит значение на 3</description>
        /// </item>
        /// <item>
        /// <term><c>value %= 7</c></term>
        /// <description><c>value = value % 7</c></description>
        /// <description>Вычисляет остаток от деления на 7</description>
        /// </item>
        /// <item>
        /// <term><c>value &lt;&lt;= 1</c></term>
        /// <description><c>value = value &lt;&lt; 1</c></description>
        /// <description>Сдвигает биты влево (умножение на 2)</description>
        /// </item>
        /// <item>
        /// <term><c>value &gt;&gt;= 1</c></term>
        /// <description><c>value = value &gt;&gt; 1</c></description>
        /// <description>Сдвигает биты вправо (деление на 2)</description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Пример выполнения:
        /// Начальное значение: 100
        /// После 'value += 10': 110  // Увеличили на 10
        /// После 'value -= 5':  105  // Уменьшили на 5
        /// После 'value *= 2':  210  // Умножили на 2
        /// После 'value /= 3':   70  // Разделили на 3
        /// Итоговое значение: 70
        /// </code>
        /// </example>
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

        /// <summary>
        /// Демонстрирует цепочки операций с несколькими переменными
        /// </summary>
        /// <remarks>
        /// <para>Метод показывает, как операции присваивания могут влиять на несколько переменных одновременно.</para>
        /// <para>Используются три переменные (a, b, c), которые взаимодействуют через операции:</para>
        /// <list type="number">
        /// <item><description>c /= 2 - делит переменную c на 2</description></item>
        /// <item><description>b -= c - вычитает из b новое значение c</description></item>
        /// <item><description>a += b - добавляет к a новое значение b</description></item>
        /// </list>
        /// <para>Каждая операция меняет состояние системы, что наглядно демонстрирует порядок выполнения.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Начальные значения:
        /// a = 10, b = 20, c = 30
        /// 
        /// // Выполняем операции:
        /// c /= 2    → c = 15
        /// b -= c    → b = 5  (20 - 15)
        /// a += b    → a = 15 (10 + 5)
        /// 
        /// // Итоговые значения:
        /// a = 15, b = 5, c = 15
        /// </code>
        /// </example>
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

        /// <summary>
        /// Демонстрирует практическое применение операций присваивания для управления бюджетом
        /// </summary>
        /// <remarks>
        /// <para>Метод моделирует финансовые операции с использованием операторов присваивания:</para>
        /// <list type="bullet">
        /// <item><description>Начальный бюджет: 0 рублей</description></item>
        /// <item><description>Поступление зарплаты: <c>budget += salaryReceived</c></description></item>
        /// <item><description>Расходы на продукты: <c>budget -= spendingOnGroceries</c></description></item>
        /// <item><description>Расходы на развлечения: <c>budget -= entertainmentExpenses</c></description></item>
        /// <item><description>Инвестиционный доход: <c>budget *= 1.1</c> (увеличение на 10%)</description></item>
        /// <item><description>Деление бюджета: <c>budget /= 2</c></description></item>
        /// </list>
        /// <para>Используются следующие константы:</para>
        /// <list type="bullet">
        /// <item><description><c>currency</c> = "руб." (валюта)</description></item>
        /// <item><description><c>salaryReceived</c> = 50000 (зарплата)</description></item>
        /// <item><description><c>spendingOnGroceries</c> = 15000 (продукты)</description></item>
        /// <item><description><c>entertainmentExpenses</c> = 5000 (развлечения)</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Пример выполнения:
        /// Начальный бюджет: 0.00 руб.
        /// + Получена зарплата: 50000.00 руб.
        ///   Текущий бюджет: 50000.00 руб.
        /// - Покупка продуктов: 15000.00 руб.
        ///   Текущий бюджет: 35000.00 руб.
        /// - Развлечения: 5000.00 руб.
        ///   Текущий бюджет: 30000.00 руб.
        /// * Инвестиционный доход (+10%): 33000.00 руб.
        /// / Поделили с семьёй (пополам): 16500.00 руб.
        /// ИТОГОВЫЙ БЮДЖЕТ: 16500.00 руб.
        /// </code>
        /// </example>
        private void ShowPracticalExample()
        {
            const string currency = "руб.";
            const double salaryReceived = 50000;
            const double spendingOnGroceries = 15000;
            const double entertainmentExpenses = 5000;

            ConsoleHelper.ShowHeader("ПРАКТИЧЕСКИЙ ПРИМЕР");

            string[] infoLines = {
                "Управление личным бюджетом",
                "Симуляция финансовых операций с использованием операторов присваивания"
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