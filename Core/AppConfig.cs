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
            public const string Name = "Конвертер двоичных чисел";
            public const int MinValue = 0;
            public const int MaxValue = 255;
            public const int BitsCount = 8;
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