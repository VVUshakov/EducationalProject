using EducationalProject.Core;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class CalculatorController : BaseController
    {
        private readonly CalculatorService _service;
        private readonly CalculatorView _view;

        public CalculatorController()
        {
            _service = new CalculatorService();
            _view = new CalculatorView();
        }

        public override string Name => AppConfig.Calculator.Name;

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            // 1. Получить данные от пользователя через View
            var inputData = _view.GetInput();

            // 2. Выполнить вычисления через Service
            var resultData = _service.Calculate(inputData);

            // 3. Показать результат через View
            _view.ShowResult(resultData);

            // 4. Ожидание нажатия клавиши
            ConsoleHelper.WaitForAnyKey();
        }
    }

    // Базовый класс контроллера
    public abstract class BaseController
    {
        public abstract string Name { get; }
        public abstract void Run();
    }
}