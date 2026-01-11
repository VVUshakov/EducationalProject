using EducationalProject.Core;
using EducationalProject.Factories;

namespace EducationalProject.Controllers
{
    public class MainMenuController
    {
        private readonly List<BaseController> _controllers;
        private readonly IConsoleHelper _consoleHelper;
        private readonly IAppConfigProvider _configApp;

        public MainMenuController(
            IControllerFactory factory,
            IConsoleHelper consoleHelper,
            IAppConfigProvider configProvider)
        {
            _controllers = factory.CreateControllers();
            _consoleHelper = consoleHelper;
            _configApp = configProvider;
        }

        public void Run()
        {
            ShowWelcome();

            while(true)
            {
                ShowMainMenu();

                int choice = InputValidator.GetValidMenuChoice(
                    minValue: 0,
                    maxValue: _controllers.Count,
                    prompt: $">>> Выберите программу (0 - выход): "
                );

                if(choice == 0)
                {
                    ShowGoodbye();
                }
                else
                {
                    // Запускаем выбранный контроллер
                    RunController(choice - 1);
                }
            }
        }

        private void ShowWelcome()
        {
            _consoleHelper.ClearAndShowHeader(
                "УЧЕБНЫЙ ПРОЕКТ C#",
                "Для школьников 7-9 классов"
            );

            Console.WriteLine("\nДобро пожаловать в учебный проект!");
            Console.WriteLine("Здесь вы можете изучить основы программирования.");
            Console.WriteLine("\nПроект включает 5 учебных программ:");

            for(int i = 0; i < _controllers.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {_controllers[i].Name}");
            }

            _consoleHelper.WaitForAnyKey();
        }

        private void ShowMainMenu()
        {
            _consoleHelper.ClearAndShowHeader("ГЛАВНОЕ МЕНЮ");

            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("ВЫБЕРИТЕ ПРОГРАММУ:");
            Console.WriteLine(new string('=', 40));

            for(int i = 0; i < _controllers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_controllers[i].Name}");
            }

            Console.WriteLine("0. Выход из программы");
            Console.WriteLine(new string('=', 40));
        }

        private void RunController(int index)
        {
            try
            {
                Console.Clear();
                _controllers[index].Run();
            }
            catch(Exception ex)
            {
                _consoleHelper.ShowError($"Ошибка: {ex.Message}");
                _consoleHelper.WaitForAnyKey();
            }
        }

        private void ShowGoodbye()
        {
            _consoleHelper.ClearAndShowHeader("ДО СВИДАНИЯ!");

            Console.WriteLine("\nСпасибо за использование учебного проекта!");
            Console.WriteLine("Надеемся, вы узнали что-то новое!");
            Console.WriteLine("\nУдачи в изучении программирования! 🚀");

            _consoleHelper.WaitForAnyKey("Нажмите любую клавишу для выхода...");
        }
    }
}