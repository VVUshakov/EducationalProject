using EducationalProject.Services;

namespace EducationalProject
{
    // Простой класс для настройки списка программ
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
                 * 1. Создайте класс (например, MyProgram)
                 * 2. Раскомментируйте строку ниже:
                 * new MyProgram(),
                */
                
                /* ===== УДАЛЕНИЕ ПРОГРАММ =====
                 * Чтобы временно отключить программу, закомментируйте строку:
                 * new Calculator(),
                 * Чтобы удалить программу навсегда, просто удалите строку
                */
            };
        }

        #region ДОПОЛНИТЕЛЬНЫЕ МЕТОДЫ с сортировкой в выдаче при запросе списка программ
        // Получить программы по категориям
        public static class ByCategory
        {
            public static BaseService[] Математика => new BaseService[]
            {
                new Calculator(),
                new BinaryConverter(),
            };

            public static BaseService[] Текст => new BaseService[]
            {
                new NameEncoder(),
            };

            public static BaseService[] Обучение => new BaseService[]
            {
                new AssignmentDemo(),
            };


            // Почему реализован внутренний класс (класс внутри класса), а не методом:
            // Сравним два варианта (по наглядности) обращения к данным класса из "внешнего кода":

            // Вариант 1: Отдельный вложенный класс
            // ProgramsConfig.ByCategory.Математика  // Четкая иерархия
            // ProgramsConfig.ByCategory.Текст       // Легко найти все категории

            // Вариант 2: Методы в основном классе
            // ProgramsConfig.GetMathPrograms()      // Много методов в одном классе
            // ProgramsConfig.GetTextPrograms()      // Сложнее группировать
        }

        // Получить описание программы по номеру (для справки, например)
        public static string GetProgramInfo(int number)
        {
            string[] descriptions = {
                "1. Калькулятор - складывает, вычитает, умножает и делит числа",
                "2. Конвертер - переводит числа из десятичной в двоичную систему и обратно",
                "3. Кодировщик - преобразует имя в разные коды (цифры, морзянку, шифр)",
                "4. Демонстрация присваивания - показывает как работают операции +=, -=, *= и т.д."
            };

            if(number >= 1 && (number <= descriptions.Length))
            {
                return descriptions[number - 1];
            }
            else
            {
                return $"Программа №{number} не описана";
            }
        }
        #endregion
    }
}