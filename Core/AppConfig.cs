namespace EducationalProject.Core
{
    public static class AppConfig
    {
        // Общие настройки
        public const int MaxPasswordLength = 50;
        public const int MinPasswordLength = 4;
        public const string Currency = "руб.";

        // Сообщения
        public static class Messages
        {
            public const string PressAnyKey = "Нажмите любую клавишу для продолжения...";
            public const string Welcome = "Добро пожаловать в учебный проект!";
            public const string Goodbye = "Спасибо за использование программы!";
        }

        // Диапазоны значений
        public static class Ranges
        {
            public const int BinaryMin = 0;
            public const int BinaryMax = 255;
            public const int BinaryBits = 8;
        }

        // Символы
        public static class Characters
        {
            public const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
            public const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            public const string Digits = "0123456789";
            public const string Special = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        }
    }
}
