namespace EducationalProject.Core
{
    public interface IAppConfigProvider
    {
        string[] GetControllerTypes();
        string GetControllersNamespace();
        string[] GetProgramNames();
    }
}