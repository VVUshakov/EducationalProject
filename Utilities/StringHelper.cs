namespace EducationalProject.Utilities
{
    public static class StringHelper
    {
        // Простое перемешивание строки
        public static string ShuffleString(string input, Random random)
        {
            char[] chars = input.ToCharArray();

            for(int i = chars.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // Используем временную переменную для обмена
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }

            return new string(chars);
        }
    }
}