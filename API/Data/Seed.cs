using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.Entities;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            List<Channel> insertedChannels = new List<Channel>();
            var reg = new System.Text.RegularExpressions.Regex(@"<img(?!.*alt).*?>");

            insertedChannels.Add(new Channel { Title = "Хабр", Link = @"https://habr.com/ru/rss/all/all/"});
            insertedChannels.Add(new Channel { Title = "Ixbt", Link = @"http://www.ixbt.com/export/news.rss"});

            foreach(Channel channel in insertedChannels){
                if(context.Channels.Where(a => a.Title == channel.Title).Count() == 0){
                    context.Channels.Add(channel);
                    context.SaveChangesAsync();
                }
            }

            if (context.Items.Count() == 0) {
                int readdingNews = 0;
                foreach(Channel channel in context.Channels.ToList()){
                    XmlDocument doc = new XmlDocument();
                    doc.Load(channel.Link);

                    XmlNodeList nodeList = doc.GetElementsByTagName("item");

                    var mapper = new XMLMapper();

                    foreach (XmlNode currentItem in nodeList)
                    {
                        Item article = mapper.Map(currentItem);
                        article.ChannelTitle = channel.Title;

                        article.Description = reg.Replace(article.Description, " ");
                        
                        channel.Items.Add(article);
                        context.Items.Add(article);
                        context.SaveChangesAsync();

                        readdingNews++;
                    }
                    context.Channels.Update(channel);
                }
                Console.WriteLine("Count of reading and saving news: " + readdingNews);
            }
            else{
                int readdingNews = 0 , savingNews=0;
                foreach(Channel channel in context.Channels.ToList()){
                    XmlDocument doc = new XmlDocument();
                    doc.Load(channel.Link);

                    XmlNodeList nodeList = doc.GetElementsByTagName("item");

                    var mapper = new XMLMapper();

                    foreach (XmlNode currentItem in nodeList)
                    {
                        Item article = mapper.Map(currentItem);
                        readdingNews++;

                        if (context.Items.Where(a => a.Title == article.Title).Count() == 0)
                        {
                            article.ChannelTitle = channel.Title;
                            article.Description = reg.Replace(article.Description, " ");


                            channel.Items.Add(article);
                            context.Items.Add(article);
                            
                            context.SaveChangesAsync();
                            savingNews++;
                        }
                    }
                    context.Channels.Update(channel);
                }
                 Console.WriteLine("Count of reading news: " + readdingNews +" and saving news: " + savingNews);
            }
        
            await context.SaveChangesAsync();
        }
    }
}