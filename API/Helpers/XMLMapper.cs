using System.Xml;
using API.Entities;

namespace API.Helpers
{
    public class XMLMapper
    {
        public Item Map(XmlNode currentItem)
        {
            Item article = new Item();
            XmlNodeList itemsList = currentItem.ChildNodes;
            foreach (XmlNode item in itemsList)
            {
                switch (item.Name)
                {
                    case "title":
                        {
                            article.Title = item.InnerText;
                            break;
                        }

                    case "link":
                        {
                            article.Link = item.InnerText;
                            break;
                        }

                    case "description":
                        {
                            article.Description = item.InnerText.Trim();
                            break;
                        }

                    case "pubDate":
                        {
                            article.PubDate = DateTime.Parse(item.InnerText);
                            break;
                        }
                }
            }

            return article;
        }
    }
}