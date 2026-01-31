using EducationalProject.Core.Controllers;
using EducationalProject.Core.Services;
using EducationalProject.Core.Views;

namespace EducationalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка приложения
            var view = new ConsoleView();
            var programs = new List<BaseProgram>
            {
                new CalculatorProgram(),
                new NameEncoderProgram(),
                new NewProgram(),
                new ReverseStringProgram(),

                // Добавить новую программу можно просто создав новый экземпляр
            };

            var controller = new MainController(view, programs);

            // Запуск приложения
            controller.Run();
        }
    }
}