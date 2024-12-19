using OrderMService.Models;
using Rebus.Bus;
using Rebus.Handlers;

namespace OrderMController.Sagas.CreateOrderSaga;

public record OrderCreatedEvent(Guid OrderId, Guid UserId, List<int> ProductIds, List<int> ProductQuantities, Address ShippingAddress);

public record OrderCreateUserVerifiedEvent(Guid OrderId, Guid UserId, List<int> ProductIds, List<int> ProductQuantities, bool IsVerified);

public record OrderCreatedPaymentVerified(Guid OrderId, Guid UserId);

public record StoreOrderCreatedEvent(Guid OrderId, Address ShippingAddress);

public record OrderDeliveryCreatedEvent(Guid OrderId);

public record OrderDeliveredEvent(Guid OrderId);