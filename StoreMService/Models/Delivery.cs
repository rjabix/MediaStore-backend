using Microsoft.EntityFrameworkCore;

namespace StoreMService.Models;

[PrimaryKey(nameof(OrderId))]
public class Delivery
{
    public required Guid OrderId { get; init; }
    public Guid UserId { get; set; }
    public required string Address { get; init; }
}