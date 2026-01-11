using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class NameEncoderView
    {
        public string GetNameInput()
        {
            ConsoleHelper.ShowInfoBlock(
                "ВВОД ИМЕНИ",
                new string[] {
                    "Введите имя или текст для кодирования.",
                    "Можно использовать русские и английские буквы.",
                    "Длина: от 1 до 50 символов."
                }
            );

            return InputValidator.GetValidText(
                "Введите имя: ",
                minLength: 1,
                maxLength: 50
            );
        }

        public int GetEncodingMethod()
        {
            ConsoleHelper.ShowMenu(
                "Выберите метод кодирования",
                new string[] {
                    "Алфавитный код (А=1, Б=2...)",
                    "Азбука Морзе",
                    "Числовой шифр (позиция × 3)"
                }
            );

            return InputValidator.GetValidMenuChoice(1, 3, ">>> Выберите метод (1-3): ");
        }

        public void ShowResult(EncodingResult result)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("РЕЗУЛЬТАТ КОДИРОВАНИЯ");
            Console.WriteLine(new string('-', 50));

            Console.WriteLine($"Исходное имя: {result.OriginalName}");
            Console.WriteLine($"Метод кодирования: {result.MethodName}");
            Console.WriteLine(new string('-', 20));

            if(!result.IsValid)
            {
                ConsoleHelper.ShowError("Ошибка кодирования!");
                return;
            }

            Console.WriteLine("Закодированное имя:");
            Console.ForegroundColor = ConsoleColor.Yellow;

            if(result.MethodChoice == 2) // Морзе - особая обработка
            {
                string[] morseParts = result.EncodedName.Split(' ');
                foreach(string part in morseParts)
                {
                    if(part == "/")
                        Console.Write(" / ");
                    else
                        Console.Write($"{part} ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(result.EncodedName);
            }

            Console.ResetColor();

            Console.WriteLine(new string('-', 50));

            // Показать примеры
            ShowExamples(result.MethodChoice);

            Console.WriteLine(new string('=', 50));
        }

        private void ShowExamples(int methodChoice)
        {
            Console.WriteLine("\nПРИМЕРЫ:");

            switch(methodChoice)
            {
                case 1:
                    Console.WriteLine("АЛЕКСЕЙ → 01-12-06-11-19-06-11");
                    Console.WriteLine("ИВАН → 10-03-01-15");
                    Console.WriteLine("MARIA → 13-01-18-09-01");
                    break;

                case 2:
                    Console.WriteLine("А → .-    Б → -...   В → .--");
                    Console.WriteLine("ИВАН → .. ...- .- -.");
                    Console.WriteLine("ПРОСТРАНСТВО → / (пробел)");
                    break;

                case 3:
                    Console.WriteLine("А → 003 (1×3)    Б → 006 (2×3)");
                    Console.WriteLine("ИВАН → 030 009 003 045");
                    Console.WriteLine("МАША → 039 003 057 003");
                    break;
            }
        }

        public void ShowEncodingInfo()
        {
            Console.WriteLine("\n" + new string('*', 40));
            Console.WriteLine("ИНФОРМАЦИЯ О МЕТОДАХ КОДИРОВАНИЯ:");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. Алфавитный код: А=01, Б=02, ..., Я=33");
            Console.WriteLine("2. Азбука Морзе: точки и тире");
            Console.WriteLine("3. Числовой шифр: позиция × 3");
            Console.WriteLine(new string('*', 40));
        }
    }
}