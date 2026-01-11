namespace EducationalProject.Core
{
    public class AppConfigProvider : IAppConfigProvider
    {
        public string[] GetControllerTypes()
        {
            return AppConfig.MenuConfig.ControllerTypes;
        }

        public string GetControllersNamespace()
        {
            return AppConfig.MenuConfig.ControllersNamespace;
        }

        public string[] GetProgramNames()
        {
            return AppConfig.MenuConfig.ProgramNames;
        }
    }
}