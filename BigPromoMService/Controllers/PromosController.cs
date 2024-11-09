using BigPromoMService.Models;
using Microsoft.AspNetCore.Mvc;
using BigPromoMService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BigPromoMService.Controllers;
[ApiController]
[Route("[controller]")]
public class PromosController : ControllerBase
{
    private readonly ILogger<PromosController> _logger;
    private readonly BigPromoService _bigPromoService;

    public PromosController(ILogger<PromosController> logger, BigPromoService bigPromoService)
    {
        _logger = logger;
        _bigPromoService = bigPromoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BigPromoItem>>> GetAll()
    {
        var items = await _bigPromoService.GetAllBigPromoItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BigPromoItem>> Get(int id)
    {
        var item = await _bigPromoService.GetBigPromoItemByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Add(BigPromoItem item)
    {
        var newItem = await _bigPromoService.AddBigPromoItemAsync(item);
        return CreatedAtAction(nameof(Get), new { id = newItem.id }, newItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BigPromoItem item)
    {
        if (id != item.id)
        {
            return BadRequest();
        }

        var existingItem = await _bigPromoService.GetBigPromoItemByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _bigPromoService.UpdateBigPromoItemAsync(item);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingItem = await _bigPromoService.GetBigPromoItemByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _bigPromoService.DeleteBigPromoItemAsync(id);

        return NoContent();
    }
}
