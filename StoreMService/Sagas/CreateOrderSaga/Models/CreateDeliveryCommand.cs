namespace StoreMService.Sagas.CreateOrderSaga.Models;

public class CreateDeliveryCommand(Guid orderId, string destinationAddressId)
{
    public Guid OrderId { get; set; } = orderId;
    public string DestinationAddressId { get; set; } = destinationAddressId;
}