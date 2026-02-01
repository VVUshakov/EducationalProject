using EducationalProject.Core.Interfaces;
using EducationalProject.Core.Services;

namespace EducationalProject.Core.Controllers
{
    public class MainController : IController
    {
        private readonly List<IView> _views; // Список представлений (Консоль, запись в файл и т.п.)
        private IView _currentView; // Текущее активное представление
        private readonly List<BaseProgram> _programs; // Список программ
        private bool _isRunning; // флаг управления основным циклом программы (Run Loop)
        private const int FIRST_MENU_ITEM = 1; // Номер первого элемента Меню

        public MainController(List<IView> views, IView currentView, List<BaseProgram> programs)
        {
            _views = views;
            _programs = programs;
            _currentView = currentView;
        }

        public void Run()
        {
            _isRunning = true;

            while(_isRunning)
            {
                ShowMainMenu();
                ProcessChoice();
            }
        }

        private void ShowMainMenu()
        {
            // Очищаем все представления
            foreach(IView view in _views)
            {
                view.Clear();
            }

            // Выводим меню во все представления
            foreach(IView view in _views)
            {
                view.ShowMessage("=== ГЛАВНОЕ МЕНЮ ===");
                view.ShowMessage("Выберите программу для запуска:");
            }

            // Выводим список программ во все представления
            for(int i = 0; i < _programs.Count; i++)
            {
                string menuItem = $"{i + FIRST_MENU_ITEM}. {_programs[i].Name}";
                foreach(IView view in _views)
                {
                    view.ShowMessage(menuItem);
                }
            }

            // Выводим пункт выхода во все представления
            string exitItem = $"{_programs.Count + FIRST_MENU_ITEM}. Выход";
            foreach(IView view in _views)
            {
                view.ShowMessage(exitItem);
            }
        }

        private void ProcessChoice()
        {
            // Получаем ввод только из текущего (активного/главного) Представления
            // (остальные представления могут быть только для вывода)
            string input = _currentView.ReadInput("Ваш выбор: ");

            if(!int.TryParse(input, out int choice))
            {
                // Показываем ошибку во всех представлениях
                foreach(IView view in _views)
                {
                    view.ShowError("Введите число!");
                }
                return;
            }

            if(choice == _programs.Count + FIRST_MENU_ITEM)
            {
                _isRunning = false;
                // Показываем сообщение о выходе во всех представлениях
                foreach(IView view in _views)
                {
                    view.ShowMessage("До свидания!");
                }
                return;
            }

            if(choice >= FIRST_MENU_ITEM && choice <= _programs.Count)
            {
                _programs[choice - FIRST_MENU_ITEM].Run();

                // Ждем клавишу во всех представлениях
                foreach(IView view in _views)
                {
                    view.WaitForAnyKey();
                }
                return;
            }

            // Показываем ошибку неверного выбора во всех представлениях
            foreach(IView view in _views)
            {
                view.ShowError("Неверный выбор!");
            }
        }
    }
}