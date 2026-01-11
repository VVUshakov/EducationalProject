// EducationalProject/Program.cs (обновленная версия)
using EducationalProject.Controllers;
using EducationalProject.Core;
using EducationalProject.Factories;

namespace EducationalProject
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Создаем необходимые зависимости
                IConsoleHelper consoleHelper = new ConsoleHelper();
                IAppConfigProvider configApp = new AppConfigProvider();
                IControllerFactory factory = new ControllerFactory(configApp, consoleHelper);

                // Создаем главное меню с внедренными зависимостями
                var mainMenu = new MainMenuController(factory, consoleHelper, configApp);
                mainMenu.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }
    }
}