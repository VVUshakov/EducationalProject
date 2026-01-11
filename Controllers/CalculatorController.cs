using EducationalProject.Core;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class CalculatorController : BaseController
    {
        private readonly CalculatorService _service;
        private readonly CalculatorView _view;
        public override string Name => AppConfig.CalculatorConfig.Name;

        // Конструктор по умолчанию для обратной совместимости
        public CalculatorController() : this(new ConsoleHelper()) { }

        // Конструктор с внедрением зависимости
        public CalculatorController(IConsoleHelper consoleHelper) : base(consoleHelper)
        {
            _service = new CalculatorService();
            _view = new CalculatorView(consoleHelper);
        }

        public override void Run()
        {
            ShowHeader(); // Используем метод из базового класса

            // 1. Получить данные от пользователя через View
            var inputData = _view.GetInput();

            // 2. Выполнить вычисления через Service
            var resultData = _service.Calculate(inputData);

            // 3. Показать результат через View
            _view.ShowResult(resultData);

            // 4. Ожидание нажатия клавиши
            WaitForAnyKey(); // Используем метод из базового класса
        }
    }
}