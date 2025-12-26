// Подключаем пространство имен для работы с случайными числами
using System.Text;

// Объявляем пространство имен для сервисов
namespace EducationalProject.Services
{
    // Класс Генератор паролей, наследуется от BaseService
    public class PasswordGenerator : BaseService
    {
        // Реализуем свойство Name - название программы
        public override string Name => "Генератор паролей";

        // Реализуем метод Run() - основная логика программы
        public override void Run()
        {
            // Очищаем экран и показываем заголовок
            ConsoleHelper.ClearAndShowHeader(Name);

            // Массив строк с описанием программы
            string[] infoLines = {
                "Программа создает безопасные пароли разной сложности.",
                "Вы можете выбрать длину пароля и какие символы использовать.",
                "Пароли генерируются случайным образом для максимальной безопасности."
            };

            // Показываем информационный блок с описанием
            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            // Получаем длину пароля от пользователя
            int length = GetPasswordLength();

            // Получаем настройки символов от пользователя
            var settings = GetCharacterSettings();

            // Генерируем пароль
            string password = GeneratePassword(length, settings);

            // Показываем результат
            ShowResult(password, length, settings);

            // Ждем нажатия клавиши для возврата в меню
            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        // Метод для получения длины пароля от пользователя
        private int GetPasswordLength()
        {
            // Показываем меню с вариантами длины
            string[] lengthOptions = {
                "Короткий (6-8 символов) - для простых аккаунтов",
                "Средний (9-12 символов) - для почты, соцсетей",
                "Длинный (13-16 символов) - для банковских аккаунтов",
                "Очень длинный (17-20 символов) - максимальная безопасность",
                "Своя длина - задать вручную"
            };

            // Показываем меню вариантов длины
            ConsoleHelper.ShowMenu("ВЫБЕРИТЕ ДЛИНУ ПАРОЛЯ", lengthOptions);

            // Получаем выбор пользователя (1-5)
            int choice = InputValidator.GetValidMenuChoice(1, 5, ">>> Выберите вариант (1-5): ");

            // В зависимости от выбора возвращаем длину
            return choice switch
            {
                1 => new Random().Next(6, 9),    // Случайное число от 6 до 8
                2 => new Random().Next(9, 13),   // Случайное число от 9 до 12
                3 => new Random().Next(13, 17),  // Случайное число от 13 до 16
                4 => new Random().Next(17, 21),  // Случайное число от 17 до 20
                5 => GetCustomLength(),          // Пользователь сам задает длину
                _ => 12                          // По умолчанию 12 символов
            };
        }

        // Метод для получения пользовательской длины пароля
        private int GetCustomLength()
        {
            // Бесконечный цикл для получения правильного ввода
            while(true)
            {
                // Получаем ввод от пользователя
                string input = ConsoleHelper.GetInput(">>> Введите длину пароля (от 4 до 50): ");

                // Пытаемся преобразовать строку в число
                if(!int.TryParse(input, out int length))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;  // Переходим к следующей итерации цикла
                }

                // Проверяем, что длина в допустимом диапазоне
                if(length < 4)
                {
                    ConsoleHelper.ShowError("Пароль слишком короткий! Минимум 4 символа.");
                    continue;
                }

                if(length > 50)
                {
                    ConsoleHelper.ShowError("Пароль слишком длинный! Максимум 50 символов.");
                    continue;
                }

                // Возвращаем правильную длину
                return length;
            }
        }

        // Метод для получения настроек символов от пользователя
        private (bool useLower, bool useUpper, bool useDigits, bool useSpecial) GetCharacterSettings()
        {
            // Показываем меню настроек
            Console.WriteLine("\n═════════════ НАСТРОЙКИ СИМВОЛОВ ═════════════");
            Console.WriteLine("Какие символы использовать в пароле?");
            Console.WriteLine("(Рекомендуется использовать все типы для безопасности)");

            // По умолчанию все опции включены
            bool useLower = true;    // строчные буквы (a-z)
            bool useUpper = true;    // заглавные буквы (A-Z)
            bool useDigits = true;   // цифры (0-9)
            bool useSpecial = true;  // специальные символы (!@#$% и т.д.)

            // Массив с вариантами
            string[] options = {
                "1. Строчные буквы (a-z) - ДА",
                "2. Заглавные буквы (A-Z) - ДА",
                "3. Цифры (0-9) - ДА",
                "4. Специальные символы (!@#$%^&*) - ДА",
                "5. Сгенерировать пароль с текущими настройками"
            };

            // Бесконечный цикл для настройки
            while(true)
            {
                // Показываем текущие настройки
                Console.WriteLine("\nТекущие настройки:");
                Console.WriteLine($"  Строчные буквы: {(useLower ? "ДА" : "НЕТ")}");
                Console.WriteLine($"  Заглавные буквы: {(useUpper ? "ДА" : "НЕТ")}");
                Console.WriteLine($"  Цифры: {(useDigits ? "ДА" : "НЕТ")}");
                Console.WriteLine($"  Специальные символы: {(useSpecial ? "ДА" : "НЕТ")}");

                // Показываем меню
                Console.WriteLine("\nВыберите опцию для изменения:");
                foreach(string option in options)
                {
                    Console.WriteLine(option);
                }

                // Получаем выбор пользователя (1-5)
                int choice = InputValidator.GetValidMenuChoice(1, 5, ">>> Ваш выбор (1-5): ");

                // Обрабатываем выбор
                switch(choice)
                {
                    case 1:
                        useLower = !useLower;  // Меняем на противоположное (ДА ↔ НЕТ)
                        ConsoleHelper.ShowInfo($"Строчные буквы: {(useLower ? "ВКЛЮЧЕНЫ" : "ВЫКЛЮЧЕНЫ")}");
                        break;

                    case 2:
                        useUpper = !useUpper;
                        ConsoleHelper.ShowInfo($"Заглавные буквы: {(useUpper ? "ВКЛЮЧЕНЫ" : "ВЫКЛЮЧЕНЫ")}");
                        break;

                    case 3:
                        useDigits = !useDigits;
                        ConsoleHelper.ShowInfo($"Цифры: {(useDigits ? "ВКЛЮЧЕНЫ" : "ВЫКЛЮЧЕНЫ")}");
                        break;

                    case 4:
                        useSpecial = !useSpecial;
                        ConsoleHelper.ShowInfo($"Специальные символы: {(useSpecial ? "ВКЛЮЧЕНЫ" : "ВЫКЛЮЧЕНЫ")}");
                        break;

                    case 5:
                        // Проверяем, что хотя бы один тип символов выбран
                        if(!useLower && !useUpper && !useDigits && !useSpecial)
                        {
                            ConsoleHelper.ShowError("Должен быть выбран хотя бы один тип символов!");
                            continue;  // Продолжаем цикл
                        }

                        // Возвращаем настройки
                        return (useLower, useUpper, useDigits, useSpecial);
                }
            }
        }

        // Метод для генерации пароля
        private string GeneratePassword(int length, (bool useLower, bool useUpper, bool useDigits, bool useSpecial) settings)
        {
            // Создаем объект для генерации случайных чисел
            Random random = new Random();

            // Создаем StringBuilder для эффективного построения строки
            StringBuilder password = new StringBuilder();

            // Строки с доступными символами для каждого типа
            string lowerCase = "abcdefghijklmnopqrstuvwxyz";          // Строчные буквы
            string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";          // Заглавные буквы
            string digits = "0123456789";                            // Цифры
            string specialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";       // Специальные символы

            // Создаем общую строку со всеми выбранными символами
            string allChars = "";

            // Добавляем символы в общую строку в зависимости от настроек
            if(settings.useLower) allChars += lowerCase;     // Если выбраны строчные - добавляем
            if(settings.useUpper) allChars += upperCase;     // Если выбраны заглавные - добавляем
            if(settings.useDigits) allChars += digits;       // Если выбраны цифры - добавляем
            if(settings.useSpecial) allChars += specialChars; // Если выбраны спецсимволы - добавляем

            // Гарантируем, что в пароле будет хотя бы по одному символу из каждого выбранного типа
            if(settings.useLower)
                password.Append(lowerCase[random.Next(lowerCase.Length)]);  // Добавляем случайную строчную букву

            if(settings.useUpper)
                password.Append(upperCase[random.Next(upperCase.Length)]);  // Добавляем случайную заглавную букву

            if(settings.useDigits)
                password.Append(digits[random.Next(digits.Length)]);        // Добавляем случайную цифру

            if(settings.useSpecial)
                password.Append(specialChars[random.Next(specialChars.Length)]); // Добавляем случайный спецсимвол

            // Добираем остальные символы до нужной длины
            for(int i = password.Length; i < length; i++)  // i начинается с текущей длины
            {
                // Выбираем случайный символ из общей строки и добавляем к паролю
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Перемешиваем символы в пароле для большей случайности
            string shuffledPassword = ShuffleString(password.ToString(), random);

            // Возвращаем готовый пароль
            return shuffledPassword;
        }

        // Метод для перемешивания символов в строке
        private string ShuffleString(string input, Random random)
        {
            // Преобразуем строку в массив символов
            char[] chars = input.ToCharArray();

            // Алгоритм Фишера-Йетса для перемешивания
            for(int i = chars.Length - 1; i > 0; i--)  // Идем с конца к началу
            {
                // Выбираем случайный индекс от 0 до i
                int j = random.Next(i + 1);

                // Меняем местами символы на позициях i и j
                char temp = chars[i];  // Сохраняем символ на позиции i во временную переменную
                chars[i] = chars[j];   // На позицию i ставим символ с позиции j
                chars[j] = temp;       // На позицию j ставим сохраненный символ
            }

            // Создаем новую строку из перемешанных символов
            return new string(chars);
        }

        // Метод для показа результата
        private void ShowResult(string password, int length, (bool useLower, bool useUpper, bool useDigits, bool useSpecial) settings)
        {
            // Показываем заголовок результата
            Console.WriteLine("\n════════════════ РЕЗУЛЬТАТ ════════════════");

            // Показываем сгенерированный пароль (зеленым цветом)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВаш пароль ({length} символов):");
            Console.WriteLine($"\n  {password}");
            Console.ResetColor();

            // Показываем информацию о пароле
            Console.WriteLine($"\nИнформация о пароле:");
            Console.WriteLine($"  • Длина: {length} символов");
            Console.WriteLine($"  • Использованы символы:");
            Console.WriteLine($"    - Строчные буквы: {(settings.useLower ? "✓" : "✗")}");
            Console.WriteLine($"    - Заглавные буквы: {(settings.useLower ? "✓" : "✗")}");
            Console.WriteLine($"    - Цифры: {(settings.useDigits ? "✓" : "✗")}");
            Console.WriteLine($"    - Специальные символы: {(settings.useSpecial ? "✓" : "✗")}");

            // Показываем оценку сложности пароля
            Console.WriteLine($"\n  • Сложность пароля: {GetPasswordStrength(password)}");

            // Показываем советы по безопасности
            Console.WriteLine($"\nСоветы по безопасности:");
            Console.WriteLine($"  • Никому не сообщайте свой пароль");
            Console.WriteLine($"  • Используйте разные пароли для разных аккаунтов");
            Console.WriteLine($"  • Регулярно меняйте пароли (раз в 3-6 месяцев)");
            Console.WriteLine($"  • Используйте менеджер паролей для хранения");
        }

        // Метод для оценки сложности пароля
        private string GetPasswordStrength(string password)
        {
            // Константы для оценки сложности пароля
            const int MIN_SECURE_PASSWORD_LENGTH = 8;   // Минимальная длина безопасного пароля
            const int TYPES_COUNT_ONE = 1;              // Используется один тип символов
            const int TYPES_COUNT_TWO = 2;              // Используется два типа символов
            const int TYPES_COUNT_THREE = 3;            // Используется три типа символов
            const int TYPES_COUNT_FOUR = 4;             // Используются все четыре типов символов

            // Описания сложности пароля
            const string STRENGTH_WEAK = "СЛАБЫЙ";
            const string STRENGTH_MEDIUM = "СРЕДНИЙ";
            const string STRENGTH_GOOD = "ХОРОШИЙ";
            const string STRENGTH_EXCELLENT = "ОТЛИЧНЫЙ";

            // Проверяем длину пароля
            if(password.Length < MIN_SECURE_PASSWORD_LENGTH)
                return $"{STRENGTH_WEAK} (слишком короткий)";

            // Проверяем наличие разных типов символов
            bool hasLower = false;  // Есть ли строчные буквы
            bool hasUpper = false;  // Есть ли заглавные буквы
            bool hasDigit = false;  // Есть ли цифры
            bool hasSpecial = false; // Есть ли спецсимволы

            // Перебираем все символы пароля
            foreach(char symbol in password)
            {
                if(char.IsLower(symbol)) hasLower = true;       // Если символ строчный
                else if(char.IsUpper(symbol)) hasUpper = true;  // Если символ заглавный
                else if(char.IsDigit(symbol)) hasDigit = true;  // Если символ цифра
                else hasSpecial = true;                         // Иначе спецсимвол
            }

            // Подсчитываем сколько типов символов используется
            int typesCount = 0;
            if(hasLower) typesCount++;
            if(hasUpper) typesCount++;
            if(hasDigit) typesCount++;
            if(hasSpecial) typesCount++;

            // Определяем сложность пароля в зависимости от количества используемых типов символов
            string strength; // Надежность пароля
            switch(typesCount)
            {
                // Только один тип символов
                case TYPES_COUNT_ONE:
                    strength = $"{STRENGTH_WEAK} (используется только один тип символов)";
                    break;

                // Два типа символов
                case TYPES_COUNT_TWO:
                    strength = $"{STRENGTH_MEDIUM} (можно улучшить)";
                    break;

                // Три типа символов
                case TYPES_COUNT_THREE:
                    strength = $"{STRENGTH_GOOD} (достаточно безопасно)";
                    break;

                // Все четыре типа символов
                case TYPES_COUNT_FOUR:
                    strength = $"{STRENGTH_EXCELLENT} (очень безопасно)";
                    break;

                // На всякий случай
                default:
                    strength = "НЕИЗВЕСТНО";
                    break;
            }

            return strength;
        }
    }
}