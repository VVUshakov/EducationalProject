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
            int firstIndex = 0;
            int lastIndex = Items.Count - 1;

            for(int i = lastIndex; i >= firstIndex; i--)
            {
                if(Items[i].Id == id)
                {
                    Items.RemoveAt(i);
                }
            }
        }
    }
}