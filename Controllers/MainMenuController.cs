using EducationalProject.Core;

namespace EducationalProject.Controllers
{
    public class MainMenuController
    {
        private readonly List<BaseController> _controllers;

        public MainMenuController()
        {
            // Все контроллеры в одном месте
            _controllers = new List<BaseController>
            {
                new CalculatorController(),
                // Остальные контроллеры добавим позже
            };
        }

        public void Run()
        {
            ConsoleHelper.ClearAndShowHeader("УЧЕБНЫЙ ПРОЕКТ C#");
            Console.WriteLine(AppConfig.WelcomeMessage);
            ConsoleHelper.WaitForAnyKey();

            while(true)
            {
                ConsoleHelper.ClearAndShowHeader("ГЛАВНОЕ МЕНЮ");
                ShowMenu();

                int choice = InputValidator.GetValidMenuChoice(0, _controllers.Count, ">>> Выберите программу (0 - выход): ");

                if(choice == 0)
                {
                    ShowGoodbye();
                    break;
                }

                // Запускаем выбранный контроллер
                _controllers[choice - 1].Run();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("Доступные программы:");
            Console.WriteLine(new string('=', 30));

            for(int i = 0; i < _controllers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_controllers[i].Name}");
            }

            Console.WriteLine("0. Выход из программы");
            Console.WriteLine(new string('=', 30));
        }

        private void ShowGoodbye()
        {
            ConsoleHelper.ClearAndShowHeader("ДО СВИДАНИЯ!");
            Console.WriteLine(AppConfig.GoodbyeMessage);
            ConsoleHelper.WaitForAnyKey();
        }
    }
}