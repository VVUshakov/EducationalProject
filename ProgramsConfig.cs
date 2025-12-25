using EducationalProject.Services;

namespace EducationalProject
{
    public static class ProgramsConfig
    {
        public static BaseService[] GetAllPrograms()
        {
            return new BaseService[]
            {
                new Calculator(),
                new BinaryConverter(),
                new NameEncoder(),
                new AssignmentDemo(),
            };
        }

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

        public static void ShowAllProgramsInfo()
        {
            ConsoleHelper.ClearAndShowHeader("ПОДРОБНАЯ ИНФОРМАЦИЯ О ПРОГРАММАХ");

            var programs = GetAllPrograms();
            string[] descriptions = {
                "выполняет базовые арифметические операции: сложение, вычитание, умножение, деление и нахождение остатка",
                "переводит числа между десятичной и двоичной системами (8-битные числа)",
                "преобразует имя в разные коды: алфавитные позиции, азбуку Морзе, простой шифр",
                "показывает как работают составные операторы: +=, -=, *=, /=, %=, <<=, >>="
            };

            for(int i = 0; i < programs.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Программа {i + 1}: ");
                Console.ResetColor();
                Console.WriteLine(programs[i].Name);
                Console.WriteLine($"  {descriptions[i]}");
                Console.WriteLine();
            }
        }
    }
}