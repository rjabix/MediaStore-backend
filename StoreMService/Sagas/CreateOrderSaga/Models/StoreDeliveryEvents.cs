namespace StoreMService.Sagas.CreateOrderSaga.Models;

public record StoreOrderCreatedEvent(Guid OrderId, string ShippingAddressId);

public record OrderDeliveryCreatedEvent(Guid OrderId);

public record OrderDeliveredEvent(Guid OrderId);