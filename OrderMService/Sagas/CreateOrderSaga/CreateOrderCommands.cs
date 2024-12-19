using OrderMService.Models;

namespace OrderMController.Sagas.CreateOrderSaga;

public class VerifyUserCommand(Guid userId, List<int> productIds, List<int> productQuantities)
{
    public Guid UserId { get; set; } = userId;
    public List<int> ProductIds { get; set; } = productIds;
    public List<int> ProductQuantities { get; set; } = productQuantities;
}

public class CreateOrderInStoreCommand(Guid orderId, Guid cartId)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid CartId { get; set; } = cartId;
}

public class CreateDeliveryCommand(Guid orderId, Address destinationAddress)
{
    public Guid OrderId { get; set; } = orderId;
    public Address DestinationAddress { get; set; } = destinationAddress;
}

public class PaymentVerifyCommand(Guid orderId, Guid userId) // Currently only checking whether the credit card number is valid
{
    public Guid OrderId { get; set; } = orderId;
    public Guid UserId { get; set; } = userId;
}

public class UpdateOrderStatusCommand(Guid orderId, string status)
{
    public Guid OrderId { get; set; } = orderId;
    public string Status { get; set; } = status;
}
