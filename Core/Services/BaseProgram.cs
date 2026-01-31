using EducationalProject.Core.Interfaces;
using EducationalProject.Core.Views;

namespace EducationalProject.Core.Services
{
    public abstract class BaseProgram : IProgram
    {
        protected readonly IView _view;

        public abstract string Name { get; }
        public abstract string Description { get; }

        protected BaseProgram(IView view = null)
        {
            _view = view ?? new ConsoleView();
        }

        public abstract void Run();

        protected void ShowHeader()
        {
            _view.Clear();
            _view.ShowMessage($"=== {Name.ToUpper()} ===");
            _view.ShowMessage(Description);
            _view.ShowMessage("");
        }
    }
}
