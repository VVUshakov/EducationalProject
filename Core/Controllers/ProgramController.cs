using EducationalProject.Core.Interfaces;

namespace EducationalProject.Core.Controllers
{
    public abstract class ProgramController : IController
    {
        protected readonly IView _view;

        protected ProgramController(IView view)
        {
            _view = view;
        }

        public abstract void Run();
    }
}
