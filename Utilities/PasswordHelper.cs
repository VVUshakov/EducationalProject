namespace EducationalProject.Utilities
{
    public static class PasswordHelper
    {
        // Строки с доступными символами
        public const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";
        public const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string DIGITS = "0123456789";
        public const string SPECIAL_CHARS = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        // Константы для оценки сложности пароля
        const int MIN_SECURE_PASSWORD_LENGTH = 8;   // Минимальная длина безопасного пароля
        const int TYPES_COUNT_ONE = 1;              // Используется один тип символов
        const int TYPES_COUNT_TWO = 2;              // Используется два типа символов
        const int TYPES_COUNT_THREE = 3;            // Используется три типа символов
        const int TYPES_COUNT_FOUR = 4;             // Используются все четыре типов символов

        // Оценка сложности (надежности) пароля
        public static string GetPasswordStrength(string password)
        {
            const int MIN_SECURE_LENGTH = 8;

            // Проверяем длину
            if(password.Length < MIN_SECURE_LENGTH)
                return "СЛАБЫЙ (слишком короткий)";

            // Проверяем типы символов
            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            foreach(char symbol in password)
            {
                if(char.IsLower(symbol)) hasLower = true;
                else if(char.IsUpper(symbol)) hasUpper = true;
                else if(char.IsDigit(symbol)) hasDigit = true;
                else hasSpecial = true;
            }

            // Считаем сколько типов символов используется в пароде
            int typesCount = 0;
            if(hasLower) typesCount++;
            if(hasUpper) typesCount++;
            if(hasDigit) typesCount++;
            if(hasSpecial) typesCount++;

            // Определяем сложность пароля в зависимости от количества используемых типов символов
            switch(typesCount)
            {
                case TYPES_COUNT_ONE: return "СЛАБЫЙ (используется только один тип символов)"; // Только один тип символов
                case TYPES_COUNT_TWO: return "СРЕДНИЙ (можно улучшить)"; // Два типа символов
                case TYPES_COUNT_THREE: return "ХОРОШИЙ (достаточно безопасно)"; // Три типа символов
                case TYPES_COUNT_FOUR: return "ОТЛИЧНЫЙ (очень безопасно)"; // Все четыре типа символов
                default: return "НЕИЗВЕСТНО"; // На всякий случай
            }
        }
    }
}