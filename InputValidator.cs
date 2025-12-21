namespace EducationalProject
{
    /// <summary>
    /// Статический класс для валидации пользовательского ввода.
    /// Содержит универсальные методы проверки различных типов данных с обратной связью для пользователя.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Класс предоставляет многоуровневую систему валидации:
    /// <list type="bullet">
    /// <item><description>Проверка корректности формата данных</description></item>
    /// <item><description>Проверка соответствия диапазонам и ограничениям</description></item>
    /// <item><description>Проверка семантической правильности (например, деление на ноль)</description></item>
    /// <item><description>Цикличный запрос данных до получения корректного ввода</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Используется для обеспечения надежности пользовательского ввода во всех программах проекта.
    /// Все методы возвращают понятные сообщения об ошибках на русском языке.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Простая валидация меню
    /// int choice = InputValidator.GetValidMenuChoice(0, 5, "Введите выбор: ");
    /// 
    /// // Валидация числа в диапазоне
    /// double value = InputValidator.GetValidNumberInRange("Введите число от 1 до 10: ", 1, 10);
    /// 
    /// // Валидация текста
    /// string name = InputValidator.GetValidText("Введите ваше имя: ", 2, 50);
    /// 
    /// // Валидация двоичного числа
    /// string binary = InputValidator.GetValidBinary("Введите 8-битное двоичное число: ");
    /// </code>
    /// </example>
    public static class InputValidator
    {
        #region ===== ОСНОВНЫЕ СТРУКТУРЫ ДАННЫХ =====

        /// <summary>
        /// Структура, представляющая результат операции валидации.
        /// </summary>
        /// <remarks>
        /// Содержит информацию о том, прошла ли валидация успешно,
        /// проверенное значение (если успешно) и сообщение об ошибке (если есть).
        /// Используется всеми методами валидации для стандартизированного возврата результатов.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Использование ValidationResult
        /// ValidationResult result = InputValidator.ValidateNumber("123");
        /// if(result.IsValid)
        /// {
        ///     double value = (double)result.Value;
        ///     Console.WriteLine($"Получено число: {value}");
        /// }
        /// else
        /// {
        ///     Console.WriteLine($"Ошибка: {result.ErrorMessage}");
        /// }
        /// </code>
        /// </example>
        public struct ValidationResult
        {
            /// <summary>
            /// Получает или задает значение, указывающее, прошла ли валидация успешно.
            /// </summary>
            /// <value><c>true</c> если валидация прошла успешно; иначе <c>false</c>.</value>
            public bool IsValid { get; set; }

            /// <summary>
            /// Получает или задает сообщение об ошибке, если валидация не прошла.
            /// </summary>
            /// <value>Строка с описанием ошибки, или пустая строка если ошибки нет.</value>
            public string ErrorMessage { get; set; }

            /// <summary>
            /// Получает или задает проверенное значение.
            /// </summary>
            /// <value>Проверенное значение любого типа, или <c>null</c> если валидация не прошла.</value>
            /// <remarks>
            /// Требуется приведение типа к ожидаемому типу данных.
            /// </remarks>
            public object Value { get; set; }

            /// <summary>
            /// Создает успешный результат валидации с указанным значением.
            /// </summary>
            /// <param name="value">Проверенное значение.</param>
            /// <returns>Успешный <see cref="ValidationResult"/>.</returns>
            /// <example>
            /// <code>
            /// ValidationResult success = ValidationResult.Success(42);
            /// // success.IsValid == true
            /// // success.Value == 42
            /// // success.ErrorMessage == ""
            /// </code>
            /// </example>
            public static ValidationResult Success(object value)
            {
                return new ValidationResult
                {
                    IsValid = true,
                    Value = value,
                    ErrorMessage = string.Empty
                };
            }

            /// <summary>
            /// Создает результат валидации с ошибкой.
            /// </summary>
            /// <param name="message">Сообщение об ошибке.</param>
            /// <returns>Неуспешный <see cref="ValidationResult"/>.</returns>
            /// <example>
            /// <code>
            /// ValidationResult error = ValidationResult.Error("Неверный формат числа");
            /// // error.IsValid == false
            /// // error.Value == null
            /// // error.ErrorMessage == "Неверный формат числа"
            /// </code>
            /// </example>
            public static ValidationResult Error(string message)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    ErrorMessage = message,
                    Value = null
                };
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ МЕНЮ =====

        /// <summary>
        /// Проверяет выбор пользователя из меню на корректность.
        /// </summary>
        /// <param name="input">Введенная пользователем строка.</param>
        /// <param name="minValue">Минимальное допустимое значение (включительно).</param>
        /// <param name="maxValue">Максимальное допустимое значение (включительно).</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с целым числом, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// Выполняет три уровня проверки:
        /// <list type="number">
        /// <item><description>Проверка на пустой ввод</description></item>
        /// <item><description>Проверка, что ввод является целым числом</description></item>
        /// <item><description>Проверка, что число находится в допустимом диапазоне</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Проверка выбора из меню с пунктами 1-5
        /// ValidationResult result = ValidateMenuChoice("3", 1, 5);
        /// // result.IsValid == true, result.Value == 3
        /// 
        /// result = ValidateMenuChoice("abc", 1, 5);
        /// // result.IsValid == false, result.ErrorMessage содержит сообщение об ошибке
        /// 
        /// result = ValidateMenuChoice("10", 1, 5);
        /// // result.IsValid == false, result.ErrorMessage указывает на недопустимый диапазон
        /// </code>
        /// </example>
        public static ValidationResult ValidateMenuChoice(string input, int minValue, int maxValue)
        {
            // Проверка на пустой ввод
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            // Проверка на число
            if(!int.TryParse(input, out int number))
            {
                return ValidationResult.Error($"'{input}' - это не число! Пожалуйста, введите номер цифрами.");
            }

            // Проверка диапазона
            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"Нет пункта № {number}! Доступны номера от {minValue} до {maxValue}.");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Получает валидный выбор из меню с повторными попытками в случае некорректного ввода.
        /// </summary>
        /// <param name="minValue">Минимальное допустимое значение.</param>
        /// <param name="maxValue">Максимальное допустимое значение.</param>
        /// <param name="prompt">Текст приглашения для ввода. По умолчанию: ">>> Введите номер: ".</param>
        /// <returns>Валидный целочисленный выбор пользователя.</returns>
        /// <remarks>
        /// <para>
        /// Метод работает в цикле до получения корректного ввода:
        /// <list type="number">
        /// <item><description>Выводит приглашение через <see cref="ConsoleHelper.GetInput"/></description></item>
        /// <item><description>Проверяет ввод с помощью <see cref="ValidateMenuChoice"/></description></item>
        /// <item><description>При ошибке показывает сообщение и предлагает повторить ввод</description></item>
        /// <item><description>При успехе возвращает проверенное значение</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Цикл продолжается бесконечно до получения корректного ввода.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение выбора из меню с 5 пунктами
        /// int choice = GetValidMenuChoice(1, 5, "Выберите пункт меню: ");
        /// // Пользователь будет повторно запрашиваться, пока не введет число от 1 до 5
        /// </code>
        /// </example>
        /// <seealso cref="ValidateMenuChoice"/>
        /// <seealso cref="ConsoleHelper.GetInput"/>
        /// <seealso cref="ConsoleHelper.ShowError"/>
        public static int GetValidMenuChoice(int minValue, int maxValue, string prompt = ">>> Введите номер: ")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateMenuChoice(input, minValue, maxValue);

                if(result.IsValid)
                {
                    return (int)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ЧИСЕЛ =====

        /// <summary>
        /// Проверяет строку на соответствие числовому формату.
        /// </summary>
        /// <param name="input">Введенная строка.</param>
        /// <param name="allowNegative">Разрешает ли отрицательные числа. По умолчанию <c>true</c>.</param>
        /// <param name="allowZero">Разрешает ли нулевое значение. По умолчанию <c>true</c>.</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с числом типа <see cref="double"/>, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Выполняет следующие проверки:
        /// <list type="number">
        /// <item><description>Непустой ввод</description></item>
        /// <item><description>Корректный числовой формат (с плавающей точкой)</description></item>
        /// <item><description>Отрицательные числа (если запрещены)</description></item>
        /// <item><description>Нулевое значение (если запрещено)</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Использует сравнение с <see cref="double.Epsilon"/> для проверки на ноль.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Разрешить отрицательные, запретить ноль
        /// ValidateNumber("-5.5", true, false); // Успех
        /// ValidateNumber("0", true, false);    // Ошибка: "Ноль не разрешен!"
        /// ValidateNumber("abc", true, true);   // Ошибка: "не является допустимым числом!"
        /// </code>
        /// </example>
        public static ValidationResult ValidateNumber(string input, bool allowNegative = true, bool allowZero = true)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            if(!double.TryParse(input, out double number))
            {
                return ValidationResult.Error($"'{input}' не является допустимым числом!");
            }

            if(!allowNegative && number < 0)
            {
                return ValidationResult.Error("Отрицательные числа не разрешены!");
            }

            if(!allowZero && Math.Abs(number) < double.Epsilon)
            {
                return ValidationResult.Error("Ноль не разрешен!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Проверяет число на соответствие заданному диапазону.
        /// </summary>
        /// <param name="input">Введенная строка.</param>
        /// <param name="minValue">Минимальное допустимое значение (включительно).</param>
        /// <param name="maxValue">Максимальное допустимое значение (включительно).</param>
        /// <param name="valueName">Наименование проверяемого значения для сообщений об ошибке. По умолчанию "Число".</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с числом типа <see cref="double"/>, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Метод сначала вызывает базовую валидацию числа через <see cref="ValidateNumber"/>,
        /// затем проверяет попадание в диапазон.
        /// </para>
        /// <para>
        /// Используется для проверки параметров, требующих ограничений по диапазону (возраст, проценты, и т.д.).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Проверка процента от 0 до 100
        /// ValidateNumberInRange("75", 0, 100, "Процент"); // Успех
        /// ValidateNumberInRange("-10", 0, 100, "Процент"); // Ошибка: "Процент должно быть от 0 до 100!"
        /// ValidateNumberInRange("150", 0, 100, "Процент"); // Ошибка: "Процент должно быть от 0 до 100!"
        /// </code>
        /// </example>
        /// <seealso cref="ValidateNumber"/>
        public static ValidationResult ValidateNumberInRange(string input, double minValue, double maxValue, string valueName = "Число")
        {
            var basicResult = ValidateNumber(input);
            if(!basicResult.IsValid)
            {
                return basicResult;
            }

            double number = (double)basicResult.Value;

            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"{valueName} должно быть от {minValue} до {maxValue}!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Проверяет целое число на соответствие заданному диапазону.
        /// </summary>
        /// <param name="input">Введенная строка.</param>
        /// <param name="minValue">Минимальное допустимое целое значение (включительно).</param>
        /// <param name="maxValue">Максимальное допустимое целое значение (включительно).</param>
        /// <param name="valueName">Наименование проверяемого значения для сообщений об ошибке. По умолчанию "Число".</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с целым числом, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// В отличие от <see cref="ValidateNumberInRange"/>, этот метод работает только с целыми числами.
        /// </para>
        /// <para>
        /// Используется для проверки целочисленных параметров (количество предметов, возраст в годах, и т.д.).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Проверка возраста от 0 до 120 лет
        /// ValidateIntegerInRange("25", 0, 120, "Возраст"); // Успех
        /// ValidateIntegerInRange("25.5", 0, 120, "Возраст"); // Ошибка: "не является целым числом!"
        /// </code>
        /// </example>
        public static ValidationResult ValidateIntegerInRange(string input, int minValue, int maxValue, string valueName = "Число")
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            if(!int.TryParse(input, out int number))
            {
                return ValidationResult.Error($"'{input}' не является целым числом!");
            }

            if(number < minValue || number > maxValue)
            {
                return ValidationResult.Error($"{valueName} должно быть от {minValue} до {maxValue}!");
            }

            return ValidationResult.Success(number);
        }

        /// <summary>
        /// Получает валидное число с повторными попытками в случае некорректного ввода.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода.</param>
        /// <param name="allowNegative">Разрешает ли отрицательные числа. По умолчанию <c>true</c>.</param>
        /// <param name="allowZero">Разрешает ли нулевое значение. По умолчанию <c>true</c>.</param>
        /// <returns>Валидное число типа <see cref="double"/>.</returns>
        /// <remarks>
        /// Метод работает в цикле до получения корректного числа.
        /// Использует <see cref="ValidateNumber"/> для проверки каждого ввода.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение положительного числа (ноль разрешен)
        /// double value = GetValidNumber("Введите положительное число: ", false, true);
        /// // Пользователь будет повторно запрашиваться, пока не введет неотрицательное число
        /// </code>
        /// </example>
        /// <seealso cref="ValidateNumber"/>
        public static double GetValidNumber(string prompt, bool allowNegative = true, bool allowZero = true)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateNumber(input, allowNegative, allowZero);

                if(result.IsValid)
                {
                    return (double)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получает валидное число в заданном диапазоне с повторными попытками.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода.</param>
        /// <param name="minValue">Минимальное допустимое значение.</param>
        /// <param name="maxValue">Максимальное допустимое значение.</param>
        /// <param name="valueName">Наименование значения для сообщений об ошибке. По умолчанию "Число".</param>
        /// <returns>Валидное число типа <see cref="double"/> в указанном диапазоне.</returns>
        /// <remarks>
        /// Метод работает в цикле до получения числа, удовлетворяющего всем ограничениям.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение оценки от 1 до 5
        /// double grade = GetValidNumberInRange("Введите оценку (1-5): ", 1, 5, "Оценка");
        /// </code>
        /// </example>
        /// <seealso cref="ValidateNumberInRange"/>
        public static double GetValidNumberInRange(string prompt, double minValue, double maxValue, string valueName = "Число")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateNumberInRange(input, minValue, maxValue, valueName);

                if(result.IsValid)
                {
                    return (double)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получает валидное целое число в заданном диапазоне с повторными попытками.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода.</param>
        /// <param name="minValue">Минимальное допустимое целое значение.</param>
        /// <param name="maxValue">Максимальное допустимое целое значение.</param>
        /// <param name="valueName">Наименование значения для сообщений об ошибке. По умолчанию "Число".</param>
        /// <returns>Валидное целое число в указанном диапазоне.</returns>
        /// <remarks>
        /// Метод работает в цикле до получения целого числа, удовлетворяющего ограничениям диапазона.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение количества предметов от 1 до 10
        /// int count = GetValidIntegerInRange("Сколько предметов (1-10)? ", 1, 10, "Количество");
        /// </code>
        /// </example>
        /// <seealso cref="ValidateIntegerInRange"/>
        public static int GetValidIntegerInRange(string prompt, int minValue, int maxValue, string valueName = "Число")
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateIntegerInRange(input, minValue, maxValue, valueName);

                if(result.IsValid)
                {
                    return (int)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ТЕКСТА =====

        /// <summary>
        /// Проверяет текстовый ввод на соответствие заданным критериям.
        /// </summary>
        /// <param name="input">Введенная строка.</param>
        /// <param name="minLength">Минимальная допустимая длина строки. По умолчанию 1.</param>
        /// <param name="maxLength">Максимальная допустимая длина строкы. По умолчанию 100.</param>
        /// <param name="allowDigits">Разрешает ли цифры в тексте. По умолчанию <c>true</c>.</param>
        /// <param name="allowSpecialChars">Разрешает ли специальные символы (пробелы, пунктуацию). По умолчанию <c>true</c>.</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с обрезанной строкой, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Выполняет комплексную проверку текста:
        /// <list type="number">
        /// <item><description>Непустой ввод</description></item>
        /// <item><description>Длина строки в заданных пределах</description></item>
        /// <item><description>Наличие хотя бы одной буквы (обязательно)</description></item>
        /// <item><description>Только разрешенные символы (настраивается параметрами)</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Пробелы в начале и конце строки автоматически удаляются.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Проверка имени (только буквы, 2-30 символов)
        /// ValidateText("Иван", 2, 30, false, false); // Успех
        /// ValidateText("Иван123", 2, 30, false, false); // Ошибка: цифры не разрешены
        /// ValidateText("A", 2, 30, true, true); // Ошибка: менее 2 символов
        /// </code>
        /// </example>
        public static ValidationResult ValidateText(string input, int minLength = 1, int maxLength = 100,
                                                    bool allowDigits = true, bool allowSpecialChars = true)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            string trimmedInput = input.Trim();

            if(trimmedInput.Length < minLength)
            {
                return ValidationResult.Error($"Текст должен содержать не менее {minLength} символов!");
            }

            if(trimmedInput.Length > maxLength)
            {
                return ValidationResult.Error($"Текст должен содержать не более {maxLength} символов!");
            }

            // Проверка на наличие хотя бы одной буквы
            bool hasLetters = false;
            foreach(char c in trimmedInput)
            {
                if(char.IsLetter(c))
                {
                    hasLetters = true;
                    break;
                }
            }

            if(!hasLetters)
            {
                return ValidationResult.Error("Текст должен содержать хотя бы одну букву!");
            }

            // Проверка разрешенных символов
            foreach(char c in trimmedInput)
            {
                if(char.IsLetter(c)) continue;
                if(allowDigits && char.IsDigit(c)) continue;
                if(allowSpecialChars && char.IsWhiteSpace(c)) continue;
                if(allowSpecialChars && char.IsPunctuation(c)) continue;

                return ValidationResult.Error($"Символ '{c}' не разрешен!");
            }

            return ValidationResult.Success(trimmedInput);
        }

        /// <summary>
        /// Проверяет строку на соответствие формату двоичного числа.
        /// </summary>
        /// <param name="input">Введенная строка.</param>
        /// <param name="maxBits">Максимальное количество бит (символов 0/1). По умолчанию 8.</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с очищенной двоичной строкой (без пробелов), если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Выполняет проверки:
        /// <list type="number">
        /// <item><description>Непустой ввод</description></item>
        /// <item><description>Длина не превышает максимальное количество бит</description></item>
        /// <item><description>Только символы '0' и '1'</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// Пробелы в строке автоматически удаляются перед проверкой.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Проверка 8-битного двоичного числа
        /// ValidateBinary("10101010"); // Успех
        /// ValidateBinary("1010 1010"); // Успех (пробелы удаляются)
        /// ValidateBinary("10201010"); // Ошибка: содержит символ '2'
        /// ValidateBinary("1010101010"); // Ошибка: более 8 бит
        /// </code>
        /// </example>
        public static ValidationResult ValidateBinary(string input, int maxBits = 8)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.Error("Вы ничего не ввели!");
            }

            // Убираем пробелы для проверки
            string cleanInput = input.Replace(" ", "");

            if(cleanInput.Length > maxBits)
            {
                return ValidationResult.Error($"Слишком длинное число! Максимум {maxBits} бит.");
            }

            // Проверка на допустимые символы (только 0 и 1)
            foreach(char c in cleanInput)
            {
                if(c != '0' && c != '1')
                {
                    return ValidationResult.Error($"'{input}' содержит недопустимые символы! Используйте только 0 и 1.");
                }
            }

            return ValidationResult.Success(cleanInput);
        }

        /// <summary>
        /// Получает валидный текст с повторными попытками в случае некорректного ввода.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода.</param>
        /// <param name="minLength">Минимальная допустимая длина строки. По умолчанию 1.</param>
        /// <param name="maxLength">Максимальная допустимая длина строки. По умолчанию 100.</param>
        /// <param name="allowDigits">Разрешает ли цифры в тексте. По умолчанию <c>true</c>.</param>
        /// <param name="allowSpecialChars">Разрешает ли специальные символы. По умолчанию <c>true</c>.</param>
        /// <returns>Валидная текстовая строка, удовлетворяющая всем критериям.</returns>
        /// <remarks>
        /// Метод работает в цикле до получения текста, удовлетворяющего всем ограничениям.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение имени (только буквы, 2-50 символов)
        /// string name = GetValidText("Введите ваше имя: ", 2, 50, false, false);
        /// // Пользователь будет повторно запрашиваться, пока не введет корректное имя
        /// </code>
        /// </example>
        /// <seealso cref="ValidateText"/>
        public static string GetValidText(string prompt, int minLength = 1, int maxLength = 100,
                                         bool allowDigits = true, bool allowSpecialChars = true)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateText(input, minLength, maxLength, allowDigits, allowSpecialChars);

                if(result.IsValid)
                {
                    return (string)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        /// <summary>
        /// Получает валидное двоичное число с повторными попытками в случае некорректного ввода.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода.</param>
        /// <param name="maxBits">Максимальное количество бит. По умолчанию 8.</param>
        /// <returns>Валидная двоичная строка (только символы 0 и 1).</returns>
        /// <remarks>
        /// Метод работает в цикле до получения корректного двоичного числа.
        /// Пробелы в вводе игнорируются.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение 8-битного двоичного числа
        /// string binary = GetValidBinary("Введите двоичное число (8 бит): ");
        /// // Примеры допустимого ввода: "10101010", "1010 1010", "11110000"
        /// </code>
        /// </example>
        /// <seealso cref="ValidateBinary"/>
        public static string GetValidBinary(string prompt, int maxBits = 8)
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(prompt);
                var result = ValidateBinary(input, maxBits);

                if(result.IsValid)
                {
                    return (string)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== МЕТОДЫ ВАЛИДАЦИИ ВЫБОРА ОПЕРАЦИЙ =====

        /// <summary>
        /// Проверяет символ на соответствие допустимым математическим операциям.
        /// </summary>
        /// <param name="operationChar">Проверяемый символ операции.</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с символом операции, если валидация прошла успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// Поддерживаемые операции:
        /// <list type="bullet">
        /// <item><description>+ (сложение)</description></item>
        /// <item><description>- (вычитание)</description></item>
        /// <item><description>* (умножение)</description></item>
        /// <item><description>/ (деление)</description></item>
        /// <item><description>% (остаток от деления)</description></item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <code>
        /// ValidateMathOperation('+'); // Успех
        /// ValidateMathOperation('*'); // Успех
        /// ValidateMathOperation('&'); // Ошибка: операция не поддерживается
        /// </code>
        /// </example>
        public static ValidationResult ValidateMathOperation(char operationChar)
        {
            char[] validOperations = { '+', '-', '*', '/', '%' };

            if(Array.IndexOf(validOperations, operationChar) == -1)
            {
                return ValidationResult.Error($"Операция '{operationChar}' не поддерживается! Используйте: +, -, *, /, %");
            }

            return ValidationResult.Success(operationChar);
        }

        /// <summary>
        /// Получает валидный символ математической операции с повторными попытками.
        /// </summary>
        /// <param name="prompt">Текст приглашения для ввода. По умолчанию: ">>> Выберите операцию (+, -, *, /, %): ".</param>
        /// <returns>Валидный символ математической операции.</returns>
        /// <remarks>
        /// Метод считывает один символ с клавиатуры без ожидания Enter.
        /// Работает в цикле до получения допустимой операции.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Получение операции для калькулятора
        /// char operation = GetValidMathOperation("Введите операцию (+, -, *, /, %): ");
        /// // Пользователь нажимает один символ, который сразу проверяется
        /// </code>
        /// </example>
        /// <seealso cref="ValidateMathOperation"/>
        public static char GetValidMathOperation(string prompt = ">>> Выберите операцию (+, -, *, /, %): ")
        {
            while(true)
            {
                Console.Write(prompt);
                char operationChar = Console.ReadKey().KeyChar;
                Console.WriteLine();

                var result = ValidateMathOperation(operationChar);

                if(result.IsValid)
                {
                    return (char)result.Value;
                }

                ConsoleHelper.ShowError(result.ErrorMessage);
                Console.WriteLine("Попробуйте ещё раз...\n");
            }
        }

        #endregion

        #region ===== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Преобразует двоичную строку в десятичное число.
        /// </summary>
        /// <param name="binaryString">Двоичная строка (только символы 0 и 1).</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с целым десятичным числом, если преобразование прошло успешно,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Использует стандартный метод <see cref="Convert.ToInt32(string, int)"/> с основанием 2.
        /// </para>
        /// <para>
        /// Обрабатывает возможные исключения:
        /// <list type="bullet">
        /// <item><description><see cref="FormatException"/> - неверный формат числа</description></item>
        /// <item><description><see cref="OverflowException"/> - число слишком большое</description></item>
        /// <item><description>Другие исключения</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// BinaryToDecimal("1010"); // Успех: 10
        /// BinaryToDecimal("1020"); // Ошибка: неверный формат
        /// BinaryToDecimal("11111111111111111111111111111111"); // Ошибка: переполнение
        /// </code>
        /// </example>
        /// <seealso cref="Convert.ToInt32(string, int)"/>
        public static ValidationResult BinaryToDecimal(string binaryString)
        {
            try
            {
                int decimalNumber = Convert.ToInt32(binaryString, 2);
                return ValidationResult.Success(decimalNumber);
            }
            catch(FormatException)
            {
                return ValidationResult.Error("Неверный формат двоичного числа!");
            }
            catch(OverflowException)
            {
                return ValidationResult.Error("Число слишком большое!");
            }
            catch(Exception ex)
            {
                return ValidationResult.Error($"Ошибка при конвертации: {ex.Message}");
            }
        }

        /// <summary>
        /// Преобразует десятичное число в двоичную строку с фиксированной длиной.
        /// </summary>
        /// <param name="decimalNumber">Десятичное целое число.</param>
        /// <param name="bitsCount">Количество бит в выходной строке. По умолчанию 8.</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с двоичной строкой, дополненной нулями слева до указанной длины,
        /// или с сообщением об ошибке в случае неудачи.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Использует метод <see cref="Convert.ToString(int, int)"/> с основанием 2,
        /// затем дополняет результат нулями слева до указанного количества бит.
        /// </para>
        /// <para>
        /// Если исходное число требует больше бит, чем указано в <paramref name="bitsCount"/>,
        /// двоичное представление будет обрезано слева (теряются старшие биты).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// DecimalToBinary(10, 8); // Успех: "00001010"
        /// DecimalToBinary(255, 8); // Успех: "11111111"
        /// DecimalToBinary(256, 8); // Успех: "00000000" (теряется старший бит)
        /// </code>
        /// </example>
        /// <seealso cref="Convert.ToString(int, int)"/>
        /// <seealso cref="String.PadLeft(int, char)"/>
        public static ValidationResult DecimalToBinary(int decimalNumber, int bitsCount = 8)
        {
            try
            {
                string binaryString = Convert.ToString(decimalNumber, 2);
                string paddedBinary = binaryString.PadLeft(bitsCount, '0');
                return ValidationResult.Success(paddedBinary);
            }
            catch(Exception ex)
            {
                return ValidationResult.Error($"Ошибка при конвертации: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет возможность деления на указанное число (проверка на ноль).
        /// </summary>
        /// <param name="divisor">Делитель для проверки.</param>
        /// <param name="operationName">Наименование операции для сообщения об ошибке. По умолчанию "деление".</param>
        /// <returns>
        /// <see cref="ValidationResult"/> с <c>true</c>, если деление возможно (делитель не ноль),
        /// или с сообщением об ошибке в случае нулевого делителя.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Использует сравнение с <see cref="double.Epsilon"/> для определения близости к нулю.
        /// </para>
        /// <para>
        /// Первая буква в <paramref name="operationName"/> автоматически делается заглавной.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// ValidateDivision(5.0); // Успех: true
        /// ValidateDivision(0.0); // Ошибка: "Деление на ноль невозможно!"
        /// ValidateDivision(0.0, "деление"); // Ошибка: "Деление на ноль невозможно!"
        /// </code>
        /// </example>
        /// <seealso cref="Math.Abs(double)"/>
        /// <seealso cref="double.Epsilon"/>
        public static ValidationResult ValidateDivision(double divisor, string operationName = "деление")
        {
            if(Math.Abs(divisor) < double.Epsilon)
            {
                return ValidationResult.Error($"{operationName.FirstCharToUpper()} на ноль невозможно!");
            }

            return ValidationResult.Success(true);
        }

        #endregion
    }

    /// <summary>
    /// Статический класс, содержащий методы расширения для работы со строками.
    /// </summary>
    /// <remarks>
    /// Предоставляет дополнительные функции для строк, не входящие в стандартную библиотеку .NET.
    /// </remarks>
    public static class StringExtensions
    {
        /// <summary>
        /// Преобразует первую букву строки в верхний регистр.
        /// </summary>
        /// <param name="input">Исходная строка.</param>
        /// <returns>
        /// Строка с первой буквой в верхнем регистре и остальными буквами без изменений.
        /// Если строка пустая или <c>null</c>, возвращается исходная строка.
        /// </returns>
        /// <remarks>
        /// Метод учитывает только первый символ строки, не меняя регистр остальных символов.
        /// </remarks>
        /// <example>
        /// <code>
        /// "hello".FirstCharToUpper(); // "Hello" (только H станет заглавным)
        /// "HELLO".FirstCharToUpper(); // "HELLO" (ни чего не изменится)
        /// "".FirstCharToUpper(); // ""
        /// null.FirstCharToUpper(); // null
        /// </code>
        /// </example>
        public static string FirstCharToUpper(this string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}