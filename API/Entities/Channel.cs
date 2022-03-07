namespace API.Entities
{
    public class Channel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}