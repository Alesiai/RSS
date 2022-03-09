using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class ChannelsController: ControllerBase
    {
        private readonly DataContext _context;
        public ChannelsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<string> GetItems()
        {
            var request = _context.Channels.ToList();
            List<string> channelsToFront = new List<string>();
            channelsToFront.Add("all");
            foreach(Channel channel in request){
                channelsToFront.Add(channel.Title);
            }
            return channelsToFront;
        }
    }
}