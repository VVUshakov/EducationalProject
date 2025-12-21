namespace EducationalProject
{
    // Менеджер меню для управления программами
    public class MenuManager
    {
        #region ===== СВОЙСТВА И КОНСТРУКТОР =====

        // Массив всех доступных программ
        private BaseService[] programs;
        // Значение, соответствующее завершению программы
        const int EXIT = 0;

        // Конструктор класса
        public MenuManager()
        {
            programs = new BaseService[0];
        }

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        // Инициализация программ
        public void Initialize()
        {
            // Программы берутся из конфигурационного файла
            programs = ProgramsConfig.GetAllPrograms();

            ConsoleHelper.ShowInfo($"Загружено программ: {programs.Length}");
            ConsoleHelper.WaitForAnyKey();
            Console.Clear();
        }

        // Запустить выбранную программу
        private void RunProgram(int number)
        {
            Console.Clear();

            string programName = programs[number - 1].Name;
            ConsoleHelper.ShowHeader(programName);
            ConsoleHelper.ShowInfo($"Запускаем: {programName}", "🚀");

            programs[number - 1].Run();
        }

        // Главный цикл программы
        public void Run()
        {
            ShowWelcome();
            Initialize();

            // Бесконечный цикл, пока не выберут выход
            while(true)
            {
                ShowMenu();

                // Получить ввод от пользователя
                int userChoice = GetUserChoice();

                // Проверить выбор "0" - выход
                if(userChoice == EXIT)
                {
                    ShowFinalMessage();
                    break;
                }

                RunProgram(userChoice);
                ConsoleHelper.WaitForAnyKey();
                Console.Clear();
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВЫВОДА ТЕКСТА =====

        // Показать приветствие
        private void ShowWelcome()
        {
            string[] welcomeLines = {
                "ОБУЧАЮЩИЙ ПРОЕКТ C#",
                "Демонстрация ООП подходов"
            };

            ConsoleHelper.ShowHeader(welcomeLines);

            string[] infoLines = {
                "Для школьников 7-11 классов",
                "Автор: [Ваше имя/школа]"
            };

            ConsoleHelper.ShowInfoBlock("Описание проекта", infoLines);
        }

        // Показать прощальное сообщение
        private void ShowFinalMessage()
        {
            string[] goodbyeLines = {
                "ДО СВИДАНИЯ!",
                "",
                "Спасибо за использование программы!",
                "До новых встреч! 👋"
            };

            ConsoleHelper.ShowHeader(goodbyeLines, 40);
        }

        // Показать главное меню
        private void ShowMenu()
        {
            var menuItems = new List<(int, string)>();

            for(int i = 0; i < programs.Length; i++)
            {
                menuItems.Add((i + 1, programs[i].Name));
            }

            menuItems.Add((0, "Выход"));

            ConsoleHelper.ShowMenuWithNumbers("ГЛАВНОЕ МЕНЮ", menuItems);
        }

        #endregion

        #region ===== МЕТОД ПОЛУЧЕНИЯ ВВОДА =====

        // Получить ввод пользователя
        private int GetUserChoice()
        {
            return ConsoleHelper.GetMenuChoice(EXIT, programs.Length, ">>> Введите номер программы: ");
        }

        #endregion
    }
}