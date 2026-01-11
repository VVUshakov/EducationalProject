namespace EducationalProject.Controllers
{
    public abstract class BaseController
    {
        public abstract string Name { get; }
        public abstract void Run();
    }
}
