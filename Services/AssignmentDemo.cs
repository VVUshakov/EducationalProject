// Объявляем пространство имен для сервисов
namespace EducationalProject.Services
{
    // Класс Демонстрация операций присваивания
    public class AssignmentDemo : BaseService
    {
        // Реализуем свойство Name
        public override string Name => "Демонстрация операций присваивания";

        // Реализуем метод Run()
        public override void Run()
        {
            // Очищаем экран и показываем заголовок
            ConsoleHelper.ClearAndShowHeader(Name);

            // Показываем меню демонстраций
            ShowMenu();

            // Получаем выбор пользователя (1, 2 или 3)
            int choice = InputValidator.GetValidMenuChoice(1, 3, ">>> Введите номер демонстрации (1-3): ");

            // Выполняем выбранную демонстрацию
            ExecuteChoice(choice);

            // Ждем нажатия клавиши
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        // Метод для показа меню демонстраций
        private void ShowMenu()
        {
            // Массив с названиями демонстраций
            string[] menuItems = {
                "Базовые операции присваивания",
                "Комбинированные операции",
                "Практический пример (бюджет)"
            };

            // Показываем меню
            ConsoleHelper.ShowMenu("ДОСТУПНЫЕ ДЕМОНСТРАЦИИ", menuItems);

            // Пустая строка
            Console.WriteLine();
        }

        // Метод для выполнения выбранной демонстрации
        private void ExecuteChoice(int choice)
        {
            // Конструкция switch - выбираем демонстрацию
            switch(choice)
            {
                case 1:  // Если выбрали 1
                    ShowBasicOperations();  // Показываем базовые операции
                    break;  // Выходим из switch

                case 2:  // Если выбрали 2
                    ShowCombinedOperations();  // Показываем комбинированные операции
                    break;

                case 3:  // Если выбрали 3
                    ShowPracticalExample();  // Показываем практический пример
                    break;

                default:  // Если другой номер (теоретически невозможно из-за валидации)
                    ConsoleHelper.ShowError("Неверный выбор. Показываем базовые операции...");
                    ShowBasicOperations();  // Все равно показываем базовые операции
                    break;
            }
        }

        // Демонстрация 1: Базовые операции присваивания
        private void ShowBasicOperations()
        {
            // Показываем заголовок
            ConsoleHelper.ShowHeader("БАЗОВЫЕ ОПЕРАЦИИ ПРИСВАИВАНИЯ");

            // Массив с описанием
            string[] infoLines = {
                "Демонстрация составных операторов: +=, -=, *=, /=, %=",
                "А также битовых сдвигов: <<=, >>="
            };

            // Показываем информационный блок
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Начальное значение переменной
            int value = 100;

            // Показываем начальное значение
            ConsoleHelper.ShowInfo($"Начальное значение: {value}");

            // Создаем список операций для демонстрации
            // Каждый элемент - кортеж (операция, описание, результат)
            var operations = new List<(string operation, string description, int result)>
            {
                ("value += 10", "Увеличили на 10", value += 10),  // Прибавляем 10
                ("value -= 5",  "Уменьшили на 5", value -= 5),    // Вычитаем 5
                ("value *= 2",  "Умножили на 2", value *= 2),     // Умножаем на 2
                ("value /= 3",  "Разделили на 3 (целочисленное деление)", value /= 3), // Делим на 3
                ("value %= 7",  "Взяли остаток от деления на 7", value %= 7), // Остаток от деления
                ("value <<= 1", "Сдвиг влево (умножение на 2)", value <<= 1), // Битовый сдвиг влево
                ("value >>= 1", "Сдвиг вправо (деление на 2)", value >>= 1)   // Битовый сдвиг вправо
            };

            // Перебираем все операции
            foreach(var op in operations)  // Для каждой операции в списке
            {
                // Выводим результат операции
                // {op.result,3} - выравнивание по правому краю на 3 символа
                Console.WriteLine($"После '{op.operation}': {op.result,3}  // {op.description}");
            }

            // Рисуем разделитель
            Console.WriteLine(new string('═', 35));

            // Показываем итоговое значение (зеленым цветом)
            ConsoleHelper.ShowSuccess($"Итоговое значение: {value}");
        }

        // Демонстрация 2: Комбинированные операции
        private void ShowCombinedOperations()
        {
            // Показываем заголовок
            ConsoleHelper.ShowHeader("КОМБИНИРОВАННЫЕ ОПЕРАЦИИ");

            // Массив с описанием
            string[] infoLines = {
                "Цепочка операций с несколькими переменными",
                "Показывает взаимодействие переменных через операции"
            };

            // Показываем информационный блок
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Инициализируем три переменные
            int a = 10, b = 20, c = 30;  // Можно объявить несколько переменных в одной строке

            // Показываем начальные значения
            ConsoleHelper.ShowInfo($"Начальные значения: a = {a}, b = {b}, c = {c}");

            // Выводим заголовок для цепочки операций
            Console.WriteLine("\nВыполняем цепочку операций:\n");

            // Операция 1: делим c на 2
            c /= 2;  // Эквивалентно c = c / 2;

            // Показываем результат
            Console.WriteLine($"c /= 2    → c = 15\n   Текущие: a={a}, b={b}, c={c}");

            // Операция 2: вычитаем c из b
            b -= c;  // Эквивалентно b = b - c;

            // Показываем результат
            Console.WriteLine($"b -= c    → b = 5  (20 - 15)\n   Текущие: a={a}, b={b}, c={c}");

            // Операция 3: добавляем b к a
            a += b;  // Эквивалентно a = a + b;

            // Показываем результат
            Console.WriteLine($"a += b    → a = 15 (10 + 5)\n   Текущие: a={a}, b={b}, c={c}");

            // Рисуем разделитель
            Console.WriteLine(new string('═', 35));

            // Показываем итоговые значения
            ConsoleHelper.ShowSuccess($"Итоговые значения: a = {a}, b = {b}, c = {c}");
        }

        // Демонстрация 3: Практический пример с бюджетом
        private void ShowPracticalExample()
        {
            // Константы для финансовых операций
            const double salaryReceived = 50000;          // Зарплата
            const double spendingOnGroceries = 15000;     // Траты на продукты
            const double entertainmentExpenses = 5000;    // Траты на развлечения
            const string currency = "руб.";               // Валюта

            // Показываем заголовок
            ConsoleHelper.ShowHeader("ПРАКТИЧЕСКИЙ ПРИМЕР");

            // Массив с описанием
            string[] infoLines = {
                "Управление личным бюджетом",
                "Симуляция финансовых операций\nс использованием операторов присваивания"
            };

            // Показываем информационный блок
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Начальный бюджет
            double budget = 0;

            // Показываем начальный бюджет
            ConsoleHelper.ShowInfo($"💰 Начальный бюджет: {budget:F2} {currency}");

            // Заголовок для финансовых операций
            Console.WriteLine("\nФинансовые операции:\n");

            // Операция 1: получение зарплаты
            budget += salaryReceived;  // Увеличиваем бюджет на зарплату

            // Показываем операцию и текущий бюджет
            // {salaryReceived,8:F2} - выравнивание на 8 символов, 2 знака после запятой
            Console.WriteLine($"+ Получена зарплата: {salaryReceived,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            // Операция 2: покупка продуктов
            budget -= spendingOnGroceries;  // Уменьшаем бюджет на продукты

            // Показываем операцию
            Console.WriteLine($"- Покупка продуктов: {spendingOnGroceries,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            // Операция 3: траты на развлечения
            budget -= entertainmentExpenses;  // Уменьшаем бюджет на развлечения

            // Показываем операцию
            Console.WriteLine($"- Развлечения: {entertainmentExpenses,8:F2} {currency}");
            Console.WriteLine($"  Текущий бюджет: {budget,8:F2} {currency}");

            // Заголовок для инвестиционных операций
            Console.WriteLine("\nИнвестиционные операции:\n");

            // Операция 4: инвестиционный доход (+10%)
            budget *= 1.1;  // Умножаем бюджет на 1.1 (увеличиваем на 10%)

            // Показываем операцию
            Console.WriteLine($"* Инвестиционный доход (+10%): {budget:F2} {currency}");

            // Операция 5: делим бюджет пополам (поделились с семьей)
            budget /= 2;  // Делим бюджет на 2

            // Показываем операцию
            Console.WriteLine($"/ Поделили с семьёй (пополам): {budget:F2} {currency}");

            // Рисуем разделитель
            Console.WriteLine(new string('═', 35));

            // Показываем итоговый бюджет
            ConsoleHelper.ShowSuccess($"ИТОГОВЫЙ БЮДЖЕТ: 📊 {budget:F2} {currency}");
        }
    }
}