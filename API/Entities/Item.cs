namespace API.Entities
{
    public class Item
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }

        public int? ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
        public string ChannelTitle{get;set;}
    }
}