using EducationalProject.Core;

namespace EducationalProject.Controllers
{
    public abstract class BaseController
    {
        protected readonly IConsoleHelper ConsoleHelper;
        public abstract string Name { get; }
        public abstract void Run();

        // Конструктор по умолчанию для обратной совместимости (TO DO: впоследствии убрать и изменить все использующие классы)
        protected BaseController() : this(new ConsoleHelper()) { }

        // Конструктор с внедрением зависимости
        protected BaseController(IConsoleHelper consoleHelper)
        {
            ConsoleHelper = consoleHelper;
        }

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
