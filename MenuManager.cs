namespace EducationalProject
{
    public class MenuManager
    {
        private BaseService[] programs;
        const int EXIT = 0;

        public MenuManager()
        {
            programs = new BaseService[0];
        }

        public void Initialize()
        {
            programs = ProgramsConfig.GetAllPrograms();
            ConsoleHelper.ShowInfo($"Загружено программ: {programs.Length}");
            ConsoleHelper.WaitForAnyKey();
            Console.Clear();
        }

        public void Run()
        {
            ShowWelcome();
            Initialize();

            while(true)
            {
                ShowMenu();
                int userChoice = InputValidator.GetValidMenuChoice(EXIT, programs.Length, ">>> Введите номер программы: ");

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

        private void ShowWelcome()
        {
            ConsoleHelper.ShowHeader("ОБУЧАЮЩИЙ ПРОЕКТ C#", "Демонстрация ООП подходов");

            string[] infoLines = {
                "Для школьников 7-11 классов",
                "Используйте меню для навигации",
                "Нажмите 0 для выхода из программы"
            };

            ConsoleHelper.ShowInfoBlock("Описание проекта", infoLines);
        }

        private void ShowFinalMessage()
        {
            // Создаем простой прощальный заголовок
            ConsoleHelper.ShowHeader("ДО СВИДАНИЯ!", "Спасибо за использование программы!");

            // Выводим дополнительное сообщение
            Console.WriteLine("\nДо новых встреч! 👋");
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n════════════════ ГЛАВНОЕ МЕНЮ ════════════════");

            for(int i = 0; i < programs.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {programs[i].Name}");
            }

            Console.WriteLine("0. Выход");
        }

        private void RunProgram(int number)
        {
            Console.Clear();

            string programName = programs[number - 1].Name;
            ConsoleHelper.ShowHeader(programName);
            ConsoleHelper.ShowInfo($"Запускаем: {programName}");

            programs[number - 1].Run();
        }
    }
}