//// Объявляем пространство имен
//namespace EducationalProject
//{
//    // Класс для управления главным меню
//    public class MenuManager
//    {
//        // Массив для хранения всех программ
//        private BaseService[] programs;

//        // Константа для выхода (значение 0)
//        const int EXIT = 0;

//        // Конструктор - вызывается при создании объекта
//        public MenuManager()
//        {
//            programs = new BaseService[0];  // Создаем пустой массив
//        }

//        // Метод для инициализации (загрузки программ)
//        public void Initialize()
//        {
//            programs = ProgramsConfig.GetAllPrograms();  // Получаем все программы
//            ConsoleHelper.ShowInfo($"Загружено программ: {programs.Length}");  // Показываем количество
//            ConsoleHelper.WaitForAnyKey();  // Ждем нажатия клавиши
//            Console.Clear();  // Очищаем экран
//        }

//        // Главный метод работы менеджера
//        public void Run()
//        {
//            ShowWelcome();  // Показываем приветствие
//            Initialize();   // Загружаем программы

//            // Бесконечный цикл, пока пользователь не выберет выход
//            while(true)
//            {
//                ShowMenu();  // Показываем меню

//                // Получаем выбор пользователя (от 0 до количества программ)
//                int userChoice = InputValidator.GetValidMenuChoice(EXIT, programs.Length, ">>> Введите номер программы: ");

//                // Если выбрали 0 - выходим
//                if(userChoice == EXIT)
//                {
//                    ShowFinalMessage();  // Показываем прощальное сообщение
//                    break;  // Выходим из цикла
//                }

//                // Запускаем выбранную программу
//                RunProgram(userChoice);

//                // Ждем, пока пользователь нажмет клавишу
//                ConsoleHelper.WaitForAnyKey();

//                // Очищаем экран для следующего выбора
//                Console.Clear();
//            }
//        }

//        // Метод для показа приветственного сообщения
//        private void ShowWelcome()
//        {
//            // Показываем заголовок с подзаголовком
//            ConsoleHelper.ShowHeader("ОБУЧАЮЩИЙ ПРОЕКТ C#", "Демонстрация ООП подходов");

//            // Массив строк для информационного блока
//            string[] infoLines = {
//                "Для школьников 7-11 классов",
//                "Используйте меню для навигации",
//                "Нажмите 0 для выхода из программы"
//            };

//            // Показываем информационный блок
//            ConsoleHelper.ShowInfoBlock("Описание проекта", infoLines);
//        }

//        // Метод для показа прощального сообщения
//        private void ShowFinalMessage()
//        {
//            // Показываем заголовок прощания
//            ConsoleHelper.ShowHeader("ДО СВИДАНИЯ!", "Спасибо за использование программы!");

//            // Дополнительное сообщение с эмодзи
//            Console.WriteLine("\nДо новых встреч! 👋");
//        }

//        // Метод для показа главного меню
//        private void ShowMenu()
//        {
//            // Рисуем заголовок меню
//            Console.WriteLine("\n════════════════ ГЛАВНОЕ МЕНЮ ════════════════");

//            // Перебираем все программы и выводим их с номерами
//            for(int i = 0; i < programs.Length; i++)  // i идет от 0 до количества программ-1
//            {
//                // Выводим номер (i+1, так как люди считают с 1) и название программы
//                Console.WriteLine($"{i + 1}. {programs[i].Name}");
//            }

//            // Выводим пункт "Выход"
//            Console.WriteLine("0. Выход");
//        }

//        // Метод для запуска программы
//        private void RunProgram(int number)
//        {
//            Console.Clear();  // Очищаем экран

//            // Получаем название программы (number-1, потому что массив начинается с 0)
//            string programName = programs[number - 1].Name;

//            // Показываем заголовок с названием программы
//            ConsoleHelper.ShowHeader(programName);

//            // Сообщаем о запуске
//            ConsoleHelper.ShowInfo($"Запускаем: {programName}");

//            // Запускаем программу (вызываем метод Run у выбранной программы)
//            programs[number - 1].Run();
//        }
//    }
//}