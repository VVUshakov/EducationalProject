using EducationalProject.Core;
using EducationalProject.Models;

namespace EducationalProject.Views
{
    public class PasswordGeneratorView
    {
        public int GetPasswordLength()
        {
            ConsoleHelper.ShowMenu(
                "Выберите длину пароля",
                new string[] {
                    $"Короткий (6-8 символов)",
                    $"Средний (9-12 символов)",
                    $"Длинный (13-16 символов)",
                    $"Очень длинный (17-20 символов)",
                    $"Своя длина (от {AppConfig.PasswordGeneratorConfig.MinLength} до {AppConfig.PasswordGeneratorConfig.MaxLength})"
                }
            );

            int choice = InputValidator.GetValidMenuChoice(1, 5, ">>> Выберите опцию (1-5): ");

            Random random = new Random();

            return choice switch
            {
                1 => random.Next(6, 9),     // 6-8
                2 => random.Next(9, 13),    // 9-12
                3 => random.Next(13, 17),   // 13-16
                4 => random.Next(17, 21),   // 17-20
                5 => GetCustomLength(),
                _ => AppConfig.PasswordGeneratorConfig.DefaultLength
            };
        }

        private int GetCustomLength()
        {
            while(true)
            {
                string input = ConsoleHelper.GetInput(
                    $"Введите длину пароля ({AppConfig.PasswordGeneratorConfig.MinLength}-{AppConfig.PasswordGeneratorConfig.MaxLength}): ");

                if(!int.TryParse(input, out int length))
                {
                    ConsoleHelper.ShowError("Пожалуйста, введите число!");
                    continue;
                }

                if(length < AppConfig.PasswordGeneratorConfig.MinLength)
                {
                    ConsoleHelper.ShowError($"Длина должна быть не менее {AppConfig.PasswordGeneratorConfig.MinLength}!");
                    continue;
                }

                if(length > AppConfig.PasswordGeneratorConfig.MaxLength)
                {
                    ConsoleHelper.ShowError($"Длина должна быть не более {AppConfig.PasswordGeneratorConfig.MaxLength}!");
                    continue;
                }

                return length;
            }
        }

        public PasswordSettings GetPasswordSettings()
        {
            var settings = new PasswordSettings();

            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("НАСТРОЙКИ СИМВОЛОВ");
            Console.WriteLine(new string('-', 40));

            while(true)
            {
                Console.WriteLine("\nТекущие настройки:");
                ShowCurrentSettings(settings);

                ConsoleHelper.ShowMenu(
                    "Изменить настройки",
                    new string[] {
                        $"Строчные буквы (a-z) [{GetStatusText(settings.UseLower)}]",
                        $"Заглавные буквы (A-Z) [{GetStatusText(settings.UseUpper)}]",
                        $"Цифры (0-9) [{GetStatusText(settings.UseDigits)}]",
                        $"Специальные символы (!@#...) [{GetStatusText(settings.UseSpecial)}]",
                        "Сгенерировать пароль"
                    }
                );

                int choice = InputValidator.GetValidMenuChoice(1, 5, ">>> Выберите (1-5): ");

                if(choice == 5)
                {
                    if(!settings.HasAnySetting)
                    {
                        ConsoleHelper.ShowError("Должен быть выбран хотя бы один тип символов!");
                        continue;
                    }
                    break;
                }

                // Переключение настроек
                switch(choice)
                {
                    case 1: settings.UseLower = !settings.UseLower; break;
                    case 2: settings.UseUpper = !settings.UseUpper; break;
                    case 3: settings.UseDigits = !settings.UseDigits; break;
                    case 4: settings.UseSpecial = !settings.UseSpecial; break;
                }

                ConsoleHelper.ShowInfo($"Настройка изменена!");
            }

            return settings;
        }

        public void ShowResult(PasswordResult result)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("СГЕНЕРИРОВАННЫЙ ПАРОЛЬ");
            Console.WriteLine(new string('=', 50));

            if(!result.IsValid)
            {
                ConsoleHelper.ShowError("Ошибка генерации пароля!");
                return;
            }

            // Показываем пароль с цветом
            Console.WriteLine($"\nДлина: {result.Length} символов");
            Console.WriteLine(new string('-', 30));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{result.Password}");
            Console.ResetColor();

            Console.WriteLine(new string('-', 30));

            // Показываем настройки
            Console.WriteLine("\nИспользованные символы:");
            Console.WriteLine($"✓ Строчные буквы: {(result.Settings.UseLower ? "Да" : "Нет")}");
            Console.WriteLine($"✓ Заглавные буквы: {(result.Settings.UseUpper ? "Да" : "Нет")}");
            Console.WriteLine($"✓ Цифры: {(result.Settings.UseDigits ? "Да" : "Нет")}");
            Console.WriteLine($"✓ Специальные символы: {(result.Settings.UseSpecial ? "Да" : "Нет")}");

            // Показываем надежность
            Console.WriteLine("\nНадежность пароля:");
            Console.ForegroundColor = GetStrengthColor(result.Strength);
            Console.WriteLine($"★ {result.Strength} ★");
            Console.ResetColor();

            Console.WriteLine(new string('=', 50));

            // Показываем советы
            ShowPasswordTips();
        }

        private void ShowCurrentSettings(PasswordSettings settings)
        {
            Console.WriteLine($"• Строчные буквы: {GetStatusText(settings.UseLower)}");
            Console.WriteLine($"• Заглавные буквы: {GetStatusText(settings.UseUpper)}");
            Console.WriteLine($"• Цифры: {GetStatusText(settings.UseDigits)}");
            Console.WriteLine($"• Специальные символы: {GetStatusText(settings.UseSpecial)}");
        }

        private string GetStatusText(bool isEnabled)
        {
            return isEnabled ? "ВКЛ" : "ВЫКЛ";
        }

        private ConsoleColor GetStrengthColor(string strength)
        {
            return strength switch
            {
                "Слабый" => ConsoleColor.Red,
                "Средний" => ConsoleColor.Yellow,
                "Сильный" => ConsoleColor.Green,
                "Очень сильный" => ConsoleColor.Cyan,
                _ => ConsoleColor.White
            };
        }

        private void ShowPasswordTips()
        {
            Console.WriteLine("\n" + new string('*', 40));
            Console.WriteLine("СОВЕТЫ ПО БЕЗОПАСНОСТИ:");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("• Используйте пароли длиной не менее 12 символов");
            Console.WriteLine("• Комбинируйте разные типы символов");
            Console.WriteLine("• Не используйте личную информацию");
            Console.WriteLine("• Меняйте пароли каждые 3-6 месяцев");
            Console.WriteLine("• Не используйте один пароль на всех сайтах");
            Console.WriteLine(new string('*', 40));
        }
    }
}