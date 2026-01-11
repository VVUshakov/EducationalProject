namespace EducationalProject.Core
{
    public static class AppConfig // TO DO: Требуется рефакторин повторяющегося кода
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

        // Настройки названий пунктов Главного меню
        public static class MenuTitles
        {
            public const string Calculator = "Калькулятор";
            public const string BinaryConverter = "Двоичный преобразователь";
            public const string NameEncoder = "Кодировщик имён";
            public const string PasswordGenerator = "Генератор паролей";
            public const string AssignmentDemo = "Демо: Операции присваивания";
        }

        // Настройки общих сообщений
        public static class AppMessages
        {
            public const string SelectProgram = "Выберите программу для запуска:";
            public const string InvalidChoice = "Неверный выбор! Попробуйте еще раз.";
            public const string PressToContinue = "Нажмите любую клавишу для продолжения...";
            public const string Exiting = "Выход из программы...";
        }
    }
}