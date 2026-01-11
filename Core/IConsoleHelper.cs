namespace EducationalProject.Core
{
    public interface IConsoleHelper
    {
        void ClearAndShowHeader(string title);
        void ClearAndShowHeader(string title, string subtitle);
        void ShowHeader(string title, string subtitle = null);
        void ShowMenu(string title, string[] items);
        void ShowInfoBlock(string title, string[] lines);
        void ShowError(string message);
        void ShowInfo(string message);
        void ShowSuccess(string message);
        void WaitForAnyKey(string message = null);
        string GetInput(string prompt = ">>> ");
        void ShowSeparator(int length = 40);
        void ShowSectionTitle(string title);
        void ClearScreen();
        void WriteColor(string text, ConsoleColor color);
        void WriteLineColor(string text, ConsoleColor color);
        bool GetConfirmation(string question);
        void ShowProgress(int current, int total, string message = "");
    }
}