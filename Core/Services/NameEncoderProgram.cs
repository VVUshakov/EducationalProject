using EducationalProject.Core.Views;

namespace EducationalProject.Core.Services
{
    public class NameEncoderProgram : BaseProgram
    {
        public override string Name => "Кодировщик имени";
        public override string Description => "Преобразует имя в специальный код";

        public NameEncoderProgram() : base(new ConsoleView()) { }

        public override void Run()
        {
            ShowHeader();

            string name = _view.ReadInput("Введите ваше имя: ");

            if(string.IsNullOrWhiteSpace(name))
            {
                _view.ShowError("Имя не может быть пустым!");
                return;
            }

            string encodedName = EncodeName(name);
            _view.ShowMessage($"\nЗакодированное имя: {encodedName}");
        }

        private string EncodeName(string name)
        {
            // Простой алгоритм кодирования для примера
            char[] chars = name.ToUpper().ToCharArray();

            for(int i = 0; i < chars.Length; i++)
            {
                if(char.IsLetter(chars[i]))
                {
                    // Сдвигаем букву на 1 позицию в алфавите
                    chars[i] = (char)((chars[i] - 'A' + 1) % 26 + 'A');
                }
            }

            return new string(chars);
        }
    }
}