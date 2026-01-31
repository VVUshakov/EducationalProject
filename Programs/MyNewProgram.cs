using EducationalProject.Core.Views;

namespace EducationalProject.Programs
{
    public class MyNewProgram : BaseProgram
    {
        public override string Name => "Моя новая программа";
        public override string Description => "Описание новой программы";

        public MyNewProgram() : base(new ConsoleView()) { }

        public override void Execute()
        {
            ShowHeader();
            // Логика вашей программы здесь
            _view.ShowMessage("Привет от новой программы!");
        }
    }
}
