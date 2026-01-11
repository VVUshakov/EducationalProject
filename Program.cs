using EducationalProject.Controllers;
using EducationalProject.Core;

namespace EducationalProject
{
    class Program
    {
        static void Main()
        {
            // Показываем приветствие
            ConsoleHelper.ClearAndShowHeader("УЧЕБНЫЙ ПРОЕКТ C#");
            Console.WriteLine(AppConfig.WelcomeMessage);
            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для продолжения...");

            // Создаем и запускаем контроллер калькулятора
            var calculatorController = new CalculatorController();
            calculatorController.Run();

            // Прощание
            ConsoleHelper.ClearAndShowHeader("СПАСИБО!");
            Console.WriteLine(AppConfig.GoodbyeMessage);
            ConsoleHelper.WaitForAnyKey();
        }
    }
}