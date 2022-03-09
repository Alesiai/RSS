using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Extensions;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<Item>>> GetItems([FromQuery]ItemParams itemParams)
    {
        var query = _context.Items.AsQueryable();

        query = itemParams.OrderBy switch {
            "channel" => query.OrderBy(i => i.ChannelTitle),
            "date" => query.OrderByDescending(i => i.PubDate),
            _ => query,
        };

        query = itemParams.SearchedChannel switch{
            "all" => query,
            _ => query.Where(c=>c.ChannelTitle==itemParams.SearchedChannel),
        };

        var result = await PagedList<Item>.CreateAsync(query.AsNoTracking(), 
                    itemParams.PageNumber, itemParams.PageSize);


        var items = result;
        
        Response.AddPaginationHeader(items.CurrentPage, items.PageSize, 
            items.TotalCount, items.TotalPages);

        
        
        return Ok(items);
    }

     [HttpGet("page")]
    public async Task<ActionResult<PagedList>> GetPageInfo([FromQuery]ItemParams itemParams)
    {
        var query = _context.Items.AsQueryable();

        query = itemParams.OrderBy switch {
            "channel" => query.OrderBy(i => i.ChannelTitle),
            "date" => query.OrderByDescending(i => i.PubDate),
            _ => query,
        };

        query = itemParams.SearchedChannel switch{
            "all" => query,
            _ => query.Where(c=>c.ChannelTitle==itemParams.SearchedChannel),
        };

        var result = await PagedList<Item>.CreateAsync(query.AsNoTracking(), 
                    itemParams.PageNumber, itemParams.PageSize);
    
        PagedList page = new PagedList(result.TotalCount, result.CurrentPage, result.PageSize);

        Console.WriteLine("HEEEEYYYYYYYYYYYYYYYYYYYY"  + page.TotalCount +  " " + page.TotalPages);
        
        return Ok(page);
    }
}
