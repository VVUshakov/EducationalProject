using EducationalProject.Controllers;

namespace EducationalProject
{
    class Program
    {
        static void Main()
        {
            // Запускаем главное меню
            var mainMenu = new MainMenuController();
            mainMenu.Run();
        }
    }
}