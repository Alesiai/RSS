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
        public IEnumerable<Channel> GetItems()
        {
            return _context.Channels.ToList();
        }
    }
}