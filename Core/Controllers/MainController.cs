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
            ClearAllViews();

            // Выводим меню во все представления
            PutMenuInViews();

            // Выводим список программ во все представления
            PutListProgramsInViews();

            // Выводим пункт выхода во все представления
            PutExitPointInViews();
        }

        private void ProcessChoice()
        {
            // Получаем ввод только из текущего (активного/главного) Представления
            // (остальные представления могут быть только для вывода)
            string input = _currentView.ReadInput("Ваш выбор: ");

            // Если ввод не число
            if(!int.TryParse(input, out int choice))
            {
                // Выводим сообщение об ошибке во всех представлениях
                PutErrorInViews();
                return;
            }

            // Если ввод равен пункту "Выход"
            if(choice == _programs.Count + FIRST_MENU_ITEM)
            {
                _isRunning = false; // Устанавливаем флаг завершения возврата в меню

                // Выводим сообщение о выходе во всех представлениях
                PutExitMessageInViews();
                return;
            }

            if(choice >= FIRST_MENU_ITEM && choice <= _programs.Count)
            {
                _programs[choice - FIRST_MENU_ITEM].Run();

                // Ждем клавишу во всех представлениях
                WaitKeyInViews();
                return;
            }

            // Показываем ошибку неверного выбора во всех представлениях
            PutInvalidSelectionErrorInViews();
        }

        #region // === ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ===

        // Очистить все представления
        private void ClearAllViews()
        {
            foreach(IView view in _views)
            {
                view.Clear();
            }
        }

        // Разместить меню в представлениях
        private void PutMenuInViews()
        {
            foreach(IView view in _views)
            {
                view.ShowMessage("=== ГЛАВНОЕ МЕНЮ ===");
                view.ShowMessage("Выберите программу для запуска:");
            }
        }

        // Разместить список программ в представлениях
        private void PutListProgramsInViews()
        {
            for(int i = 0; i < _programs.Count; i++)
            {
                string menuItem = $"{i + FIRST_MENU_ITEM}. {_programs[i].Name}";

                foreach(IView view in _views)
                {
                    view.ShowMessage(menuItem);
                }
            }
        }

        // Разместить точку выхода в представлениях
        private void PutExitPointInViews()
        {
            string exitItem = $"{_programs.Count + FIRST_MENU_ITEM}. Выход";
            foreach(IView view in _views)
            {
                view.ShowMessage(exitItem);
            }
        }

        // Вывести сообщение об ошибке в представлениях
        private void PutErrorInViews()
        {
            foreach(IView view in _views)
            {
                view.ShowError("Введите число!");
            }
        }

        // Добавить сообщение о завершении работы в представления
        private void PutExitMessageInViews()
        {
            foreach(IView view in _views)
            {
                view.ShowMessage("До свидания!");
            }
        }

        // Ожидание ввода клавиши в представлениях
        private void WaitKeyInViews()
        {
            foreach(IView view in _views)
            {
                view.WaitForAnyKey();
            }
        }

        // Вывести сообщение об ошибке "Неверный выбор" в представлениях
        private void PutInvalidSelectionErrorInViews()
        {
            foreach(IView view in _views)
            {
                view.ShowError("Неверный выбор!");
            }
        }

        #endregion
    }
}