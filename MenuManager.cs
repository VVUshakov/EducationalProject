namespace EducationalProject
{
    /// <summary>
    /// Менеджер меню для управления образовательными программами.
    /// Координирует взаимодействие пользователя с коллекцией программ через консольный интерфейс.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс реализует паттерн "Управляющий" (Controller) и предоставляет:
    /// <list type="bullet">
    /// <item><description>Инициализацию и загрузку доступных программ</description></item>
    /// <item><description>Отображение главного меню навигации</description></item>
    /// <item><description>Обработку пользовательского выбора</description></item>
    /// <item><description>Управление жизненным циклом программ</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Работает в связке с:
    /// <list type="bullet">
    /// <item><description><see cref="ProgramsConfig"/> - загрузка конфигурации</description></item>
    /// <item><description><see cref="BaseService"/> - базовый класс для всех программ</description></item>
    /// <item><description><see cref="InputValidator"/> - проверка ввода пользователя</description></item>
    /// <item><description><see cref="ConsoleHelper"/> - форматированный вывод в консоль</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример использования менеджера меню:
    /// <code>
    /// // Создание и запуск менеджера
    /// var menuManager = new MenuManager();
    /// menuManager.Run();
    /// 
    /// // В результате пользователь увидит:
    /// // 1. Приветственное сообщение
    /// // 2. Загрузку программ
    /// // 3. Главное меню с нумерованным списком
    /// // 4. Возможность выбора программы или выхода
    /// </code>
    /// </example>
    public class MenuManager
    {
        #region ===== СВОЙСТВА И КОНСТРУКТОР =====

        /// <summary>
        /// Массив доступных образовательных программ.
        /// </summary>
        /// <remarks>
        /// Каждый элемент массива является экземпляром класса, унаследованного от <see cref="BaseService"/>.
        /// Программы загружаются при инициализации через <see cref="ProgramsConfig.GetAllPrograms"/>.
        /// </remarks>
        private BaseService[] programs;

        /// <summary>
        /// Константа, представляющая значение для выхода из программы.
        /// </summary>
        /// <remarks>
        /// Используется в меню как пункт "0 - Выход".
        /// Значение сравнивается с пользовательским вводом в методе <see cref="Run"/>.
        /// </remarks>
        const int EXIT = 0;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MenuManager"/>.
        /// </summary>
        /// <remarks>
        /// Конструктор создает пустой массив программ. 
        /// Фактическая загрузка программ выполняется позднее в методе <see cref="Initialize"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Создание менеджера меню
        /// var manager = new MenuManager();
        /// // На этом этапе массив programs пуст
        /// // Программы будут загружены при вызове manager.Initialize()
        /// </code>
        /// </example>
        public MenuManager()
        {
            programs = new BaseService[0];
        }

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Инициализирует менеджер, загружая доступные программы из конфигурации.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Метод выполняет:
        /// <list type="number">
        /// <item><description>Загрузку программ через <see cref="ProgramsConfig.GetAllPrograms"/></description></item>
        /// <item><description>Отображение информации о количестве загруженных программ</description></item>
        /// <item><description>Паузу для ознакомления пользователя с информацией</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Этот метод должен быть вызван перед использованием меню. 
        /// Обычно вызывается автоматически из метода <see cref="Run"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Ручная инициализация (обычно не требуется)
        /// menuManager.Initialize();
        /// // В консоль будет выведено: "Загружено программ: X"
        /// </code>
        /// </example>
        public void Initialize()
        {
            // Программы берутся из конфигурационного файла
            programs = ProgramsConfig.GetAllPrograms();

            ConsoleHelper.ShowInfo($"Загружено программ: {programs.Length}");
            ConsoleHelper.WaitForAnyKey();
            Console.Clear();
        }

        /// <summary>
        /// Запускает выбранную образовательную программу.
        /// </summary>
        /// <param name="number">Номер программы в меню (начинается с 1).</param>
        /// <remarks>
        /// <para>
        /// Метод выполняет:
        /// <list type="number">
        /// <item><description>Очистку консоли</description></item>
        /// <item><description>Получение имени программы по индексу</description></item>
        /// <item><description>Отображение заголовка с названием программы</description></item>
        /// <item><description>Запуск программы через метод <see cref="BaseService.Run"/></description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Важно: номер программы должен быть в диапазоне от 1 до количества программ.
        /// Преобразование индекса: пользовательский номер (начиная с 1) → индекс массива (начиная с 0).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Запуск первой программы из меню
        /// RunProgram(1);
        /// // Будет выполнен поиск programs[0] и вызов programs[0].Run()
        /// </code>
        /// </example>
        /// <exception cref="IndexOutOfRangeException">
        /// Выбрасывается, если <paramref name="number"/> меньше 1 или больше длины массива programs.
        /// </exception>
        private void RunProgram(int number)
        {
            Console.Clear();

            string programName = programs[number - 1].Name;
            ConsoleHelper.ShowHeader(programName);
            ConsoleHelper.ShowInfo($"Запускаем: {programName}", "🚀");

            programs[number - 1].Run();
        }

        /// <summary>
        /// Главный цикл выполнения менеджера меню.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Метод реализует основной цикл работы приложения:
        /// <list type="number">
        /// <item><description>Показ приветственного сообщения (<see cref="ShowWelcome"/>)</description></item>
        /// <item><description>Инициализацию программ (<see cref="Initialize"/>)</description></item>
        /// <item><description>Бесконечный цикл, пока пользователь не выберет выход:</description></item>
        /// <item>
        ///     <description>a. Показ главного меню (<see cref="ShowMenu"/>)</description>
        /// </item>
        /// <item>
        ///     <description>b. Получение валидного выбора пользователя</description>
        /// </item>
        /// <item>
        ///     <description>c. Проверка выхода (0) или запуск программы (<see cref="RunProgram"/>)</description>
        /// </item>
        /// <item><description>Показ прощального сообщения при выходе</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Цикл продолжается до тех пор, пока пользователь не введет 0.
        /// После каждой выполненной программы очищается консоль для подготовки к следующему выбору.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Запуск менеджера (обычно из Program.Main())
        /// MenuManager manager = new MenuManager();
        /// manager.Run();
        /// 
        /// // После запуска пользователь взаимодействует с консольным меню
        /// // Цикл продолжается до выбора "0 - Выход"
        /// </code>
        /// </example>
        /// <seealso cref="InputValidator.GetValidMenuChoice"/>
        public void Run()
        {
            ShowWelcome();
            Initialize();

            // Бесконечный цикл, пока не выберут выход
            while(true)
            {
                ShowMenu();

                // Получить валидный ввод от пользователя
                int userChoice = InputValidator.GetValidMenuChoice(EXIT, programs.Length, ">>> Введите номер программы: ");

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

        /// <summary>
        /// Отображает приветственное сообщение и информацию о проекте.
        /// </summary>
        /// <remarks>
        /// Метод использует <see cref="ConsoleHelper"/> для форматированного вывода:
        /// <list type="number">
        /// <item><description>Заголовок "ОБУЧАЮЩИЙ ПРОЕКТ C#" с подзаголовком</description></item>
        /// <item><description>Информационный блок с описанием проекта</description></item>
        /// </list>
        /// Сообщение содержит инструкции по использованию программы.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Вывод приветствия
        /// ShowWelcome();
        /// // В консоли появится:
        /// // ================================
        /// //     ОБУЧАЮЩИЙ ПРОЕКТ C#
        /// //     Демонстрация ООП подходов
        /// // ================================
        /// // [Описание проекта]
        /// // Для школьников 7-11 классов
        /// // Используйте меню для навигации
        /// // Нажмите 0 для выхода из программы
        /// </code>
        /// </example>
        private void ShowWelcome()
        {
            string[] welcomeLines = {
                "ОБУЧАЮЩИЙ ПРОЕКТ C#",
                "Демонстрация ООП подходов"
            };

            ConsoleHelper.ShowHeader(welcomeLines);

            string[] infoLines = {
                "Для школьников 7-11 классов",
                "Используйте меню для навигации",
                "Нажмите 0 для выхода из программы"
            };

            ConsoleHelper.ShowInfoBlock("Описание проекта", infoLines);
        }

        /// <summary>
        /// Отображает прощальное сообщение при выходе из программы.
        /// </summary>
        /// <remarks>
        /// Вызывается при выборе пользователем пункта "0 - Выход" в главном меню.
        /// Использует уменьшенную ширину рамки (40 символов) для визуального отличия от основного интерфейса.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Показ прощального сообщения
        /// ShowFinalMessage();
        /// // В консоли появится:
        /// // ========================================
        /// //           ДО СВИДАНИЯ!
        /// // 
        /// //  Спасибо за использование программы!
        /// //           До новых встреч! 👋
        /// // ========================================
        /// </code>
        /// </example>
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

        /// <summary>
        /// Отображает главное меню с нумерованным списком доступных программ.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Метод формирует список пунктов меню:
        /// <list type="number">
        /// <item><description>Для каждой программы в массиве <see cref="programs"/> создается пункт с номером (i+1) и именем</description></item>
        /// <item><description>В конец списка добавляется пункт "0 - Выход"</description></item>
        /// <item><description>Список передается в <see cref="ConsoleHelper.ShowMenuWithNumbers"/> для форматированного вывода</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Нумерация программ начинается с 1 для удобства пользователя.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Показ меню с 3 программами
        /// ShowMenu();
        /// // В консоли появится:
        /// // ================================
        /// //         ГЛАВНОЕ МЕНЮ
        /// // ================================
        /// // 1. Калькулятор
        /// // 2. Конвертер валют
        /// // 3. Игра "Угадай число"
        /// // 0. Выход
        /// // ================================
        /// // >>> Введите номер программы:
        /// </code>
        /// </example>
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
    }
}