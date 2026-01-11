using EducationalProject.Core;

namespace EducationalProject.Controllers
{
    public abstract class BaseController
    {
        public abstract string Name { get; }
        public abstract void Run();

        protected void ShowHeader()
        {
            ConsoleHelper.ClearAndShowHeader(Name);
        }

        protected void ShowDescription(string title, string[] lines)
        {
            ConsoleHelper.ShowInfoBlock(title, lines);
        }

        protected void WaitForAnyKey()
        {
            ConsoleHelper.WaitForAnyKey();
        }
    }
}
