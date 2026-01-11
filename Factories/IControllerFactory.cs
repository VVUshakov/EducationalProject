using EducationalProject.Controllers;

namespace EducationalProject.Factories
{
    public interface IControllerFactory
    {
        List<BaseController> CreateControllers();
    }
}