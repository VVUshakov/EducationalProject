namespace EducationalProject
{
    /// <summary>
    /// Главный класс приложения - точка входа в программу.
    /// Отвечает за инициализацию и запуск системы меню образовательных программ.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Этот класс является отправной точкой приложения, который:
    /// 1. Создает экземпляр <see cref="MenuManager"/>
    /// 2. Запускает основной цикл управления меню
    /// </para>
    /// <para>
    /// Структура проекта:
    /// <list type="number">
    /// <item><description><see cref="Program"/> - точка входа (текущий класс)</description></item>
    /// <item><description><see cref="MenuManager"/> - управление меню и навигацией</description></item>
    /// <item><description><see cref="BaseService"/> - базовый класс для всех программ</description></item>
    /// <item><description><see cref="ProgramsConfig"/> - конфигурация списка программ</description></item>
    /// <item><description><see cref="ConsoleHelper"/> - утилиты для работы с консолью</description></item>
    /// <item><description><see cref="InputValidator"/> - валидация пользовательского ввода</description></item>
    /// <item><description><see cref="Services"/> - папка с конкретными программами</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример запуска приложения:
    /// <code>
    /// // При компиляции и запуске проекта автоматически выполняется:
    /// // 1. Program.Main() -> создание MenuManager
    /// // 2. manager.Run() -> отображение главного меню
    /// </code>
    /// </example>
    /// <seealso cref="MenuManager"/>
    /// <seealso cref="BaseService"/>
    /// <seealso cref="ProgramsConfig"/>
    class Program
    {
        /// <summary>
        /// Точка входа в приложение. Создает и запускает менеджер меню.
        /// </summary>
        /// <remarks>
        /// Метод выполняет следующие действия:
        /// <list type="number">
        /// <item><description>Создает экземпляр класса <see cref="MenuManager"/></description></item>
        /// <item><description>Вызывает метод <see cref="MenuManager.Run"/> для запуска основного цикла программы</description></item>
        /// </list>
        /// </remarks>
        /// 
        /// <example>
        /// <code>
        /// static void Main()
        /// {
        ///     // Инициализация менеджера меню
        ///     var manager = new MenuManager();
        ///     
        ///     // Запуск основного цикла приложения
        ///     manager.Run();
        /// }
        /// </code>
        /// </example>
        static void Main()
        {
            // Создаем менеджер меню и запускаем его
            var manager = new MenuManager();
            manager.Run();
        }
    }
}