using EducationalProject.Utilities; // Подключаем пространство имен для работы с случайными числами

// Объявляем пространство имен для сервисов
namespace EducationalProject.Services
{
    // Структура для хранения настроек символов
    public struct CharacterSettings
    {
        public bool UseLower;    // строчные буквы (a-z)
        public bool UseUpper;    // заглавные буквы (A-Z)
        public bool UseDigits;   // цифры (0-9)
        public bool UseSpecial;  // специальные символы (!@#$% и т.д.)
    }

    // Структура для хранения результата генерации пароля
    public struct PasswordResult
    {
        public string Password;           // Сгенерированный пароль
        public int Length;                // Длина пароля
        public CharacterSettings Settings; // Настройки символов
    }

    // Класс Генератор паролей, наследуется от BaseService
    public class PasswordGenerator : BaseService
    {
        // Константы для текстовых сообщений
        private const string ENABLED_TEXT = "ВКЛЮЧЕНЫ";   // Текст для отображения, когда опция включена
        private const string DISABLED_TEXT = "ВЫКЛЮЧЕНЫ"; // Текст для отображения, когда опция выключена
        private const string YES_TEXT = "ДА";             // Текст для отображения "ДА"
        private const string NO_TEXT = "НЕТ";             // Текст для отображения "НЕТ"

        // Константы для минимальной и максимальной длины пароля
        private const int MIN_PASSWORD_LENGTH = 4;     // Самая короткая допустимая длина пароля (меньше нельзя)
        private const int MAX_PASSWORD_LENGTH = 50;    // Самая длинная допустимая длина пароля (больше нельзя)                                                      
        private const int INCLUDE_UPPER_BOUND = 1;     /* Константа для метода Random.Next() - чтобы включить верхнюю границу диапазона
                                                        * Метод Random.Next(minValue, maxValue) возвращает число в диапазоне [minValue, maxValue)
                                                        * - то есть включая minValue, но исключая maxValue. 
                                                        * Чтобы включить и максимальное значение, нужно добавить 1.
                                                        */

        // Константы для предопределенных длин паролей
        private const int MIN_SHORT_LENGTH = 6;        // Минимальная длина короткого пароля (для опции 1)
        private const int MAX_SHORT_LENGTH = 8;        // Максимальная длина короткого пароля (для опции 1)
        private const int MIN_MEDIUM_LENGTH = 9;       // Минимальная длина среднего пароля (для опции 2)
        private const int MAX_MEDIUM_LENGTH = 12;      // Максимальная длина среднего пароля (для опции 2)
        private const int MIN_LONG_LENGTH = 13;        // Минимальная длина длинного пароля (для опции 3)
        private const int MAX_LONG_LENGTH = 16;        // Максимальная длина длинного пароля (для опции 3)
        private const int MIN_EXTRA_LONG_LENGTH = 17;  // Минимальная длина очень длинного пароля (для опции 4)
        private const int MAX_EXTRA_LONG_LENGTH = 20;  // Максимальная длина очень длинного пароля (для опции 4)
        private const int DEFAULT_LENGTH = 12;         // Длина пароля по умолчанию (если что-то пошло не так)

        // Константы для вариантов длины пароля в меню
        private const int MENU_LENGTH_OPTION_COUNT = 5;    // Общее количество пунктов в меню выбора длины (от 1 до 5)
        private const int SHORT_LENGTH_OPTION = 1;         // Номер пункта меню для короткого пароля (6-8 символов)
        private const int MEDIUM_LENGTH_OPTION = 2;        // Номер пункта меню для среднего пароля (9-12 символов)
        private const int LONG_LENGTH_OPTION = 3;          // Номер пункта меню для длинного пароля (13-16 символов)
        private const int EXTRA_LONG_LENGTH_OPTION = 4;    // Номер пункта меню для очень длинного пароля (17-20 символов)
        private const int CUSTOM_LENGTH_OPTION = 5;        // Номер пункта меню для пользовательской длины

        // Константы для вариантов настроек символов в меню
        private const int MIN_MENU_CHOICE = 1;                 // Минимальный номер пункта меню настроек
        private const int MAX_MENU_CHOICE = 5;                 // Максимальный номер пункта меню настроек
        private const int LOWER_CASE_OPTION = 1;               // Пункт меню для строчных букв
        private const int UPPER_CASE_OPTION = 2;               // Пункт меню для заглавных букв
        private const int DIGITS_OPTION = 3;                   // Пункт меню для цифр
        private const int SPECIAL_CHARS_OPTION = 4;            // Пункт меню для спецсимволов
        private const int GENERATE_PASSWORD_OPTION = 5;        // Пункт меню для генерации пароля

        public override string Name => "Генератор паролей"; // Реализуем свойство Name - название программы

        // Реализуем метод Run() - основная логика программы        
        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name); // Очищаем экран и показываем заголовок

            // Массив строк с описанием программы
            string[] infoLines = [
                "Программа создает безопасные пароли разной сложности.",
                "Вы можете выбрать длину пароля и какие символы использовать.",
                "Пароли генерируются случайным образом для максимальной безопасности.",
            ];

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines); // Показываем информационный блок с описанием

            int length = GetPasswordLength(); // Получаем длину пароля от пользователя
            CharacterSettings settings = GetCharacterSettings(); // Получаем настройки символов от пользователя
            string password = GeneratePassword(length, settings); // Генерируем пароль

            // Создаем структуру с результатом
            PasswordResult result;
            result.Password = password;
            result.Length = length;
            result.Settings = settings;

            ShowResult(result); // Показываем результат, передавая структуру
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN); // Ждем нажатия клавиши для возврата в меню
        }

        // Метод для получения длины пароля от пользователя
        private int GetPasswordLength()
        {
            // Показываем меню с вариантами длины
            string[] lengthOptions = [
                "Короткий (6-8 символов) - для простых аккаунтов",
                "Средний (9-12 символов) - для почты, соцсетей",
                "Длинный (13-16 символов) - для банковских аккаунтов",
                "Очень длинный (17-20 символов) - максимальная безопасность",
                "Своя длина - задать вручную",
            ];

            ConsoleHelper.ShowMenu("ВЫБЕРИТЕ ДЛИНУ ПАРОЛЯ", lengthOptions); // Массив строк с вариантами длины

            // Получаем выбор пользователя (1-5)
            int choice = InputValidator.GetValidMenuChoice(
                minValue: MIN_MENU_CHOICE, // Минимальный номер пункта меню настроек
                maxValue: MENU_LENGTH_OPTION_COUNT, // Общее количество пунктов в меню выбора длины (от 1 до 5)
                prompt: ">>> Выберите вариант (1-5): "
            );

            Random random = new Random();               // Создаем объект для генерации случайных чисел
            int passwordLength = 0;                     // Создаем переменную для хранения длины пароля

            // В зависимости от выбора пользователя возвращаем длину
            switch(choice)
            {
                case SHORT_LENGTH_OPTION:               // Если выбран номер пункта меню для короткого пароля (6-8 символов)
                    passwordLength = random.Next(       // Сгенерировать случайное число от 6 до 8
                        minValue: MIN_SHORT_LENGTH,
                        maxValue: MAX_SHORT_LENGTH + INCLUDE_UPPER_BOUND);
                    break;                              // выйти из switch

                case MEDIUM_LENGTH_OPTION:              // Если выбран номер пункта меню для среднего пароля (9-12 символов)
                    passwordLength = random.Next(       // Сгенерировать случайное число от 9 до 12
                        minValue: MIN_MEDIUM_LENGTH,
                        maxValue: MAX_MEDIUM_LENGTH + INCLUDE_UPPER_BOUND);
                    break;                              // выйти из switch

                case LONG_LENGTH_OPTION:                // Если выбран номер пункта меню для длинного пароля (13-16 символов)
                    passwordLength = random.Next(       // Сгенерировать случайное число от 13 до 16
                        minValue: MIN_LONG_LENGTH,
                        maxValue: MAX_LONG_LENGTH + INCLUDE_UPPER_BOUND);
                    break;                              // выйти из switch

                case EXTRA_LONG_LENGTH_OPTION:          // Если выбран номер пункта меню для очень длинного пароля (17-20 символов)
                    passwordLength = random.Next(       // Сгенерировать случайное число от 17 до 20
                        minValue: MIN_EXTRA_LONG_LENGTH,
                        maxValue: MAX_EXTRA_LONG_LENGTH + INCLUDE_UPPER_BOUND);
                    break;                              // выйти из switch

                case CUSTOM_LENGTH_OPTION:              // Если выбран номер пункта меню пользовательской длины
                    passwordLength = GetCustomLength(); // Получить пользовательскую длину пароля
                    break;                              // выйти из switch

                default:                                // Если нет подходящего значения в switch
                    passwordLength = DEFAULT_LENGTH;    // По умолчанию вернуть значение длины пароля в 12 символов
                    break;                              // выйти из switch
            }

            return passwordLength;                      // Возвращаем значение длины пароля
        }

        // Метод для получения пользовательской длины пароля
        private int GetCustomLength()
        {
            // Бесконечный цикл для получения правильного ввода
            while(true)
            {
                string input = ConsoleHelper.GetInput($"Введите длину пароля (от {MIN_PASSWORD_LENGTH} до {MAX_PASSWORD_LENGTH}): "); // Получаем ввод от пользователя

                // Пытаемся преобразовать строку в число
                if(!int.TryParse(input, out int length))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!"); // Показываем ошибку
                    continue;  // Прервать выполнение кода и перейти к следующей итерации цикла
                }

                // Проверяем, что длина в допустимом диапазоне
                if(length < MIN_PASSWORD_LENGTH)
                {
                    ConsoleHelper.ShowError($"Пароль слишком короткий! Минимум {MIN_PASSWORD_LENGTH} символов."); // Показываем ошибку
                    continue; // Прервать выполнение кода и перейти к следующей итерации цикла
                }

                if(length > MAX_PASSWORD_LENGTH)
                {
                    ConsoleHelper.ShowError($"Пароль слишком длинный! Максимум {MAX_PASSWORD_LENGTH} символов."); // Показываем ошибку
                    continue; // Прервать выполнение кода и перейти к следующей итерации цикла
                }

                return length; // Возвращаем правильную длину
            }
        }

        // Метод для получения настроек символов от пользователя
        private CharacterSettings GetCharacterSettings()
        {
            // Показываем меню настроек
            Console.WriteLine("\n═════════════ НАСТРОЙКИ СИМВОЛОВ ═════════════");
            Console.WriteLine("Какие символы использовать в пароле?");
            Console.WriteLine("(Рекомендуется использовать все типы для безопасности)");

            // Массив с вариантами символов (опции/сложность пароля)
            string[] options = [
                "1. Строчные буквы (a-z) - ДА",
                "2. Заглавные буквы (A-Z) - ДА",
                "3. Цифры (0-9) - ДА",
                "4. Специальные символы (!@#$%^&*) - ДА",
                "5. Сгенерировать пароль с текущими настройками",
            ];

            // Создаем структуру для настроек и Инициализируем поля структуры (по умолчанию все опции включены)
            CharacterSettings settings;
            settings.UseLower = true;    // строчные буквы (a-z)
            settings.UseUpper = true;    // заглавные буквы (A-Z)
            settings.UseDigits = true;   // цифры (0-9)
            settings.UseSpecial = true;  // специальные символы (!@#$% и т.д.)

            // Бесконечный цикл для настройки опций/сложности пароля
            while(true)
            {
                // Показываем текущие настройки
                Console.WriteLine("\nТекущие настройки:");
                Console.WriteLine($"  Строчные буквы: {(settings.UseLower ? YES_TEXT : NO_TEXT)}");
                Console.WriteLine($"  Заглавные буквы: {(settings.UseUpper ? YES_TEXT : NO_TEXT)}");
                Console.WriteLine($"  Цифры: {(settings.UseDigits ? YES_TEXT : NO_TEXT)}");
                Console.WriteLine($"  Специальные символы: {(settings.UseSpecial ? YES_TEXT : NO_TEXT)}");

                // Показываем меню
                Console.WriteLine("\nВыберите опцию для изменения:");
                foreach(string option in options)
                {
                    Console.WriteLine(option);
                }

                // Получаем выбор пользователя (1-5)
                int choice = InputValidator.GetValidMenuChoice(
                    minValue: MIN_MENU_CHOICE, // Минимальный номер пункта меню настроек
                    maxValue: MAX_MENU_CHOICE, // Максимальный номер пункта меню настроек
                    prompt: ">>> Ваш выбор (1-5): "
                );

                // Обрабатываем выбор
                switch(choice)
                {
                    case LOWER_CASE_OPTION: // Если выбран пункт меню для строчных букв
                        settings.UseLower = !settings.UseLower;  // Меняем на противоположное (ДА <-> НЕТ)
                        string lowerStatus = settings.UseLower ? ENABLED_TEXT : DISABLED_TEXT; // Показываем информационный блок с описанием вкл/выкл опция
                        ConsoleHelper.ShowInfo($"Строчные буквы: {lowerStatus}"); // Показываем информацию
                        break; // выйти из switch

                    case UPPER_CASE_OPTION: // Если выбран пункт меню для заглавных букв
                        settings.UseUpper = !settings.UseUpper; // Меняем на противоположное (ДА <-> НЕТ)
                        string upperStatus = settings.UseUpper ? ENABLED_TEXT : DISABLED_TEXT; // Показываем информационный блок с описанием вкл/выкл опция
                        ConsoleHelper.ShowInfo($"Заглавные буквы: {upperStatus}"); // Показываем информацию
                        break; // выйти из switch

                    case DIGITS_OPTION: // Если выбран пункт меню для цифр
                        settings.UseDigits = !settings.UseDigits; // Меняем на противоположное (ДА <-> НЕТ)
                        string digitsStatus = settings.UseDigits ? ENABLED_TEXT : DISABLED_TEXT; // Показываем информационный блок с описанием вкл/выкл опция
                        ConsoleHelper.ShowInfo($"Цифры: {digitsStatus}"); // Показываем информацию
                        break; // выйти из switch

                    case SPECIAL_CHARS_OPTION: // Если выбран пункт меню для спецсимволов
                        settings.UseSpecial = !settings.UseSpecial; // Меняем на противоположное (ДА <-> НЕТ)
                        string specialStatus = settings.UseSpecial ? ENABLED_TEXT : DISABLED_TEXT; // Показываем информационный блок с описанием вкл/выкл опция
                        ConsoleHelper.ShowInfo($"Специальные символы: {specialStatus}"); // Показываем информацию
                        break; // выйти из switch

                    case GENERATE_PASSWORD_OPTION: // Если выбран пункт меню для генерации пароля
                        // Проверяем, что хотя бы один тип символов выбран
                        if(!settings.UseLower && !settings.UseUpper && !settings.UseDigits && !settings.UseSpecial)
                        {
                            ConsoleHelper.ShowError("Должен быть выбран хотя бы один тип символов!"); // Показываем информационный блок с ошибкой
                            continue;  // Прервать выполнение кода и перейти к следующей итерации цикла
                        }

                        return settings; // Возвращаем готовую структуру с настройками
                }
            }
        }

        // Метод для генерации пароля
        private string GeneratePassword(int length, CharacterSettings settings)
        {
            Random random = new Random(); // Создаем объект для генерации случайных чисел

            string password = ""; // Начинаем с пустой строки

            // Добавляем гарантированные символы (гарантируем, что в пароле будет хотя бы по одному символу из каждого выбранного типа)
            if(settings.UseLower) // Если в настройках/опциях помечено использование строчных букв
                password += PasswordHelper.LOWER_CASE[random.Next(PasswordHelper.LOWER_CASE.Length)]; // Добавляем случайную строчную букву

            if(settings.UseUpper) // Если в настройках/опциях помечено использование заглавных букв
                password += PasswordHelper.UPPER_CASE[random.Next(PasswordHelper.UPPER_CASE.Length)]; // Добавляем случайную заглавную букву

            if(settings.UseDigits) // Если в настройках/опциях помечено использование цифр
                password += PasswordHelper.DIGITS[random.Next(PasswordHelper.DIGITS.Length)]; // Добавляем случайную цифру

            if(settings.UseSpecial) // Если в настройках/опциях помечено использование спецсимволов
                password += PasswordHelper.SPECIAL_CHARS[random.Next(PasswordHelper.SPECIAL_CHARS.Length)]; // Добавляем случайный спецсимвол

            // Добавляем символы в общую строку в зависимости от настроек/опций
            string allChars = ""; // Создаем общую строку со всеми выбранными символами
            if(settings.UseLower) allChars += PasswordHelper.LOWER_CASE;        // Если выбраны строчные - добавляем
            if(settings.UseUpper) allChars += PasswordHelper.UPPER_CASE;        // Если выбраны заглавные - добавляем
            if(settings.UseDigits) allChars += PasswordHelper.DIGITS;           // Если выбраны цифры - добавляем
            if(settings.UseSpecial) allChars += PasswordHelper.SPECIAL_CHARS;   // Если выбраны спецсимволы - добавляем

            // Добираем остальные символы до нужной длины
            while(password.Length < length)
            {
                password += allChars[random.Next(allChars.Length)]; // Выбираем случайный символ из общей строки и добавляем к паролю
            }

            string shuffledPassword = StringHelper.ShuffleString(password, random); // Перемешиваем символы в пароле для большей случайности

            return shuffledPassword; // Возвращаем готовый пароль
        }

        // Метод для показа результата
        private void ShowResult(PasswordResult result)
        {
            // Показываем заголовок результата
            Console.WriteLine("\n════════════════ РЕЗУЛЬТАТ ════════════════");

            // Показываем сгенерированный пароль (зеленым цветом)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВаш пароль ({result.Length} символов):");
            Console.WriteLine($"\n  {result.Password}");
            Console.ResetColor();

            // Показываем информацию о пароле
            Console.WriteLine($"\nИнформация о пароле:");
            Console.WriteLine($"  • Длина: {result.Length} символов");
            Console.WriteLine($"  • Использованы символы:");
            Console.WriteLine($"    - Строчные буквы: {(result.Settings.UseLower ? "✓" : "✗")}");
            Console.WriteLine($"    - Заглавные буквы: {(result.Settings.UseUpper ? "✓" : "✗")}");
            Console.WriteLine($"    - Цифры: {(result.Settings.UseDigits ? "✓" : "✗")}");
            Console.WriteLine($"    - Специальные символы: {(result.Settings.UseSpecial ? "✓" : "✗")}");

            // Показываем оценку сложности пароля
            Console.WriteLine($"\n  • Сложность пароля: {PasswordHelper.GetPasswordStrength(result.Password)}");

            // Показываем советы по безопасности
            Console.WriteLine($"\nСоветы по безопасности:");
            Console.WriteLine($"  • Никому не сообщайте свой пароль");
            Console.WriteLine($"  • Используйте разные пароли для разных аккаунтов");
            Console.WriteLine($"  • Регулярно меняйте пароли (раз в 3-6 месяцев)");
            Console.WriteLine($"  • Используйте менеджер паролей для хранения");
        }
    }
}