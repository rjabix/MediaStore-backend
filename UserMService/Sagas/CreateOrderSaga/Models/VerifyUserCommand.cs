namespace UserMService.Sagas.CreateOrderSaga.Models;

public class VerifyUserCommand(Guid orderId, Guid userId, List<int> productIds, List<int> productQuantities)
{
    public Guid OrderId { get; } = orderId;
    public Guid UserId { get; } = userId;
    public List<int> ProductIds { get; } = productIds;
    public List<int> ProductQuantities { get; } = productQuantities;
}