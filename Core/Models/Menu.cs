namespace EducationalProject.Core.Models
{
    public class Menu
    {
        public List<MenuItem> Items { get; private set; }

        public Menu()
        {
            Items = new List<MenuItem>();
        }

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(int id)
        {
            Items.RemoveAll(item => item.Id == id);
        }
    }
}
