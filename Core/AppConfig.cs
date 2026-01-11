namespace EducationalProject.Core
{
    public static class AppConfig
    {
        // Общие настройки
        public const string PressAnyKeyMessage = "Нажмите любую клавишу для продолжения...";
        public const string WelcomeMessage = "Добро пожаловать в учебный проект!";
        public const string GoodbyeMessage = "Спасибо за использование программы!";
        public const string ErrorTitle = "ОШИБКА";
        public const string SuccessTitle = "УСПЕХ";
        public const string DescriptionTitle = "ОПИСАНИЕ";

        // Консольное оформление
        public const int FrameWidth = 35;
        public const char FrameTopLeft = '╔';
        public const char FrameTopRight = '╗';
        public const char FrameBottomLeft = '╚';
        public const char FrameBottomRight = '╝';
        public const char FrameVertical = '║';
        public const char FrameHorizontal = '═';

        // Настройки калькулятора
        public static class Calculator
        {
            public const string Name = "Калькулятор";
            public static readonly string[] OperationNames = { "Сложение", "Вычитание", "Умножение", "Деление", "Остаток от деления" };
            public static readonly char[] OperationSymbols = { '+', '-', '*', '/', '%' };
        }

        // Настройки пароля
        public static class Password
        {
            public const int MinLength = 4;
            public const int MaxLength = 50;
            public const int DefaultLength = 12;
            public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
            public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string Digits = "0123456789";
            public const string SpecialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        }

        // Настройки двоичного преобразователя
        public static class Binary
        {
            public const int MinValue = 0;
            public const int MaxValue = 255;
            public const int BitsCount = 8;
        }
    }
}