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
            new NameEncoderProgram()
            // Добавить новую программу можно просто создав новый экземпляр
        };

            var controller = new MainController(view, programs);

            // Запуск приложения
            controller.Run();
        }
    }
}