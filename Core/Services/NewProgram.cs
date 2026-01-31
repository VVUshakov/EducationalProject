using EducationalProject.Core.Views;

namespace EducationalProject.Core.Services
{
    public class NewProgram : BaseProgram
    {
        public override string Name => "Моя новая программа";
        public override string Description => "Описание новой программы";

        public NewProgram() : base(new ConsoleView()) { }

        public override void Run()
        {
            ShowHeader();
            // Логика вашей программы здесь
            _view.ShowMessage("Привет от новой программы!");
        }
    }
}
