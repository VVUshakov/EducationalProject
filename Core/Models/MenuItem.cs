namespace EducationalProject.Core.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Action Action { get; set; }

        public MenuItem(int id, string title, Action action)
        {
            Id = id;
            Title = title;
            Action = action;
        }
    }
}
