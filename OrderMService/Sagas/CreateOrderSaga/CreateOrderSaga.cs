using Microsoft.EntityFrameworkCore;
using OrderMService.Models;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace OrderMController.Sagas.CreateOrderSaga;

/*
 * This class is a saga that is initiated by the OrderCreatedEvent.
 * It is responsible for creating an order in the database.
 *
 * The saga steps are as follows:
 * 1. The OrderCreatedEvent is received. (after all the information were added on this service)
 * 2. Connect to UserService to double-check if user exists, can make new orders, and the cart is the same on both services.
 * 3. Connect to StoreService and create the order in store.
 * 4. Connect the PaymentService and create the payment. ! Currently only checking whether the provided card's number is valid
 * 5. Connect to the DeliveryService and create the delivery from warehouse to destination (user or store address).
 * 6. Update the order status to "Delivering".
 * 7. Wait the DeliveryService to Deliver and update the state to Shipped.
 */

public class CreateOrderSaga : Saga<CreateOrderSagaData>, 
    IAmInitiatedBy<OrderCreatedEvent>,
    IHandleMessages<OrderCreateUserVerifiedEvent>,
    IHandleMessages<OrderCreatedPaymentVerified>,
    IHandleMessages<StoreOrderCreatedEvent>,
    IHandleMessages<OrderDeliveryCreatedEvent>,
    IHandleMessages<OrderDeliveredEvent>
{
    private readonly IBus _bus;
    private readonly OrderDbContext _context;
    
    public CreateOrderSaga(IBus bus, OrderDbContext context)
    {
        _bus = bus;
        _context = context;
    }
    
    protected override void CorrelateMessages(ICorrelationConfig<CreateOrderSagaData> config)
    {
        config.Correlate<OrderCreatedEvent>(m => m.OrderId, s => s.Id);
        config.Correlate<OrderCreateUserVerifiedEvent>(m => m.OrderId, s => s.Id);
        config.Correlate<OrderCreatedPaymentVerified>(m => m.OrderId, s => s.Id);
        config.Correlate<StoreOrderCreatedEvent>(m => m.OrderId, s => s.Id);
        config.Correlate<OrderDeliveryCreatedEvent>(m => m.OrderId, s => s.Id);
        config.Correlate<OrderDeliveredEvent>(m => m.OrderId, s => s.Id);
    }


    public async Task Handle(OrderCreatedEvent message)
    {
        if (!IsNew) return;
        // Step 1. Initialize the saga 
        Data.OrderId = message.OrderId;
        Data.Status = "Created";
        
        // Step 2. Send a verification command to UserService
        // (it is checking whether the given userId is valid and if this user's cart is the same as the one in the message)
        
        await _bus.Send(new VerifyUserCommand(message.UserId, message.ProductIds, message.ProductQuantities));
    }

    public async Task Handle(OrderCreateUserVerifiedEvent message)
    {
        if (Data.Status != "Created") return;
        Data.Status = "UserVerified";
        // Step 3. Send a command to StoreService to create the order in the store
        await _bus.Send(new PaymentVerifyCommand(Data.OrderId, message.UserId));
    }

    public async Task Handle(OrderCreatedPaymentVerified message)
    {
        if (Data.Status != "UserVerified") return;
        
        Data.Status = "PaymentVerified";
        
        // Step 4. Send a command to StoreService to create the order in the store
        await _bus.Send(new CreateOrderInStoreCommand(Data.OrderId, message.UserId));
    }
    
    // Non-critical part below, the saga cannot be cancelled at this point - no need for compensation.

    public async Task Handle(StoreOrderCreatedEvent message)
    {
        if(Data.Status != "PaymentVerified") return;

        Data.Status = "Processing";
        await _context.Orders.Where(o => o.Id == Data.OrderId)
            .ExecuteUpdateAsync(o => 
                o.SetProperty(p => p.Status, OrderStatus.Processing));
        
        // Step 5. Creating the delivery to specified address
        
        await _bus.Send(new CreateDeliveryCommand(Data.OrderId, message.ShippingAddress));
    }

    public async Task Handle(OrderDeliveryCreatedEvent message)
    {
        if (Data.Status != "Processing") return;

        Data.Status = "Shipping";
        await _context.Orders.Where(o => o.Id == Data.OrderId)
            .ExecuteUpdateAsync(o => 
                o.SetProperty(p => p.Status, OrderStatus.Shipping));
        
        // Step 6. When the delivery is created, the order is marked as "Delivering" and no messages are needed to send
        
    }

    public async Task Handle(OrderDeliveredEvent message)
    {
        await _context.Orders.Where(o => o.Id == Data.OrderId)
            .ExecuteUpdateAsync(o => 
                o.SetProperty(p => p.Status, OrderStatus.Delivered));
        
        // When is delivered, update the order status as delivered and end the saga.
        MarkAsComplete();
    }
    // Possible to add next functions, as e.g. the update on product etc.
}