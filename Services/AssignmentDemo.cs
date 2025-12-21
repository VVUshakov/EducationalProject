namespace EducationalProject.Services
{
    /// <summary>
    /// Программа 4: Демонстрация операций присваивания
    /// 
    /// Демонстрирует различные типы операторов присваивания в языке C#:
    /// 1. Базовые составные операторы (+=, -=, *=, /=, %=)
    /// 2. Комбинированные операции с несколькими переменными
    /// 3. Практическое применение в финансовых расчетах
    /// 
    /// Цель: наглядно показать как составные операторы присваивания
    /// упрощают и сокращают код при работе с переменными.
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
            ShowHeader(); // Показать заголовок
            ShowMenu(); // Показать меню выбора

            string choice = GetUserChoice(); // Получить выбор пользователя

            ExecuteChoice(choice); // Выполнить выбранный вариант
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        // Показать заголовок программы
        private void ShowHeader()
        {
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════════════════╗");
            Console.WriteLine("║     ДЕМОНСТРАЦИЯ ОПЕРАЦИЙ ПРИСВАИВАНИЯ      ║");
            Console.WriteLine("╚═════════════════════════════════════════════╝");
            Console.WriteLine();
        }

        // Показать меню выбора демонстраций        
        private void ShowMenu()
        {
            Console.WriteLine("Доступные демонстрации:");
            Console.WriteLine("1. Базовые операции присваивания");
            Console.WriteLine("2. Комбинированные операции");
            Console.WriteLine("3. Практический пример (бюджет)");
            Console.WriteLine();
        }

        /// <summary>
        /// Ожидать нажатия клавиши для продолжения
        /// </summary>
        private void WaitForContinue()
        {
            Console.WriteLine("\n═══════════════════════════════════════════════");
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        // Получить выбор пользователя
        private string GetUserChoice()
        {
            Console.Write(">>> Введите номер демонстрации (1-3): ");
            return Console.ReadLine()?.Trim() ?? "1";
        }

        /// Выполнить выбранный вариант демонстрации
        private void ExecuteChoice(string choice)
        {
            switch(choice)
            {
                case "1":
                    ShowBasicOperations();
                    break;
                case "2":
                    ShowCombinedOperations();
                    break;
                case "3":
                    ShowPracticalExample();
                    break;
                default:
                    Console.WriteLine("\nНеверный выбор. Показываем базовые операции...");
                    ShowBasicOperations();
                    break;
            }

            WaitForContinue(); // Ждать подтверждения
        }

        #endregion

        #region ===== ДЕМОНСТРАЦИОННЫЕ МЕТОДЫ =====

        /// Демонстрация базовых операций присваивания
        private void ShowBasicOperations()
        {
            Console.WriteLine("\n=== БАЗОВЫЕ ОПЕРАЦИИ ПРИСВАИВАНИЯ ===\n");

            int value = 100;
            Console.WriteLine($"Начальное значение: {value}");

            // Демонстрация различных операторов присваивания
            value += 10;
            Console.WriteLine($"После 'value += 10':   {value}  // Увеличили на 10");

            value -= 5;
            Console.WriteLine($"После 'value -= 5':    {value}  // Уменьшили на 5");

            value *= 2;
            Console.WriteLine($"После 'value *= 2':    {value}  // Умножили на 2");

            value /= 3;
            Console.WriteLine($"После 'value /= 3':    {value}  // Разделили на 3 (целочисленное деление)");

            value %= 7;
            Console.WriteLine($"После 'value %= 7':    {value}  // Взяли остаток от деления на 7");

            value <<= 1;
            Console.WriteLine($"После 'value <<= 1':   {value}  // Сдвиг влево (умножение на 2)");

            value >>= 1;
            Console.WriteLine($"После 'value >>= 1':   {value}  // Сдвиг вправо (деление на 2)");
        }

        /// Демонстрация комбинированных операций
        private void ShowCombinedOperations()
        {
            Console.WriteLine("\n=== КОМБИНИРОВАННЫЕ ОПЕРАЦИИ ===\n");

            // Инициализация переменных
            int a = 10, b = 20, c = 30;

            Console.WriteLine("Начальные значения:");
            Console.WriteLine($"a = {a}, b = {b}, c = {c}\n");

            // Цепочка операций присваивания
            Console.WriteLine("Выполняем цепочку операций:");
            Console.WriteLine("c /= 2;   // c = 30 / 2 = 15");
            c /= 2;
            Console.WriteLine($"Текущие значения: a={a}, b={b}, c={c}\n");

            Console.WriteLine("b -= c;   // b = 20 - 15 = 5");
            b -= c;
            Console.WriteLine($"Текущие значения: a={a}, b={b}, c={c}\n");

            Console.WriteLine("a += b;   // a = 10 + 5 = 15");
            a += b;
            Console.WriteLine($"Текущие значения: a={a}, b={b}, c={c}");

            Console.WriteLine("\nИтоговые значения:");
            Console.WriteLine($"a = {a}, b = {b}, c = {c}");
        }

        /// Практический пример использования операций присваивания
        private void ShowPracticalExample()
        {
            const string currency = "руб."; // денежная единица валюты
            const double salaryReceived = 50000; // полученная зарплата
            const double spendingOnGroceries = 15000; // Расходы на продукты питания
            const double entertainmentExpenses = 5000; // Расходы на развлечения

            Console.WriteLine("\n=== ПРАКТИЧЕСКИЙ ПРИМЕР: УПРАВЛЕНИЕ БЮДЖЕТОМ ===\n");

            double budget = 0;
            Console.WriteLine($"💰 Начальный бюджет: {budget} {currency}\n");

            // Симуляция операций с бюджетом
            budget += 50000;
            Console.WriteLine($"+ Получена зарплата:      {salaryReceived} {currency}");
            Console.WriteLine($"  Текущий бюджет:         {budget} {currency}\n");

            budget -= 15000;
            Console.WriteLine($"- Покупка продуктов:      {spendingOnGroceries} {currency}");
            Console.WriteLine($"  Текущий бюджет:         {budget} {currency}\n");

            budget -= 5000;
            Console.WriteLine($"- Развлечения:            {entertainmentExpenses} {currency}");
            Console.WriteLine($"  Текущий бюджет:         {budget} {currency}\n");

            budget *= 1.1;
            Console.WriteLine($"* Инвестиционный доход (+10%):");
            Console.WriteLine($"  Текущий бюджет:         {budget} {currency}\n");

            budget /= 2;
            Console.WriteLine($"/ Поделили с семьёй (пополам):");
            Console.WriteLine($"  Текущий бюджет:         {budget} {currency}");

            Console.WriteLine();
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.WriteLine($"📊 Итоговый бюджет: {budget} {currency}");
            Console.WriteLine("═══════════════════════════════════════════════");
        }

        #endregion
    }
}