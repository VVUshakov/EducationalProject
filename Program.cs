namespace EducationalProject
{
    // Главный файл - точка входа
    class Program
    {
        static void Main()
        {
            // Создаем менеджер меню и запускаем его
            var manager = new MenuManager();
            manager.Run();
        }
    }
}