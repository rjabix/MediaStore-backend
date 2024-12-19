using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserMService.Contexts;

namespace UserMService.Controllers;

[ApiController]
[Route("identity/[controller]")]
public class UserController(ILogger<UserController> logger, UserManager<StoreUser> userManager) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    
    // Cart management

    [HttpPost("add-to-cart")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (user.ProductCart_Ids.Count == 0 || user.ProductCartQuantities.Count == 0)
        {
            user.ProductCart_Ids = [];
            user.ProductCartQuantities = [];
        }

        user.ProductCart_Ids.Add(request.ProductId);
        user.ProductCartQuantities.Add(request.Quantity);

        await userManager.UpdateAsync(user);

        return Ok();
    }
    
    public class AddToCartRequest
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
    
    [HttpGet("get-cart")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> GetCart()
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        
        if(user.ProductCart_Ids.Count == 0 || user.ProductCartQuantities.Count == 0)
        {
            return Ok("No items in cart");
        }
        
        var cart = user.ProductCart_Ids
            .Select<int, CartItem>((t, i) => new CartItem { ProductId = t, Quantity = user.ProductCartQuantities[i] })
            .ToList();

        return Ok(cart);
    }

    private class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
    [HttpPost("remove-from-cart")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> RemoveFromCart([FromBody][Required] List<int> productIds)
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (user.ProductCart_Ids.Count == 0 || user.ProductCartQuantities.Count == 0)
        {
            return BadRequest("No items in cart");
        }

        foreach (var index in productIds.Select(productId => user.ProductCart_Ids.IndexOf(productId)))
        {
            if (index == -1)
            {
                return Ok("Item not found in cart");
            }

            user.ProductCart_Ids.RemoveAt(index);
            user.ProductCartQuantities.RemoveAt(index);

        }
        await userManager.UpdateAsync(user);
        return Ok();
    }
    
    
    [HttpPost("change-cart")] // explicitly change the whole cart to the new one
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> ChangeCart([FromBody] Dictionary<int, int> request)
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        user.ProductCart_Ids = [];
        user.ProductCartQuantities = [];
        
        user.ProductCart_Ids.AddRange(request.Keys);
        user.ProductCartQuantities.AddRange(request.Values);

        await userManager.UpdateAsync(user);

        return Ok();

    }
    
    
    // Wish list management
    
    [HttpPost("add-to-wishlist")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> AddToWishList([FromQuery][Required] int productId) // Query parameter
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        user.ProductWishList ??= [];

        user.ProductWishList.Add(productId);

        await userManager.UpdateAsync(user);

        return Ok();
    }
    
    [HttpPost("remove-from-wishlist")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> RemoveFromWishList([FromQuery][Required] int productId)
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        if (user.ProductWishList.Count == 0)
        {
            return Ok("No items in wish list");
        }

        user.ProductWishList.Remove(productId);

        await userManager.UpdateAsync(user);

        return Ok();
    } 
    
    [HttpGet("get-wishlist")]
    [Authorize(Roles = "User,Manager,Admin")]
    public async Task<IActionResult> GetWishList()
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        
        return user.ProductWishList.Count == 0 ? Ok("No items in wish list") : Ok(user.ProductWishList);
    }
    
}