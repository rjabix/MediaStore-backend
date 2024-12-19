using Microsoft.AspNetCore.Mvc;

namespace OrderMController.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(ILogger<OrderController> logger) : ControllerBase
{
    
}