using EducationalProject.Controllers;
using EducationalProject.Core;
using System.Reflection;

namespace EducationalProject.Factories
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IAppConfigProvider _configApp;
        private readonly IConsoleHelper _consoleHelper;

        public ControllerFactory(IAppConfigProvider configApp, IConsoleHelper consoleHelper)
        {
            _configApp = configApp;
            _consoleHelper = consoleHelper;
        }

        public List<BaseController> CreateControllers()
        {
            var controllers = new List<BaseController>();
            string[] controllerTypes = _configApp.GetControllerTypes();
            string controllersNamespace = _configApp.GetControllersNamespace();

            foreach(string controllerTypeName in controllerTypes)
            {
                try
                {
                    string fullTypeName = $"{controllersNamespace}.{controllerTypeName}";
                    Assembly currentAssembly = Assembly.GetExecutingAssembly();
                    Type controllerType = currentAssembly.GetType(fullTypeName);

                    if(controllerType != null)
                    {
                        BaseController controller = (BaseController)Activator.CreateInstance(controllerType);
                        controllers.Add(controller);

                        // Для отладки (нужно удалить)
                        Console.WriteLine($"✓ Контроллер '{controllerTypeName}' успешно создан");
                    }
                    else
                    {
                        _consoleHelper.ShowError($"Контроллер '{controllerTypeName}' не найден!");
                    }
                }
                catch(Exception ex)
                {
                    _consoleHelper.ShowError($"Ошибка создания контроллера '{controllerTypeName}': {ex.Message}");
                }
            }

            return controllers;
        }
    }
}