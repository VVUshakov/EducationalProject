using EducationalProject.Services;

namespace EducationalProject
{
    /// <summary>
    /// Статический класс-конфигуратор для управления списком образовательных программ.
    /// Предоставляет методы для получения, организации и описания доступных программ.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс выполняет роль централизованного реестра всех программ в системе:
    /// <list type="bullet">
    /// <item><description>Определяет состав доступных программ</description></item>
    /// <item><description>Организует программы по категориям</description></item>
    /// <item><description>Предоставляет описания и метаинформацию</description></item>
    /// <item><description>Служит точкой расширения для добавления новых программ</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Все программы должны наследоваться от <see cref="BaseService"/> и быть зарегистрированы
    /// в массиве, возвращаемом методом <see cref="GetAllPrograms"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Получение всех программ для меню
    /// BaseService[] programs = ProgramsConfig.GetAllPrograms();
    /// 
    /// // Получение программ по категории
    /// BaseService[] mathPrograms = ProgramsConfig.ByCategory.Математика;
    /// 
    /// // Показ информации о программе
    /// string info = ProgramsConfig.GetProgramInfo(1);
    /// 
    /// // Показ всех программ с описаниями
    /// ProgramsConfig.ShowAllProgramsInfo();
    /// </code>
    /// </example>
    /// <seealso cref="BaseService"/>
    /// <seealso cref="MenuManager"/>
    public static class ProgramsConfig
    {
        /// <summary>
        /// Возвращает массив всех доступных образовательных программ.
        /// </summary>
        /// <returns>
        /// Массив объектов, наследующих от <see cref="BaseService"/>, представляющих доступные программы.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Метод является основной точкой конфигурации системы. Для изменения состава программ:
        /// <list type="number">
        /// <item><description>Добавить новую программу - создать экземпляр класса в массиве</description></item>
        /// <item><description>Удалить программу - закомментировать или удалить строку из массива</description></item>
        /// <item><description>Изменить порядок - переставить строки в массиве (влияет на нумерацию в меню)</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Порядок программ в массиве определяет их нумерацию в главном меню:
        /// первая программа будет под номером 1, вторая - под номером 2, и т.д.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Пример добавления новой программы MyProgram:
        /// return new BaseService[]
        /// {
        ///     new Calculator(),
        ///     new BinaryConverter(),
        ///     new MyProgram(),  // ← Новая программа будет №3 в меню
        ///     new NameEncoder(),
        /// };
        /// </code>
        /// </example>
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
        /// Вложенный статический класс для организации программ по тематическим категориям.
        /// </summary>
        /// <remarks>
        /// Позволяет группировать программы по предметным областям для:
        /// <list type="bullet">
        /// <item><description>Структурированного отображения в справке</description></item>
        /// <item><description>Фильтрации программ по интересам пользователя</description></item>
        /// <item><description>Организации учебного материала</description></item>
        /// </list>
        /// Каждая категория возвращает массив программ соответствующей тематики.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение математических программ
        /// BaseService[] mathPrograms = ProgramsConfig.ByCategory.Математика;
        /// 
        /// // Показ всех программ по категориям
        /// ProgramsConfig.ByCategory.ShowAllProgramsByCategory();
        /// </code>
        /// </example>
        public static class ByCategory
        {
            /// <summary>
            /// Возвращает массив программ, связанных с математикой.
            /// </summary>
            /// <returns>
            /// Массив программ для изучения математических концепций.
            /// </returns>
            /// <remarks>
            /// Текущий состав:
            /// <list type="bullet">
            /// <item><description><see cref="Calculator"/> - арифметические операции</description></item>
            /// <item><description><see cref="BinaryConverter"/> - системы счисления</description></item>
            /// </list>
            /// </remarks>
            public static BaseService[] Математика => new BaseService[]
            {
                new Calculator(),
                new BinaryConverter(),
            };

            /// <summary>
            /// Возвращает массив программ для работы с текстом и строками.
            /// </summary>
            /// <returns>
            /// Массив программ для обработки и преобразования текста.
            /// </returns>
            /// <remarks>
            /// Текущий состав:
            /// <list type="bullet">
            /// <item><description><see cref="NameEncoder"/> - кодирование и шифрование текста</description></item>
            /// </list>
            /// </remarks>
            public static BaseService[] Текст => new BaseService[]
            {
                new NameEncoder(),
            };

            /// <summary>
            /// Возвращает массив демонстрационных и обучающих программ.
            /// </summary>
            /// <returns>
            /// Массив программ, демонстрирующих программистские концепции.
            /// </returns>
            /// <remarks>
            /// Текущий состав:
            /// <list type="bullet">
            /// <item><description><see cref="AssignmentDemo"/> - операторы присваивания</description></item>
            /// </list>
            /// </remarks>
            public static BaseService[] Обучение => new BaseService[]
            {
                new AssignmentDemo(),
            };

            /// <summary>
            /// Отображает в консоли структурированный список всех программ, сгруппированных по категориям.
            /// </summary>
            /// <remarks>
            /// <para>
            /// Метод выполняет:
            /// <list type="number">
            /// <item><description>Очистку консоли и вывод заголовка</description></item>
            /// <item><description>Последовательный вывод программ каждой категории с маркированным списком</description></item>
            /// <item><description>Использование информационных стилей <see cref="ConsoleHelper"/> для форматирования</description></item>
            /// </list>
            /// </para>
            /// <para>
            /// Полезен для предоставления пользователю обзора доступных возможностей.
            /// </para>
            /// </remarks>
            /// <example>
            /// <code>
            /// // Показ всех программ по категориям
            /// ProgramsConfig.ByCategory.ShowAllProgramsByCategory();
            /// 
            /// // Пример вывода:
            /// // ================================
            /// //     ПРОГРАММЫ ПО КАТЕГОРИЯМ
            /// // ================================
            /// // Математические программы:
            /// //   • Калькулятор
            /// //   • Конвертер систем счисления
            /// // 
            /// // Программы для работы с текстом:
            /// //   • Кодировщик имени
            /// // 
            /// // Обучающие демонстрации:
            /// //   • Демонстрация операций присваивания
            /// </code>
            /// </example>
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
        /// Возвращает текстовое описание указанной программы.
        /// </summary>
        /// <param name="number">Номер программы в главном меню (начинается с 1).</param>
        /// <returns>
        /// Строка с подробным описанием программы. Если номер недопустим, возвращается сообщение об ошибке.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Описания хранятся в фиксированном массиве и соответствуют порядку программ,
        /// возвращаемых методом <see cref="GetAllPrograms"/>.
        /// </para>
        /// <para>
        /// Формат описания: "Номер. Название - Подробное описание функциональности"
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение описания первой программы
        /// string description = ProgramsConfig.GetProgramInfo(1);
        /// // Результат: "1. Калькулятор - выполняет базовые арифметические операции..."
        /// 
        /// // Попытка получить описание несуществующей программы
        /// string error = ProgramsConfig.GetProgramInfo(99);
        /// // Результат: "Программа №99 не описана"
        /// </code>
        /// </example>
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
        /// Отображает в консоли подробную информацию о всех доступных программах.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Метод выполняет:
        /// <list type="number">
        /// <item><description>Очистку консоли и вывод заголовка</description></item>
        /// <item><description>Для каждой программы из <see cref="GetAllPrograms"/>:</description></item>
        /// <item>
        ///     <description>a. Выделение номера и названия с помощью <see cref="ConsoleHelper.ShowKeyValueResult"/></description>
        /// </item>
        /// <item>
        ///     <description>b. Вывод подробного описания на отдельной строке</description>
        /// </item>
        /// <item><description>Разделение программ пустыми строками для читаемости</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Используется для создания справочной страницы с полным описанием функциональности.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Показ полной информации о программах
        /// ProgramsConfig.ShowAllProgramsInfo();
        /// 
        /// // Пример вывода для первой программы:
        /// // Программа 1: Калькулятор
        /// //   выполняет базовые арифметические операции: сложение, вычитание...
        /// </code>
        /// </example>
        /// <seealso cref="GetProgramInfo"/>
        /// <seealso cref="ConsoleHelper.ShowKeyValueResult"/>
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