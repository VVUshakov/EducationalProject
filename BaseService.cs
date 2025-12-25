namespace EducationalProject
{
    public abstract class BaseService
    {
        protected const string PRESS_ANY_KEY_TO_RETURN = "Нажмите любую клавишу для возврата в меню...";
        protected const string DESCRIPTION_TITLE = "Описание";
        protected const string ERROR_TITLE = "Ошибка";

        public abstract string Name { get; }
        public abstract void Run();
    }
}