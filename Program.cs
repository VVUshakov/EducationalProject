using EducationalProject.Controllers;

namespace EducationalProject
{
    class Program
    {
        static void Main()
        {
            try
            {
                // Запускаем главное меню
                var mainMenu = new MainMenuController();
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