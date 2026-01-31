using EducationalProject.Core.Interfaces;
using EducationalProject.Core.Services;

namespace EducationalProject.Core.Controllers
{
    public class MainController : IController
    {
        private readonly IView _view;
        private readonly List<BaseProgram> _programs;
        private bool _isRunning; // флаг управления основным циклом программы (Run Loop)

        // Константы для числовых значений
        private const int ExitMenuOffset = 1;
        private const int MinChoiceValue = 1;
        private const int FirstProgramIndex = 0;
        private const int MenuItemNumberOffset = 1;

        public MainController(IView view, List<BaseProgram> programs)
        {
            _view = view;
            _programs = programs;
            _isRunning = true;
        }

        public void Run()
        {
            while(_isRunning)
            {
                ShowMainMenu();
                ProcessChoice();
            }
        }

        private void ShowMainMenu()
        {
            _view.Clear();
            _view.ShowMessage("=== ГЛАВНОЕ МЕНЮ ===");
            _view.ShowMessage("Выберите программу для запуска:");

            for(int i = FirstProgramIndex; i < _programs.Count; i++)
            {
                _view.ShowMessage($"{i + MenuItemNumberOffset}. {_programs[i].Name}");
            }

            _view.ShowMessage($"{_programs.Count + ExitMenuOffset}. Выход");
        }

        private void ProcessChoice()
        {
            string input = _view.ReadInput("Ваш выбор: ");

            if(!int.TryParse(input, out int choice))
            {
                _view.ShowError("Введите число!");
                return;
            }

            if(IsValidProgramChoice(choice))
            {
                // Запуск выбранной программы
                BaseProgram program = _programs[choice - MenuItemNumberOffset];
                program.Run();
                _view.WaitForAnyKey();
                return;
            }

            if(IsExitChoice(choice))
            {
                _isRunning = false;
                _view.ShowMessage("До свидания!");
                return;
            }

            _view.ShowError("Неверный выбор!");
        }

        private bool IsValidProgramChoice(int choice)
        {
            return choice >= MinChoiceValue && choice <= _programs.Count;
        }

        private bool IsExitChoice(int choice)
        {
            return choice == _programs.Count + ExitMenuOffset;
        }
    }
}