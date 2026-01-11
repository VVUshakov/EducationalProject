using EducationalProject.Core;
using EducationalProject.Models;
using System.Text;

namespace EducationalProject.Services
{
    public class PasswordGeneratorService
    {
        private readonly Random _random;

        public PasswordGeneratorService()
        {
            _random = new Random();
        }

        public PasswordResult GeneratePassword(PasswordSettings settings, int length)
        {
            if(!settings.HasAnySetting || length < AppConfig.PasswordGeneratorConfig.MinLength)
            {
                return new PasswordResult { Password = null };
            }

            // 1. Начинаем с обязательных символов
            StringBuilder password = new StringBuilder();

            if(settings.UseLower)
                password.Append(GetRandomChar(AppConfig.PasswordGeneratorConfig.LowerCase));

            if(settings.UseUpper)
                password.Append(GetRandomChar(AppConfig.PasswordGeneratorConfig.UpperCase));

            if(settings.UseDigits)
                password.Append(GetRandomChar(AppConfig.PasswordGeneratorConfig.Digits));

            if(settings.UseSpecial)
                password.Append(GetRandomChar(AppConfig.PasswordGeneratorConfig.SpecialChars));

            // 2. Добавляем оставшиеся символы
            string allChars = GetAllChars(settings);

            while(password.Length < length)
            {
                password.Append(GetRandomChar(allChars));
            }

            // 3. Перемешиваем пароль
            string finalPassword = ShuffleString(password.ToString());

            // 4. Определяем надежность
            string strength = GetPasswordStrength(finalPassword, settings);

            return new PasswordResult
            {
                Password = finalPassword,
                Length = length,
                Settings = settings,
                Strength = strength
            };
        }

        public int GetRandomLength(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        private char GetRandomChar(string characters)
        {
            return characters[_random.Next(characters.Length)];
        }

        private string GetAllChars(PasswordSettings settings)
        {
            StringBuilder allChars = new StringBuilder();

            if(settings.UseLower)
                allChars.Append(AppConfig.PasswordGeneratorConfig.LowerCase);

            if(settings.UseUpper)
                allChars.Append(AppConfig.PasswordGeneratorConfig.UpperCase);

            if(settings.UseDigits)
                allChars.Append(AppConfig.PasswordGeneratorConfig.Digits);

            if(settings.UseSpecial)
                allChars.Append(AppConfig.PasswordGeneratorConfig.SpecialChars);

            return allChars.ToString();
        }

        private string ShuffleString(string input)
        {
            char[] chars = input.ToCharArray();

            for(int i = chars.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars);
        }

        private string GetPasswordStrength(string password, PasswordSettings settings)
        {
            if(password.Length < 8)
                return "Слабый";

            int score = 0;

            // Длина пароля
            if(password.Length >= 12) score++;
            if(password.Length >= 16) score++;

            // Разнообразие символов
            if(settings.SettingsCount >= 2) score++;
            if(settings.SettingsCount >= 3) score++;
            if(settings.SettingsCount == 4) score++;

            return score switch
            {
                0 or 1 => "Слабый",
                2 or 3 => "Средний",
                4 or 5 => "Сильный",
                _ => "Очень сильный"
            };
        }
    }
}