namespace EducationalProject
{
    /// <summary>
    /// Абстрактный базовый класс для всех образовательных программ в системе.
    /// Определяет общий интерфейс и структуру для конкретных реализаций программ.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="BaseService"/> служит основой для создания новых образовательных модулей.
    /// Все программы в системе должны наследоваться от этого класса и реализовывать его абстрактные члены.
    /// </para>
    /// <para>
    /// <strong>Как создать новую программу:</strong>
    /// <list type="number">
    /// <item><description>Создайте новый класс в папке <c>Services</c></description></item>
    /// <item><description>Наследуйтесь от <see cref="BaseService"/></description></item>
    /// <item><description>Реализуйте свойство <see cref="Name"/> с названием программы</description></item>
    /// <item><description>Реализуйте метод <see cref="Run"/> с основной логикой программы</description></item>
    /// <item><description>Добавьте экземпляр программы в список <see cref="ProgramsConfig.GetAllPrograms"/></description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Архитектурные особенности:</strong>
    /// <list type="bullet">
    /// <item><description>Использование абстрактного класса обеспечивает полиморфизм</description></item>
    /// <item><description><see cref="MenuManager"/> работает с программами через базовый интерфейс</description></item>
    /// <item><description>Все программы имеют единообразную структуру запуска</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример создания простой программы:
    /// <code>
    /// using EducationalProject;
    /// 
    /// namespace EducationalProject.Services
    /// {
    ///     public class MyProgram : BaseService
    ///     {
    ///         // Реализация абстрактного свойства Name
    ///         public override string Name => "Моя тестовая программа";
    ///         
    ///         // Реализация абстрактного метода Run
    ///         public override void Run()
    ///         {
    ///             // Очистка экрана и отображение заголовка
    ///             ConsoleHelper.ClearAndShowHeader(Name);
    ///             
    ///             // Основная логика программы
    ///             ConsoleHelper.ShowInfo("Добро пожаловать в мою программу!");
    ///             ConsoleHelper.ShowWarning("Это предупреждение");
    ///             ConsoleHelper.ShowError("Это сообщение об ошибке");
    ///             
    ///             // Ожидание действий пользователя
    ///             ConsoleHelper.WaitForAnyKey();
    ///         }
    ///     }
    /// }
    /// </code>
    /// 
    /// Пример более сложной программы с обработкой ввода:
    /// <code>
    /// public class CalculatorProgram : BaseService
    /// {
    ///     public override string Name => "Калькулятор";
    ///     
    ///     public override void Run()
    ///     {
    ///         ConsoleHelper.ClearAndShowHeader(Name);
    ///         
    ///         double number1 = InputValidator.GetValidDouble("Введите первое число: ");
    ///         double number2 = InputValidator.GetValidDouble("Введите второе число: ");
    ///         
    ///         double result = number1 + number2;
    ///         ConsoleHelper.ShowSuccess($"Результат сложения: {result}");
    ///         
    ///         ConsoleHelper.WaitForAnyKey();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MenuManager"/>
    /// <seealso cref="ProgramsConfig"/>
    /// <seealso cref="ConsoleHelper"/>
    /// <seealso cref="InputValidator"/>
    public abstract class BaseService
    {
        #region ===== ТЕКСТОВЫЕ КОНСТАНТЫ (общие для всех программ) =====

        /// <summary>
        /// Сообщение для ожидания нажатия клавиши перед возвратом в меню
        /// </summary>
        protected const string PRESS_ANY_KEY_TO_RETURN = "Нажмите любую клавишу для возврата в меню...";

        /// <summary>
        /// Сообщение для ожидания нажатия клавиши для продолжения
        /// </summary>
        protected const string PRESS_ANY_KEY_TO_CONTINUE = "Нажмите любую клавишу для продолжения...";

        /// <summary>
        /// Стандартное приглашение для ввода
        /// </summary>
        protected const string DEFAULT_INPUT_PROMPT = ">>> ";

        /// <summary>
        /// Приглашение для выбора из меню
        /// </summary>
        protected const string MENU_CHOICE_PROMPT = ">>> Введите номер: ";

        /// <summary>
        /// Заголовок для описания программы
        /// </summary>
        protected const string DESCRIPTION_TITLE = "Описание";

        /// <summary>
        /// Заголовок для отображения результата
        /// </summary>
        protected const string RESULT_TITLE = "Результат";

        /// <summary>
        /// Заголовок для отображения ошибки
        /// </summary>
        protected const string ERROR_TITLE = "Ошибка";

        /// <summary>
        /// Текст для некорректного выбора в меню
        /// </summary>
        protected const string INVALID_CHOICE_MESSAGE = "Неверный выбор";

        /// <summary>
        /// Текст для повтора попытки ввода
        /// </summary>
        protected const string TRY_AGAIN_MESSAGE = "Попробуйте ещё раз...";

        #endregion

        /// <summary>
        /// Получает отображаемое название программы.
        /// </summary>
        /// <value>
        /// Строка с названием программы, которая будет отображаться в меню выбора.
        /// </value>
        /// <remarks>
        /// <para>
        /// Название должно быть понятным и информативным для пользователя.
        /// Рекомендуется использовать краткие, но описательные названия.
        /// </para>
        /// <example>
        /// Примеры хороших названий:
        /// <code>
        /// "Калькулятор матриц"
        /// "Генератор случайных чисел"
        /// "Конвертер температур"
        /// "Анализатор текста"
        /// </code>
        /// </example>
        /// </remarks>
        public abstract string Name { get; }

        /// <summary>
        /// Основной метод выполнения программы.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Этот метод содержит всю логику конкретной программы. При реализации следует:
        /// <list type="bullet">
        /// <item><description>Использовать <see cref="ConsoleHelper"/> для вывода информации</description></item>
        /// <item><description>Использовать <see cref="InputValidator"/> для валидации ввода</description></item>
        /// <item><description>Обеспечивать корректную обработку ошибок</description></item>
        /// <item><description>Завершать выполнение вызовом <c>ConsoleHelper.WaitForAnyKey()</c> для паузы</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// <strong>Типичная структура метода Run:</strong>
        /// <list type="number">
        /// <item><description>Очистка экрана и отображение заголовка</description></item>
        /// <item><description>Ввод и валидация данных от пользователя</description></item>
        /// <item><description>Выполнение вычислений или обработки</description></item>
        /// <item><description>Вывод результатов</description></item>
        /// <item><description>Ожидание реакции пользователя</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// public override void Run()
        /// {
        ///     // Шаг 1: Инициализация
        ///     ConsoleHelper.ClearAndShowHeader(Name);
        ///     
        ///     // Шаг 2: Ввод данных
        ///     int count = InputValidator.GetValidInteger(
        ///         "Сколько чисел сгенерировать? ",
        ///         minValue: 1,
        ///         maxValue: 100
        ///     );
        ///     
        ///     // Шаг 3: Обработка
        ///     Random rnd = new Random();
        ///     var numbers = Enumerable.Range(0, count)
        ///         .Select(_ => rnd.Next(1, 100))
        ///         .ToList();
        ///     
        ///     // Шаг 4: Вывод результатов
        ///     ConsoleHelper.ShowInfo($"Сгенерировано чисел: {numbers.Count}");
        ///     ConsoleHelper.ShowSuccess($"Сумма: {numbers.Sum()}");
        ///     ConsoleHelper.ShowWarning($"Максимальное: {numbers.Max()}");
        ///     
        ///     // Шаг 5: Завершение
        ///     ConsoleHelper.WaitForAnyKey();
        /// }
        /// </code>
        /// </example>
        public abstract void Run();
    }
}