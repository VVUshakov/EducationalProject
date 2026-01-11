using EducationalProject.Core;
using System.Reflection;

namespace EducationalProject.Controllers
{
    public class MainMenuController
    {
        private readonly List<BaseController> _controllers;

        public MainMenuController()
        {
            // Автоматически создаем все контроллеры из конфигурации
            _controllers = CreateControllersFromConfig();
        }

        private List<BaseController> CreateControllersFromConfig()
        {
            // Создаем массив для формирования списка всех контроллеров
            var controllers = new List<BaseController>();

            // Получаем перечень названий всех контроллеров из файла конфигурации
            string[] controllerTypes = AppConfig.MenuConfig.ControllerTypes;

            foreach(string controllerTypeName in controllerTypes)
            {
                try
                {
                    // Формируем полное имя типа
                    string controllersNamespace = AppConfig.MenuConfig.ControllersNamespace;
                    string fullTypeName = $"{controllersNamespace}.{controllerTypeName}";

                    // Получаем текущую сборку (где находятся контроллеры)
                    Assembly currentAssembly = Assembly.GetExecutingAssembly();

                    // Ищем тип в сборке
                    Type controllerType = currentAssembly.GetType(fullTypeName);

                    if(controllerType != null)
                    {
                        // Создаем экземпляр контроллера
                        BaseController controller = (BaseController)Activator.CreateInstance(controllerType);
                        controllers.Add(controller);
                    }
                    else
                    {
                        ConsoleHelper.ShowError($"Контроллер '{controllerTypeName}' не найден!");
                    }
                }
                catch(Exception ex)
                {
                    ConsoleHelper.ShowError($"Ошибка создания контроллера '{controllerTypeName}': {ex.Message}");
                }
            }

            return controllers;
        }

        public void Run()
        {
            ShowWelcome();

            while(true)
            {
                ShowMainMenu();

                int choice = InputValidator.GetValidMenuChoice(
                    0, _controllers.Count,
                    $">>> Выберите программу (0 - выход): ");

                if(choice == 0)
                {
                    ShowGoodbye();
                    break;
                }

                // Запускаем выбранный контроллер
                RunController(choice - 1);
            }
        }

        private void ShowWelcome()
        {
            ConsoleHelper.ClearAndShowHeader(
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

            ConsoleHelper.WaitForAnyKey();
        }

        private void ShowMainMenu()
        {
            ConsoleHelper.ClearAndShowHeader("ГЛАВНОЕ МЕНЮ");

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
                ConsoleHelper.ShowError($"Ошибка: {ex.Message}");
                ConsoleHelper.WaitForAnyKey();
            }
        }

        private void ShowGoodbye()
        {
            ConsoleHelper.ClearAndShowHeader("ДО СВИДАНИЯ!");

            Console.WriteLine("\nСпасибо за использование учебного проекта!");
            Console.WriteLine("Надеемся, вы узнали что-то новое!");
            Console.WriteLine("\nУдачи в изучении программирования! 🚀");

            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для выхода...");
        }
    }
}