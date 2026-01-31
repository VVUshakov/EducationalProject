using EducationalProject.Core.Interfaces;
using EducationalProject.Core.Services;

namespace EducationalProject.Core.Controllers
{
    public class MainController : IController
    {
        private readonly IView _view;
        private readonly List<BaseProgram> _programs;
        private bool _isRunning;

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

            for(int i = 0; i < _programs.Count; i++)
            {
                _view.ShowMessage($"{i + 1}. {_programs[i].Name}");
            }

            _view.ShowMessage($"{_programs.Count + 1}. Выход");
        }

        private void ProcessChoice()
        {
            string input = _view.ReadInput("Ваш выбор: ");

            if(int.TryParse(input, out int choice))
            {
                if(choice >= 1 && choice <= _programs.Count)
                {
                    // Запуск выбранной программы
                    var program = _programs[choice - 1];
                    program.Execute();
                    _view.WaitForAnyKey();
                }
                else if(choice == _programs.Count + 1)
                {
                    _isRunning = false;
                    _view.ShowMessage("До свидания!");
                }
                else
                {
                    _view.ShowError("Неверный выбор!");
                }
            }
            else
            {
                _view.ShowError("Введите число!");
            }
        }
    }
}
