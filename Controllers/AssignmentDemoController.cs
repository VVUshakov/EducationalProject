using EducationalProject.Core;
using EducationalProject.Services;
using EducationalProject.Views;

namespace EducationalProject.Controllers
{
    public class AssignmentDemoController : BaseController
    {
        private readonly AssignmentDemoService _service;
        private readonly AssignmentDemoView _view;

        public AssignmentDemoController()
        {
            _service = new AssignmentDemoService();
            _view = new AssignmentDemoView();
        }

        public override string Name => "Демонстрация операторов присваивания";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            // Показать описание программы
            ConsoleHelper.ShowInfoBlock(
                "О ПРОГРАММЕ",
                new string[] {
                    "Эта программа демонстрирует операции присваивания в C#.",
                    "Вы узнаете как использовать:",
                    "• Простые операции: +=, -=, *=, /=, %=",
                    "• Побитовые операции: <<=, >>=",
                    "• Комбинированные операции",
                    "• Практические примеры из жизни"
                }
            );

            // 1. Получить выбор демонстрации
            int choice = _view.GetDemoChoice();

            // 2. Выполнить выбранную демонстрацию
            switch(choice)
            {
                case 1:
                    var basicData = _service.GetBasicOperations();
                    _view.ShowBasicOperations(basicData);
                    break;

                case 2:
                    var combinedData = _service.GetCombinedOperations();
                    _view.ShowCombinedOperations(combinedData);
                    break;

                case 3:
                    var practicalExample = _service.GetPracticalExample();
                    _view.ShowPracticalExample(practicalExample);
                    break;
            }

            // 3. Показать дополнительную информацию
            _view.ShowAssignmentInfo();

            // 4. Показать таблицу примеров
            _view.ShowExamplesTable();

            // 5. Ожидание нажатия клавиши
            ConsoleHelper.WaitForAnyKey();
        }
    }
}