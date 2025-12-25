// Подключаем пространство имен для доступа к классам программ
using EducationalProject.Services;

// Объявляем пространство имен
namespace EducationalProject
{
    // Статический класс для хранения списка всех программ
    public static class ProgramsConfig
    {
        // Метод, который возвращает массив всех доступных программ
        public static BaseService[] GetAllPrograms()
        {
            // Создаем и возвращаем массив с программами
            return new BaseService[]  // Создаем новый массив типа BaseService[]
            {
                new Calculator(),       // Создаем объект Калькулятора (№1 в меню)
                new BinaryConverter(),  // Создаем объект Конвертера (№2 в меню)
                new NameEncoder(),      // Создаем объект Кодировщика (№3 в меню)
                new AssignmentDemo(),   // Создаем объект Демонстрации (№4 в меню)
            };
        }

        // Вложенный статический класс для сортировки программ по категориям
        public static class ByCategory
        {
            // Свойство, возвращающее математические программы
            public static BaseService[] Математика => new BaseService[]  // Стрелочная функция
            {
                new Calculator(),       // Калькулятор - математическая программа
                new BinaryConverter(),  // Конвертер систем счисления - тоже математика
            };

            // Свойство, возвращающее программы для работы с текстом
            public static BaseService[] Текст => new BaseService[]
            {
                new NameEncoder(),      // Кодировщик имени работает с текстом
            };

            // Свойство, возвращающее обучающие демонстрации
            public static BaseService[] Обучение => new BaseService[]
            {
                new AssignmentDemo(),   // Демонстрация операций - обучающая программа
            };

            // Метод для показа всех программ, сгруппированных по категориям
            public static void ShowAllProgramsByCategory()
            {
                // Очищаем экран и показываем заголовок
                ConsoleHelper.ClearAndShowHeader("ПРОГРАММЫ ПО КАТЕГОРИЯМ");

                // Показываем заголовок категории
                ConsoleHelper.ShowInfo("Математические программы:");

                // Перебираем все математические программы
                foreach(var program in Математика)  // var автоматически определяет тип
                {
                    Console.WriteLine($"  • {program.Name}");  // Выводим с маркером •
                }

                // Пустая строка и заголовок следующей категории
                ConsoleHelper.ShowInfo("\nПрограммы для работы с текстом:");

                // Перебираем текстовые программы
                foreach(var program in Текст)
                {
                    Console.WriteLine($"  • {program.Name}");
                }

                // Еще одна категория
                ConsoleHelper.ShowInfo("\nОбучающие демонстрации:");

                // Перебираем обучающие программы
                foreach(var program in Обучение)
                {
                    Console.WriteLine($"  • {program.Name}");
                }
            }
        }

        // Метод для показа подробной информации о всех программах
        public static void ShowAllProgramsInfo()
        {
            // Очищаем экран и показываем заголовок
            ConsoleHelper.ClearAndShowHeader("ПОДРОБНАЯ ИНФОРМАЦИЯ О ПРОГРАММАХ");

            // Получаем все программы
            var programs = GetAllPrograms();

            // Массив с описаниями программ (в том же порядке, что и в GetAllPrograms)
            string[] descriptions = {
                "выполняет базовые арифметические операции: сложение, вычитание, умножение, деление и нахождение остатка",
                "переводит числа между десятичной и двоичной системами (8-битные числа)",
                "преобразует имя в разные коды: алфавитные позиции, азбуку Морзе, простой шифр",
                "показывает как работают составные операторы: +=, -=, *=, /=, %=, <<=, >>="
            };

            // Перебираем все программы по порядку
            for(int i = 0; i < programs.Length; i++)  // i от 0 до 3
            {
                // Меняем цвет текста на желтый
                Console.ForegroundColor = ConsoleColor.Yellow;

                // Выводим номер программы (i+1, потому что люди считают с 1)
                Console.Write($"Программа {i + 1}: ");

                // Возвращаем стандартный цвет
                Console.ResetColor();

                // Выводим название программы
                Console.WriteLine(programs[i].Name);

                // Выводим описание с отступом
                Console.WriteLine($"  {descriptions[i]}");

                // Пустая строка между программами
                Console.WriteLine();
            }
        }
    }
}