namespace EducationalProject.Core.Interfaces
{
    public interface IView
    {
        void ShowMessage(string message);
        void ShowError(string error);
        string ReadInput(string prompt);
        void Clear();
        void WaitForAnyKey();
    }
}