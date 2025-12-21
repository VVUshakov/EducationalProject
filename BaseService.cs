namespace EducationalProject
{
    // Базовый класс для всех Программ (вместо интерфейса)
    public abstract class BaseService
    {
        // Название программы
        public abstract string Name { get; }

        // Метод для запуска программы
        public abstract void Run();
    }
}