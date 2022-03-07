using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly DataContext _context;
    public ItemsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
        return _context.Items.ToList();
    }
    
    [HttpGet("date")]
    public IEnumerable<Item> GetItemsSortedByDate()
    {
        return _context.Items.OrderBy(p=>p.PubDate).ToList();
    }
    
    [HttpGet("channel")]
    public IEnumerable<Item> GetItemsSortedByChanel()
    {
        return _context.Items.OrderBy(t=>t.ChannelTitle).ToList();
    }
}
