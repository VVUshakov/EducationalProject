using EducationalProject.Core.Interfaces;

namespace EducationalProject.Core.Views
{
    public class ConsoleView : IView
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {error}");
            Console.ResetColor();
        }

        public string ReadInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WaitForAnyKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
