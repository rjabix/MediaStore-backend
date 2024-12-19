using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMService.Models;

public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required List<int> ProductIds { get; set; }
    
    public required List<int> ProductQuantities { get; set; }
    
    public required DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending; // Pending, Processing, Shipped, Delivered
    
    public required Address ShippingAddress { get; set; }
    
    public DateTime ExpectedFinishDate { get; set; }
}