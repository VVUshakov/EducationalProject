// Подключаем пространство имен для доступа к классам программ
using EducationalProject.Services;

// Объявляем пространство имен
namespace EducationalProject
{
    // Статический класс для хранения списка всех программ
    public static class ProgramsConfig
    {
        const int USER_FRIENDLY_START_NUMBER = 1;       // Пользователи считают с 1, не с 0

        // Метод, который возвращает массив всех доступных программ
        public static BaseService[] GetAllPrograms()
        {
            // Создаем массив с программами
            BaseService[] baseService =
            [
                new Calculator(),       // Создаем объект Калькулятора (№1 в меню)
                new BinaryConverter(),  // Создаем объект Конвертера (№2 в меню)
                new NameEncoder(),      // Создаем объект Кодировщика (№3 в меню)
                new AssignmentDemo(),   // Создаем объект Демонстрации (№4 в меню)
                new PasswordGenerator(),// Создаем объект Генератор паролей (№5 в меню)
            ];

            return baseService; // Возвращаем массив с программами
        }

        //// Метод для получения описания всех программ
        //public static void ShowAllProgramsInfo()
        //{
        //    ConsoleHelper.ClearAndShowHeader("ПОДРОБНАЯ ИНФОРМАЦИЯ О ПРОГРАММАХ");

        //    int programNumber = USER_FRIENDLY_START_NUMBER; // Переменная для номера программы
        //    BaseService[] allPrograms = GetAllPrograms(); // Получаем массив всех программ

        //    // Перебираем все программы в массиве
        //    foreach(BaseService program in allPrograms)
        //    {
        //        string programDescription = GetDescriptionForProgram(program); // Получаем описание для текущей программы
        //        DisplayProgramInfo(programNumber, program.Name, programDescription); // Выводим информацию о программе
        //        programNumber++; // Увеличиваем номер для следующей программы
        //    }
        //}

        //// Метод для получения всех программ с их описаниями (упрощенный)
        //public static void DisplayAllProgramsWithDescriptions()
        //{
        //    int programNumber = USER_FRIENDLY_START_NUMBER; // Переменная для номера программы
        //    BaseService[] allPrograms = GetAllPrograms(); // Получаем массив всех программ

        //    foreach(BaseService program in allPrograms)
        //    {
        //        string description = GetDescriptionForProgram(program); // Получить описание для конкретной программы
        //        Console.WriteLine($"{programNumber}. {program.Name}: {description}");
        //        programNumber++;
        //    }
        //}

        //// Метод для получения описания для конкретной программы
        //// Этот метод НЕ ЗАВИСИТ от порядка программ в массиве
        //private static string GetDescriptionForProgram(BaseService program)
        //{
        //    // Определяем тип программы и возвращаем соответствующее описание
        //    // (это безопаснее, чем полагаться на индексы, которые могут меняться
        //    // в зависимости от того куда в коллекции вставится новая программа)

        //    // Проверяем тип программы с помощью оператора switch и pattern matching
        //    return program switch
        //    {
        //        Calculator _ => "выполняет базовые арифметические операции: сложение, вычитание, умножение, деление и нахождение остатка",
        //        BinaryConverter _ => "переводит числа между десятичной и двоичной системами (8-битные числа)",
        //        NameEncoder _ => "преобразует имя в разные коды: алфавитные позиции, азбуку Морзе, простой шифр",
        //        AssignmentDemo _ => "показывает как работают составные операторы: +=, -=, *=, /=, %=, <<=, >>=",
        //        PasswordGenerator _ => "создает безопасные пароли разной сложности с настройкой длины и символов",
        //        _ => "описание программы отсутствует"  // На случай, если добавили новую программу и забыли описание
        //    };
        //}

        //// Вспомогательный метод для отображения информации о программе
        //private static void DisplayProgramInfo(int programNumber, string programName, string programDescription)
        //{
        //    Console.ForegroundColor = ConsoleColor.Yellow; // Желтый цвет для отображения номера программы
        //    Console.Write($"Программа {programNumber}: ");
        //    Console.ResetColor(); // Вернуть стандартный цвет текста

        //    Console.WriteLine(programName); // Название программы                        
        //    Console.WriteLine($"  {programDescription}"); // Описание с отступом                        
        //    Console.WriteLine(); // Пустая строка для разделения
        //}
    }
}