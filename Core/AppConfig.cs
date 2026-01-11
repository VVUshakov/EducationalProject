namespace EducationalProject.Core
{
    // Настройки приложения
    public static class AppConfig
    {
        // Настройки сообщений приложения
        public static class MessagesConfig
        {
            public const string WelcomeMessage = "Добро пожаловать в учебный проект!";
            public const string GoodbyeMessage = "Спасибо за использование программы!";
            public const string PressAnyKey = "Нажмите любую клавишу для продолжения...";
            public const string ErrorTitle = "ОШИБКА";
            public const string SuccessTitle = "УСПЕХ";
            public const string InfoTitle = "ИНФОРМАЦИЯ";
            public const string DescriptionTitle = "ОПИСАНИЕ";
        }

        // Настройки отображения рамок
        public static class DisplayConfig
        {
            public const int FrameWidth = 35;
            public const char FrameTopLeft = '╔';
            public const char FrameTopRight = '╗';
            public const char FrameBottomLeft = '╚';
            public const char FrameBottomRight = '╝';
            public const char FrameVertical = '║';
            public const char FrameHorizontal = '═';
        }

        // Настройки калькулятора
        public static class CalculatorConfig
        {
            public const string Name = "Калькулятор";

            public static readonly string[] OperationsNames = {
                "Сложение",
                "Вычитание",
                "Умножение",
                "Деление",
                "Остаток от деления"
            };

            public static readonly char[] OperationsSymbols = {
                '+',
                '-',
                '*',
                '/',
                '%'
            };
        }

        // Настройки двоичного калькулятора
        public static class BinaryConverterConfig
        {
            // Основная информация
            public const string Name = "Конвертер двоичных чисел";
            public const string Description = "Перевод чисел между десятичной и двоичной системами счисления";

            // Числовые параметры
            public const int MinValue = 0;
            public const int MaxValue = 255;
            public const int BitsCount = 8;

            // Сообщения
            public static class Messages
            {
                // Варианты конвертации
                public const string DecimalToBinary = "Десятичное → Двоичное";
                public const string BinaryToDecimal = "Двоичное → Десятичное";

                // Подсказки ввода
                public const string Title = "Выберите тип преобразования";

                public const string DecimalHint = "Введите десятичное число ({0}-{1}):";
                public const string BinaryHint = "Введите двоичное число ({0} бит):";
                public const string BinaryExample = "Например: {0} или {1}";

                // Информационные сообщения
                public const string InfoDecimalSystem = "• Десятичная система: цифры 0-9";
                public const string InfoBinarySystem = "• Двоичная система: только 0 и 1";
                public static string InfoByteSize(int bits) => $"• 1 байт = {bits} бит";
                public static string InfoMaxValue(int max, string binary) =>
                    $"• Максимальное значение: {max} ({binary} в двоичной)";
                public const string InfoCommonPattern = "• Часто используется: 1010 1010 = 170";

                // Результаты
                public const string ResultDecimal = "Десятичное: {0}";
                public const string ResultBinary = "Двоичное: {0}";
                public const string ResultFormatted = "Форматированное: {0}";
            }

            // Настройки отображения и форматирования
            public static class Display
            {
                public const int GroupSize = 4;
                public const char GroupSeparator = ' ';
                public const bool ShowSeparatorInBinary = true;
                public const string ExampleFormattedBinary = "1010 1010";
            }

            // Настройки валидации
            public static class Validation
            {
                public const bool AllowSpaces = true;
                public const bool TrimInput = true;
                public const bool AutoPadWithZeros = true;
            }
        }

        public static class NameEncoderConfig
        {
            public const string Name = "Кодировщик имен";
            public const int MaxNameLength = 50;
        }

        // Настройки генератора паролей        
        public static class PasswordGeneratorConfig
        {
            public const string Name = "Генератор паролей";
            public const int MinLength = 4;
            public const int MaxLength = 50;
            public const int DefaultLength = 12;

            public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
            public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string Digits = "0123456789";
            public const string SpecialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        }
    }
}