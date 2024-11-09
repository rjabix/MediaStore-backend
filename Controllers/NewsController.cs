using MediaStore_backend.Models;
using Microsoft.AspNetCore.Mvc;
using MediaStore_backend.Services;

namespace MediaStore_backend.Controllers;
[ApiController]
[Route("[controller]")]
public class NewsController : ControllerBase
{

    private readonly ILogger<NewsController> _logger;
    private readonly NewsService _newsService;

    public NewsController(ILogger<NewsController> logger, NewsService bigPromoService)
    {
        _logger = logger;
        _newsService = bigPromoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NewsCarouselItem>>> GetAll()
    {
        var items = await _newsService.GetAllNewsCarouseleItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NewsCarouselItem>> Get(int id)
    {
        var item = await _newsService.GetNewsCarouseleItemByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Add(NewsCarouselItem item)
    {
        var newItem = await _newsService.AddNewsCarouseleItemAsync(item);
        return CreatedAtAction(nameof(Get), new { id = newItem.id }, newItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, NewsCarouselItem item)
    {
        if (id != item.id)
        {
            return BadRequest();
        }

        var existingItem = await _newsService.GetNewsCarouseleItemByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _newsService.UpdateNewsCarouseleItemAsync(item);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingItem = await _newsService.GetNewsCarouseleItemByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _newsService.DeleteNewsCarouseleItemAsync(id);

        return NoContent();
    }
}
