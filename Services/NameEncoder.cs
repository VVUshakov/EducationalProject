namespace EducationalProject.Services
{
    public class NameEncoder : BaseService
    {
        public override string Name => "Кодировщик имени";

        public override void Run()
        {
            ConsoleHelper.ClearAndShowHeader(Name);

            string[] infoLines = {
                "Программа демонстрирует различные методы кодирования текста:",
                "1. Алфавитные позиции (А=1, Б=2, ...)",
                "2. Азбука Морзе",
                "3. Простой шифр (позиция × 3)"
            };

            ConsoleHelper.ShowInfoBlock(DESCRIPTION_TITLE, infoLines);

            string name = InputValidator.GetValidText(
                ">>> Введите ваше имя: ",
                minLength: 1,
                maxLength: 50
            ).ToUpper();

            ShowEncodingMethods();

            int choice = InputValidator.GetValidMenuChoice(1, 3, ">>> Выберите метод кодирования (1-3): ");
            string encodedName = EncodeName(name, choice);
            ShowResult(name, encodedName, choice);

            ConsoleHelper.WaitForAnyKey(PRESS_ANY_KEY_TO_RETURN);
        }

        private void ShowEncodingMethods()
        {
            string[] methods = {
                "Алфавитные позиции (А=1, Б=2, ...)",
                "Азбука Морзе",
                "Простой шифр (позиция × 3)"
            };

            ConsoleHelper.ShowMenu("МЕТОДЫ КОДИРОВАНИЯ", methods);
            Console.WriteLine();
        }

        private string EncodeName(string name, int methodChoice)
        {
            return methodChoice switch
            {
                1 => GetAlphabetCode(name),
                2 => GetMorseCode(name),
                3 => GetCipherCode(name),
                _ => GetAlphabetCode(name)
            };
        }

        private string GetAlphabetCode(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    int position = c - 'А' + 1;
                    codes.Add($"{position:00}");
                }
                else if(char.IsLetter(c))
                {
                    codes.Add($"{c}?");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[ПРОБЕЛ]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join("-", codes);
        }

        private string GetMorseCode(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c))
                {
                    string morseSymbol = GetMorseSymbol(c);
                    codes.Add(morseSymbol);
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("/");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
        }

        private string GetMorseSymbol(char c)
        {
            char upperC = char.ToUpper(c);

            return upperC switch
            {
                'А' => ".-",
                'Б' => "-...",
                'В' => ".--",
                'Г' => "--.",
                'Д' => "-..",
                'Е' => ".",
                'Ё' => ".",
                'Ж' => "...-",
                'З' => "--..",
                'И' => "..",
                'Й' => ".---",
                'К' => "-.-",
                'Л' => ".-..",
                'М' => "--",
                'Н' => "-.",
                'О' => "---",
                'П' => ".--.",
                'Р' => ".-.",
                'С' => "...",
                'Т' => "-",
                'У' => "..-",
                'Ф' => "..-.",
                'Х' => "....",
                'Ц' => "-.-.",
                'Ч' => "---.",
                'Ш' => "----",
                'Щ' => "--.-",
                'Ъ' => "--.--",
                'Ы' => "-.--",
                'Ь' => "-..-",
                'Э' => "..-..",
                'Ю' => "..--",
                'Я' => ".-.-",
                _ => "?"
            };
        }

        private string GetCipherCode(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name)
            {
                if(char.IsLetter(c) && IsRussianLetter(c))
                {
                    int encodedValue = (c - 'А' + 1) * 3;
                    codes.Add($"{encodedValue:000}");
                }
                else if(char.IsLetter(c))
                {
                    codes.Add($"{c}?");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[ПРОБЕЛ]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
        }

        private bool IsRussianLetter(char c)
        {
            return (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я');
        }

        private void ShowResult(string originalName, string encodedName, int methodChoice)
        {
            string methodName = methodChoice switch
            {
                1 => "Алфавитные позиции",
                2 => "Азбука Морзе",
                3 => "Простой шифр",
                _ => "Неизвестный метод"
            };

            string[] resultLines = {
                $"Исходное имя: {originalName}",
                $"Метод кодирования: {methodName}",
                "",
                $"Закодированное имя:",
                $"  {encodedName}"
            };

            ConsoleHelper.ShowInfoBlock("РЕЗУЛЬТАТ КОДИРОВАНИЯ", resultLines);
        }
    }
}