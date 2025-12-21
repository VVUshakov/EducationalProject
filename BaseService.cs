namespace EducationalProject
{
    /// <summary>
    /// Базовый класс для всех Программ
    /// 
    /// Как создать новую программу:
    /// 1. Создайте класс в папке Services
    /// 2. Наследуйтесь от BaseService
    /// 3. Реализуйте свойство Name
    /// 4. Реализуйте метод Run()
    /// 5. Добавьте программу в ProgramsConfig.GetAllPrograms()
    /// 
    /// Пример:
    /// public class MyProgram : BaseService
    /// {
    ///     public override string Name => "Моя программа";
    ///     
    ///     public override void Run()
    ///     {
    ///         ConsoleHelper.ClearAndShowHeader(Name);
    ///         ConsoleHelper.ShowInfo("Привет, это моя программа!");
    ///         ConsoleHelper.WaitForAnyKey();
    ///     }
    /// }
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Название программы (отображается в меню)
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Главный метод программы (точка входа)
        /// </summary>
        public abstract void Run();
    }
}