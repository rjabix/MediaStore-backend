using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentMService.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;
    private readonly ISender _sender;
    private readonly ICommandFactory _commandFactory;

    public PaymentController(ILogger<PaymentController> logger, ISender sender, ICommandFactory commandFactory)
    {
        _logger = logger;
        _sender = sender;
        _commandFactory = commandFactory;
    }

    [HttpPost]
    public async Task<ActionResult<object>> Handle(PaymentRequest request)
    {
        var command = _commandFactory.CreatePaymentCommand(request);
        return Ok(await _sender.Send(command));
    }
}