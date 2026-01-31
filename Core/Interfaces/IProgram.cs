namespace EducationalProject.Core.Interfaces
{
    public interface IProgram
    {
        string Name { get; }
        string Description { get; }
        void Execute();
    }
}
