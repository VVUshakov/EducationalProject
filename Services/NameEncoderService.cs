using EducationalProject.Models;

namespace EducationalProject.Services
{
    public class NameEncoderService
    {
        // Метод 1: Алфавитный код (А=1, Б=2...)
        public string EncodeAlphabet(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name.ToUpper())
            {
                if(c >= 'А' && c <= 'Я')
                {
                    int position = c - 'А' + 1;
                    codes.Add($"{position:00}");
                }
                else if(c >= 'A' && c <= 'Z')
                {
                    int position = c - 'A' + 1;
                    codes.Add($"{position:00}");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join("-", codes);
        }

        // Метод 2: Азбука Морзе
        public string EncodeMorse(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name.ToUpper())
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
            return c switch
            {
                'А' or 'A' => ".-",
                'Б' or 'B' => "-...",
                'В' or 'W' => ".--",
                'Г' or 'G' => "--.",
                'Д' or 'D' => "-..",
                'Е' or 'E' => ".",
                'Ё' or 'E' => ".",
                'Ж' or 'V' => "...-",
                'З' or 'Z' => "--..",
                'И' or 'I' => "..",
                'Й' or 'J' => ".---",
                'К' or 'K' => "-.-",
                'Л' or 'L' => ".-..",
                'М' or 'M' => "--",
                'Н' or 'N' => "-.",
                'О' or 'O' => "---",
                'П' or 'P' => ".--.",
                'Р' or 'R' => ".-.",
                'С' or 'S' => "...",
                'Т' or 'T' => "-",
                'У' or 'U' => "..-",
                'Ф' or 'F' => "..-.",
                'Х' or 'H' => "....",
                'Ц' or 'C' => "-.-.",
                'Ч' => "---.",
                'Ш' => "----",
                'Щ' => "--.-",
                'Ъ' => "--.--",
                'Ы' => "-.--",
                'Ь' => "-..-",
                'Э' => "..-..",
                'Ю' => "..--",
                'Я' => ".-.-",
                _ => $"{c}"
            };
        }

        // Метод 3: Шифр (позиция × 3)
        public string EncodeCipher(string name)
        {
            List<string> codes = new List<string>();

            foreach(char c in name.ToUpper())
            {
                if(c >= 'А' && c <= 'Я')
                {
                    int position = c - 'А' + 1;
                    int encodedValue = position * 3;
                    codes.Add($"{encodedValue:000}");
                }
                else if(c >= 'A' && c <= 'Z')
                {
                    int position = c - 'A' + 1;
                    int encodedValue = position * 3;
                    codes.Add($"{encodedValue:000}");
                }
                else if(char.IsWhiteSpace(c))
                {
                    codes.Add("[]");
                }
                else
                {
                    codes.Add($"{c}");
                }
            }

            return string.Join(" ", codes);
        }

        public EncodingResult EncodeName(string name, int methodChoice)
        {
            string encoded = methodChoice switch
            {
                1 => EncodeAlphabet(name),
                2 => EncodeMorse(name),
                3 => EncodeCipher(name),
                _ => EncodeAlphabet(name)
            };

            string methodName = methodChoice switch
            {
                1 => "Алфавитный код (А=1, Б=2...)",
                2 => "Азбука Морзе",
                3 => "Числовой шифр (×3)",
                _ => "Алфавитный код"
            };

            return new EncodingResult
            {
                OriginalName = name,
                EncodedName = encoded,
                MethodName = methodName,
                MethodChoice = methodChoice
            };
        }
    }
}