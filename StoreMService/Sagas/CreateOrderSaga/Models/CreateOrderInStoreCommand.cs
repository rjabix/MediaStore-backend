namespace StoreMService.Sagas.CreateOrderSaga.Models;

public class CreateOrderInStoreCommand(Guid orderId, Guid cartId)
{
    public Guid OrderId { get; set; } = orderId;
    public Guid CartId { get; set; } = cartId;
}