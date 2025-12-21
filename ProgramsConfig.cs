using EducationalProject.Services;

namespace EducationalProject
{
    // Класс конфигуратор для настройки списка программ
    public static class ProgramsConfig
    {
        // Список всех доступных программ
        // Чтобы добавить новую программу - просто добавьте ее в этот массив
        // Чтобы убрать программу - закомментируйте или удалите строку
        public static BaseService[] GetAllPrograms()
        {
            return new BaseService[]
            {
                // ===== ОСНОВНЫЕ ПРОГРАММЫ =====
                new Calculator(),              // №1: Калькулятор
                new BinaryConverter(),         // №2: Конвертер систем счисления
                new NameEncoder(),             // №3: Кодировщик имени
                new AssignmentDemo(),          // №4: Демо операций присваивания
                
                /* ===== ДОБАВЛЕНИЕ НОВЫХ ПРОГРАММ =====
                 * Чтобы добавить новую программу:
                 * 1. Создайте класс (например, MyProgram) наследуемый от BaseService
                 * 2. Реализуйте свойства Name и метод Run()
                 * 3. Раскомментируйте строку ниже:
                 * new MyProgram(),
                 * 
                 * Пример шаблона новой программы:
                 * 
                 * public class MyProgram : BaseService
                 * {
                 *     public override string Name => "Моя программа";
                 *     
                 *     public override void Run()
                 *     {
                 *         // Используйте ConsoleHelper для вывода
                 *         ConsoleHelper.ClearAndShowHeader(Name);
                 *         ConsoleHelper.ShowInfo("Это моя новая программа!");
                 *         ConsoleHelper.WaitForAnyKey();
                 *     }
                 * }
                */
                
                /* ===== УДАЛЕНИЕ ПРОГРАММ =====
                 * Чтобы временно отключить программу, закомментируйте строку:
                 * // new Calculator(),
                 * 
                 * Чтобы удалить программу навсегда, просто удалите строку
                 * Не забудьте также удалить соответствующий .cs файл
                */
            };
        }

        #region ДОПОЛНИТЕЛЬНЫЕ МЕТОДЫ с сортировкой в выдаче при запросе списка программ

        /// <summary>
        /// Получить программы по категориям
        /// </summary>
        public static class ByCategory
        {
            /// <summary>
            /// Математические программы
            /// </summary>
            public static BaseService[] Математика => new BaseService[]
            {
                new Calculator(),
                new BinaryConverter(),
            };

            /// <summary>
            /// Программы для работы с текстом
            /// </summary>
            public static BaseService[] Текст => new BaseService[]
            {
                new NameEncoder(),
            };

            /// <summary>
            /// Обучающие демонстрации
            /// </summary>
            public static BaseService[] Обучение => new BaseService[]
            {
                new AssignmentDemo(),
            };

            /// <summary>
            /// Пример использования: вывод всех программ по категориям
            /// </summary>
            public static void ShowAllProgramsByCategory()
            {
                ConsoleHelper.ClearAndShowHeader("ПРОГРАММЫ ПО КАТЕГОРИЯМ");

                ConsoleHelper.ShowInfo("Математические программы:");
                foreach(var program in Математика)
                {
                    Console.WriteLine($"  • {program.Name}");
                }

                ConsoleHelper.ShowInfo("\nПрограммы для работы с текстом:");
                foreach(var program in Текст)
                {
                    Console.WriteLine($"  • {program.Name}");
                }

                ConsoleHelper.ShowInfo("\nОбучающие демонстрации:");
                foreach(var program in Обучение)
                {
                    Console.WriteLine($"  • {program.Name}");
                }
            }
        }

        /// <summary>
        /// Получить описание программы по номеру (для справки)
        /// </summary>
        public static string GetProgramInfo(int number)
        {
            string[] descriptions = {
                "1. Калькулятор - выполняет базовые арифметические операции: сложение, вычитание, умножение, деление и нахождение остатка",
                "2. Конвертер систем счисления - переводит числа между десятичной и двоичной системами (8-битные числа)",
                "3. Кодировщик имени - преобразует имя в разные коды: алфавитные позиции, азбуку Морзе, простой шифр",
                "4. Демонстрация операций присваивания - показывает как работают составные операторы: +=, -=, *=, /=, %=, <<=, >>="
            };

            if(number >= 1 && number <= descriptions.Length)
            {
                return descriptions[number - 1];
            }
            else
            {
                return $"Программа №{number} не описана";
            }
        }

        /// <summary>
        /// Показать подробную информацию о всех программах
        /// </summary>
        public static void ShowAllProgramsInfo()
        {
            ConsoleHelper.ClearAndShowHeader("ПОДРОБНАЯ ИНФОРМАЦИЯ О ПРОГРАММАХ");

            var programs = GetAllPrograms();
            for(int i = 0; i < programs.Length; i++)
            {
                string info = GetProgramInfo(i + 1);
                string[] lines = info.Split(" - ");

                ConsoleHelper.ShowKeyValueResult($"Программа {i + 1}", lines[0], ConsoleColor.Yellow);
                Console.WriteLine($"  {lines[1]}");
                Console.WriteLine();
            }
        }

        #endregion
    }
}