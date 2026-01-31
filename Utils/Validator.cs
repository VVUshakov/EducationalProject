namespace EducationalProject.Utils
{
    public static class Validator
    {
        public static bool IsValidNumber(string input, out double result)
        {
            return double.TryParse(input, out result);
        }

        public static bool IsValidInteger(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
