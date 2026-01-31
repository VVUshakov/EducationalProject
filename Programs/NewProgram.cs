using EducationalProject.Core.Views;

namespace EducationalProject.Programs
{
    public class NewProgram : BaseProgram
    {
        public override string Name => "Новая программа";
        public override string Description => "Пример добавления новой программы";

        public NewProgram() : base(new ConsoleView()) { }

        public override void Execute()
        {
            ShowHeader();

            // Здесь можно реализовать логику новой программы
            _view.ShowMessage("Это пример новой программы!");
            _view.ShowMessage("Чтобы добавить её в меню, нужно:");
            _view.ShowMessage("1. Создать класс, унаследованный от BaseProgram");
            _view.ShowMessage("2. Реализовать все абстрактные методы");
            _view.ShowMessage("3. Добавить экземпляр класса в список программ в Program.cs");

            // Пример работы
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
