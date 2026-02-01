using EducationalProject.Core.Controllers;
using EducationalProject.Core.Interfaces;
using EducationalProject.Core.Services;
using EducationalProject.Core.Views;

namespace EducationalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настройка приложения с несколькими представлениями
            var views = new List<IView>
            {
                new ConsoleView(), // Основное представление
                // Можно добавить другие представления, например:
                // new WebView(),
                // new FileView("output.txt"),
            };

            // Настройка текущего активного представления
            IView currentView = new ConsoleView();

            // Создаем список программ
            var programs = new List<BaseProgram>
            {
                new CalculatorProgram(),
                new NameEncoderProgram(),
                new ReverseStringProgram(),
                new NewProgram(),

                // Добавить новую программу можно просто создав новый экземпляр
            };


            // Создаем контроллер,
            // передаем в него список представлений, текущее активное представление и список программ
            var controller = new MainController(views, currentView, programs);

            // Запускаем приложение
            controller.Run();
        }
    }
}