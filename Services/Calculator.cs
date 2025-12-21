namespace EducationalProject.Services
{
    /// <summary>
    /// Реализация программы "Калькулятор" для выполнения базовых арифметических операций.
    /// Наследуется от <see cref="BaseService"/> и демонстрирует принципы математических вычислений,
    /// обработки пользовательского ввода и отображения результатов.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс предоставляет функционал для:
    /// <list type="bullet">
    /// <item><description>Выполнения пяти основных арифметических операций</description></item>
    /// <item><description>Валидации пользовательского ввода</description></item>
    /// <item><description>Обработки ошибок (деление на ноль)</description></item>
    /// <item><description>Форматированного вывода результатов</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Поддерживаемые операции:</strong>
    /// <list type="table">
    /// <listheader>
    /// <term>Символ</term>
    /// <term>Операция</term>
    /// <term>Описание</term>
    /// </listheader>
    /// <item>
    /// <term>+</term>
    /// <term>Addition</term>
    /// <term>Сложение двух чисел</term>
    /// </item>
    /// <item>
    /// <term>-</term>
    /// <term>Subtraction</term>
    /// <term>Вычитание второго числа из первого</term>
    /// </item>
    /// <item>
    /// <term>*</term>
    /// <term>Multiplication</term>
    /// <term>Умножение двух чисел</term>
    /// </item>
    /// <item>
    /// <term>/</term>
    /// <term>Division</term>
    /// <term>Деление первого числа на второе</term>
    /// </item>
    /// <item>
    /// <term>%</term>
    /// <term>Modulo</term>
    /// <term>Остаток от деления первого числа на второе</term>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// Пример использования калькулятора:
    /// <code>
    /// // Создание экземпляра калькулятора
    /// var calculator = new Calculator();
    /// 
    /// // Запуск программы (в реальном сценарии вызывается через MenuManager)
    /// calculator.Run();
    /// 
    /// // Во время выполнения:
    /// // 1. Пользователь вводит числа: 10 и 3
    /// // 2. Выбирает операцию: /
    /// // 3. Получает результат: 3.33
    /// // 4. Дополнительно отображается остаток: 1.00
    /// </code>
    /// </example>
    /// <seealso cref="BaseService"/>
    /// <seealso cref="InputValidator"/>
    /// <seealso cref="ConsoleHelper"/>
    public class Calculator : BaseService
    {
        #region ===== СВОЙСТВА =====

        /// <summary>
        /// Получает название программы для отображения в главном меню.
        /// </summary>
        /// <value>Строка "Калькулятор"</value>
        /// <remarks>
        /// Это свойство переопределяет абстрактное свойство <see cref="BaseService.Name"/>.
        /// </remarks>
        public override string Name => "Калькулятор";

        #endregion

        #region ===== ПЕРЕЧИСЛЕНИЯ =====

        /// <summary>
        /// Определяет доступные математические операции.
        /// </summary>
        /// <remarks>
        /// Используется для типобезопасного представления операций
        /// вместо работы с сырыми символами или строками.
        /// </remarks>
        private enum MathOperation
        {
            /// <summary>Неопределенная операция</summary>
            None,
            /// <summary>Операция сложения (+)</summary>
            Addition,
            /// <summary>Операция вычитания (-)</summary>
            Subtraction,
            /// <summary>Операция умножения (*)</summary>
            Multiplication,
            /// <summary>Операция деления (/)</summary>
            Division,
            /// <summary>Операция взятия остатка от деления (%)</summary>
            Modulo
        }

        #endregion

        #region ===== ОСНОВНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Запускает основную логику программы "Калькулятор".
        /// </summary>
        /// <remarks>
        /// <para>Алгоритм работы метода:</para>
        /// <list type="number">
        /// <item><description>Очистка консоли и отображение заголовка</description></item>
        /// <item><description>Вывод описания программы</description></item>
        /// <item><description>Получение первого числа от пользователя</description></item>
        /// <item><description>Получение второго числа от пользователя</description></item>
        /// <item><description>Выбор математической операции</description></item>
        /// <item><description>Выполнение расчета</description></item>
        /// <item><description>Отображение результата</description></item>
        /// <item><description>Ожидание нажатия клавиши для возврата в меню</description></item>
        /// </list>
        /// <para>
        /// Метод использует <see cref="double.NaN"/> как сигнальное значение
        /// для прерывания выполнения при отмене ввода пользователем.
        /// </para>
        /// </remarks>
        /// <example>
        /// Последовательность вызовов внутри метода:
        /// <code>
        /// Run()
        /// ├── ConsoleHelper.ClearAndShowHeader()
        /// ├── ConsoleHelper.ShowInfoBlock()
        /// ├── GetNumber("первое")
        /// ├── GetNumber("второе")
        /// ├── GetOperation()
        /// ├── Calculate()
        /// ├── ShowCalculationResult()
        /// └── ConsoleHelper.WaitForAnyKey()
        /// </code>
        /// </example>
        public override void Run()
        {
            // Очистка экрана и отображение заголовка
            ConsoleHelper.ClearAndShowHeader(Name);

            // Описание программы
            string[] infoLines = {
                "Программа выполняет базовые арифметические операции",
                "с двумя числами: сложение, вычитание, умножение,",
                "деление и нахождение остатка от деления."
            };
            ConsoleHelper.ShowInfoBlock("Описание", infoLines);

            // Получение первого числа с проверкой на отмену
            double firstNumber = GetNumber("первое");
            if(double.IsNaN(firstNumber)) return;

            // Получение второго числа с проверкой на отмену
            double secondNumber = GetNumber("второе");
            if(double.IsNaN(secondNumber)) return;

            // Получение операции с проверкой на отмену
            MathOperation operation = GetOperation();
            if(operation == MathOperation.None) return;

            // Выполнение расчета
            CalculationResult result = Calculate(firstNumber, secondNumber, operation);

            // Отображение результата
            ShowCalculationResult(firstNumber, secondNumber, operation, result);

            // Ожидание пользователя перед возвратом в меню
            ConsoleHelper.WaitForAnyKey("Нажмите любую клавишу для возврата в меню...");
        }

        #endregion

        #region ===== МЕТОДЫ ВЗАИМОДЕЙСТВИЯ С ПОЛЬЗОВАТЕЛЕМ =====

        /// <summary>
        /// Запрашивает у пользователя ввод числа.
        /// </summary>
        /// <param name="numberName">Описательное имя числа (например, "первое", "второе").</param>
        /// <returns>
        /// Введенное пользователем число типа <see cref="double"/>.
        /// Возвращает <see cref="double.NaN"/> если пользователь отменил ввод.
        /// </returns>
        /// <remarks>
        /// Использует <see cref="InputValidator.GetValidNumber(string)"/> для валидации ввода.
        /// </remarks>
        /// <example>
        /// <code>
        /// double num = GetNumber("первое");
        /// // Вывод: ">>> Введите первое число: "
        /// // Пользователь вводит: 12.5
        /// // Возвращает: 12.5
        /// </code>
        /// </example>
        private double GetNumber(string numberName)
        {
            return InputValidator.GetValidNumber($">>> Введите {numberName} число: ");
        }

        /// <summary>
        /// Предоставляет пользователю выбор математической операции.
        /// </summary>
        /// <returns>
        /// Выбранная операция как значение перечисления <see cref="MathOperation"/>.
        /// Возвращает <see cref="MathOperation.None"/> если пользователь отменил выбор.
        /// </returns>
        /// <remarks>
        /// <para>Метод выполняет:</para>
        /// <list type="number">
        /// <item><description>Отображение меню доступных операций</description></item>
        /// <item><description>Ввод символа операции от пользователя</description></item>
        /// <item><description>Преобразование символа в значение перечисления</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Отображаемое меню операций:
        /// <code>
        /// ДОСТУПНЫЕ ОПЕРАЦИИ
        /// ┌─────────────────────────┐
        /// │ + : Сложение            │
        /// │ - : Вычитание           │
        /// │ * : Умножение           │
        /// │ / : Деление             │
        /// │ % : Остаток от деления  │
        /// └─────────────────────────┘
        /// </code>
        /// </example>
        private MathOperation GetOperation()
        {
            // Описание операций для меню
            string[] operations = {
                "+ : Сложение",
                "- : Вычитание",
                "* : Умножение",
                "/ : Деление",
                "% : Остаток от деления (модуль)"
            };

            // Отображение меню операций
            ConsoleHelper.ShowMenu("ДОСТУПНЫЕ ОПЕРАЦИИ", operations);
            Console.WriteLine();

            // Получение валидного символа операции
            char operationChar = InputValidator.GetValidMathOperation();

            // Преобразование символа в значение перечисления
            return ParseOperation(operationChar);
        }

        /// <summary>
        /// Преобразует символ операции в соответствующее значение перечисления.
        /// </summary>
        /// <param name="operationChar">Символ математической операции.</param>
        /// <returns>
        /// Соответствующее значение <see cref="MathOperation"/> или 
        /// <see cref="MathOperation.None"/> если символ не распознан.
        /// </returns>
        /// <remarks>
        /// Использует выражение switch для сопоставления символов.
        /// Поддерживаемые символы: '+', '-', '*', '/', '%'.
        /// </remarks>
        /// <example>
        /// <code>
        /// MathOperation op = ParseOperation('+'); // Возвращает MathOperation.Addition
        /// MathOperation op = ParseOperation('?'); // Возвращает MathOperation.None
        /// </code>
        /// </example>
        private MathOperation ParseOperation(char operationChar)
        {
            return operationChar switch
            {
                '+' => MathOperation.Addition,
                '-' => MathOperation.Subtraction,
                '*' => MathOperation.Multiplication,
                '/' => MathOperation.Division,
                '%' => MathOperation.Modulo,
                _ => MathOperation.None
            };
        }

        /// <summary>
        /// Отображает результат вычисления в форматированном виде.
        /// </summary>
        /// <param name="a">Первое число (левый операнд).</param>
        /// <param name="b">Второе число (правый операнд).</param>
        /// <param name="operation">Выполненная математическая операция.</param>
        /// <param name="result">Результат расчета с информацией об успехе/ошибке.</param>
        /// <remarks>
        /// <para>Метод обрабатывает два сценария:</para>
        /// <list type="number">
        /// <item><description>Успешный расчет - отображает результат в информационном блоке</description></item>
        /// <item><description>Ошибка расчета - отображает сообщение об ошибке</description></item>
        /// </list>
        /// <para>
        /// Для операции деления дополнительно отображается остаток,
        /// если он не равен нулю (с точностью 0.001).
        /// </para>
        /// </remarks>
        /// <example>
        /// Пример успешного вывода:
        /// <code>
        /// РЕЗУЛЬТАТ РАСЧЕТА
        /// ┌─────────────────────────────────────────────┐
        /// │ Операция: Деление                           │
        /// │ Числа: 10.00 ÷ 3.00                         │
        /// │ Результат: 3.33                             │
        /// └─────────────────────────────────────────────┘
        /// Остаток от деления: 1.00
        /// </code>
        /// </example>
        private void ShowCalculationResult(double a, double b, MathOperation operation, CalculationResult result)
        {
            Console.WriteLine();

            // Обработка ошибки вычисления
            if(!result.Success)
            {
                ConsoleHelper.ShowError(result.ErrorMessage);
                return;
            }

            // Получение символа и названия операции
            string operationSymbol = GetOperationSymbol(operation);
            string operationName = GetOperationName(operation);

            // Формирование строк результата
            string[] resultLines = {
                $"Операция: {operationName}",
                $"Числа: {a:F2} {operationSymbol} {b:F2}",
                $"Результат: {result.Value:F2}"
            };

            // Отображение результата в блоке
            ConsoleHelper.ShowInfoBlock("РЕЗУЛЬТАТ РАСЧЕТА", resultLines, 45);

            // Дополнительная информация для операции деления
            if(operation == MathOperation.Division && b != 0)
            {
                double remainder = a % b;
                // Проверка, что остаток существенен (не близок к нулю)
                if(Math.Abs(remainder) > 0.001)
                {
                    ConsoleHelper.ShowKeyValueResult("Остаток от деления", $"{remainder:F2}");
                }
            }
        }

        /// <summary>
        /// Получает символьное представление математической операции.
        /// </summary>
        /// <param name="operation">Математическая операция.</param>
        /// <returns>
        /// Строковый символ операции. Для неизвестных операций возвращает "?".
        /// </returns>
        /// <remarks>
        /// Использует пользовательские символы для умножения (×) и деления (÷)
        /// вместо стандартных * и / для лучшей читаемости.
        /// </remarks>
        /// <example>
        /// <code>
        /// string symbol = GetOperationSymbol(MathOperation.Multiplication); // Возвращает "×"
        /// string symbol = GetOperationSymbol(MathOperation.Division);      // Возвращает "÷"
        /// </code>
        /// </example>
        private string GetOperationSymbol(MathOperation operation)
        {
            return operation switch
            {
                MathOperation.Addition => "+",
                MathOperation.Subtraction => "-",
                MathOperation.Multiplication => "×",  // Специальный символ умножения
                MathOperation.Division => "÷",        // Специальный символ деления
                MathOperation.Modulo => "%",
                _ => "?"  // Запасной вариант для неизвестных операций
            };
        }

        /// <summary>
        /// Получает текстовое название математической операции.
        /// </summary>
        /// <param name="operation">Математическая операция.</param>
        /// <returns>Название операции на русском языке.</returns>
        /// <remarks>
        /// Используется для отображения в результатах расчета.
        /// </remarks>
        /// <example>
        /// <code>
        /// string name = GetOperationName(MathOperation.Modulo); // Возвращает "Остаток от деления"
        /// </code>
        /// </example>
        private string GetOperationName(MathOperation operation)
        {
            return operation switch
            {
                MathOperation.Addition => "Сложение",
                MathOperation.Subtraction => "Вычитание",
                MathOperation.Multiplication => "Умножение",
                MathOperation.Division => "Деление",
                MathOperation.Modulo => "Остаток от деления",
                _ => "Неизвестная операция"
            };
        }

        #endregion

        #region ===== МЕТОДЫ ВЫЧИСЛЕНИЙ =====

        /// <summary>
        /// Структура для хранения результата расчета с информацией об ошибках.
        /// </summary>
        /// <remarks>
        /// <para>Структура содержит три свойства:</para>
        /// <list type="bullet">
        /// <item><description><see cref="Success"/> - флаг успешности выполнения</description></item>
        /// <item><description><see cref="Value"/> - числовой результат (если успешно)</description></item>
        /// <item><description><see cref="ErrorMessage"/> - сообщение об ошибке (если не успешно)</description></item>
        /// </list>
        /// <para>
        /// Использование структуры вместо отдельных переменных позволяет
        /// единообразно обрабатывать как успешные результаты, так и ошибки.
        /// </para>
        /// </remarks>
        private struct CalculationResult
        {
            /// <summary>
            /// Получает или задает значение, указывающее успешность расчета.
            /// </summary>
            /// <value>
            /// <see langword="true"/> если расчет выполнен успешно; 
            /// <see langword="false"/> если произошла ошибка.
            /// </value>
            public bool Success { get; set; }

            /// <summary>
            /// Получает или задает числовое значение результата расчета.
            /// </summary>
            /// <value>
            /// Результат математической операции. Имеет смысл только при <see cref="Success"/> = <see langword="true"/>.
            /// </value>
            public double Value { get; set; }

            /// <summary>
            /// Получает или задает сообщение об ошибке.
            /// </summary>
            /// <value>
            /// Описание ошибки. Имеет смысл только при <see cref="Success"/> = <see langword="false"/>.
            /// </value>
            public string ErrorMessage { get; set; }
        }

        /// <summary>
        /// Выполняет математическую операцию над двумя числами.
        /// </summary>
        /// <param name="a">Первое число (левый операнд).</param>
        /// <param name="b">Второе число (правый операнд).</param>
        /// <param name="operation">Математическая операция для выполнения.</param>
        /// <returns>
        /// Структура <see cref="CalculationResult"/> с результатом выполнения.
        /// </returns>
        /// <remarks>
        /// <para>Особенности реализации:</para>
        /// <list type="bullet">
        /// <item><description>Включает проверку деления на ноль для операций деления и взятия остатка</description></item>
        /// <item><description>Оборачивает вычисления в try-catch для обработки исключений</description></item>
        /// <item><description>Использует <see cref="InputValidator.ValidateDivision"/> для валидации делителя</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// CalculationResult result = Calculate(10, 0, MathOperation.Division);
        /// // result.Success = false
        /// // result.ErrorMessage = "Ошибка: деление на ноль невозможно!"
        /// 
        /// CalculationResult result = Calculate(10, 3, MathOperation.Addition);
        /// // result.Success = true
        /// // result.Value = 13
        /// </code>
        /// </example>
        private CalculationResult Calculate(double a, double b, MathOperation operation)
        {
            CalculationResult result = new();

            try
            {
                switch(operation)
                {
                    case MathOperation.Addition:
                        result.Value = a + b;
                        result.Success = true;
                        break;

                    case MathOperation.Subtraction:
                        result.Value = a - b;
                        result.Success = true;
                        break;

                    case MathOperation.Multiplication:
                        result.Value = a * b;
                        result.Success = true;
                        break;

                    case MathOperation.Division:
                        // Проверка деления на ноль
                        var divisionCheck = InputValidator.ValidateDivision(b, "деление");
                        if(!divisionCheck.IsValid)
                        {
                            result.Success = false;
                            result.ErrorMessage = divisionCheck.ErrorMessage;
                        }
                        else
                        {
                            result.Value = a / b;
                            result.Success = true;
                        }
                        break;

                    case MathOperation.Modulo:
                        // Проверка деления на ноль для операции модуля
                        var moduloCheck = InputValidator.ValidateDivision(b, "операция модуля");
                        if(!moduloCheck.IsValid)
                        {
                            result.Success = false;
                            result.ErrorMessage = moduloCheck.ErrorMessage;
                        }
                        else
                        {
                            result.Value = a % b;
                            result.Success = true;
                        }
                        break;

                    default:
                        result.Success = false;
                        result.ErrorMessage = "Неизвестная операция!";
                        break;
                }
            }
            catch(Exception ex)
            {
                // Обработка неожиданных исключений
                result.Success = false;
                result.ErrorMessage = $"Ошибка при вычислении: {ex.Message}";
            }

            return result;
        }

        #endregion
    }
}