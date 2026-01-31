using EducationalProject.Core.Views;

namespace EducationalProject.Core.Services
{
    public class ReverseStringProgram : BaseProgram
    {
        public override string Name => "Обратная строка";
        public override string Description => "Переворачивает введенный текст";

        public ReverseStringProgram() : base(new ConsoleView()) { }

        public override void Run()
        {
            ShowHeader();
            string input = _view.ReadInput("\nВведите текст: ");
            string reversed = ReverseString(input);
            _view.ShowMessage($"Перевернутая строка: {reversed}");
        }

        private string ReverseString(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}