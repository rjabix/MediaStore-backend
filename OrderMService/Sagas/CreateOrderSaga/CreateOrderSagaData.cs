using OrderMService.Models;
using Rebus.Sagas;

namespace OrderMController.Sagas.CreateOrderSaga;

public class CreateOrderSagaData : ISagaData
{
    public Guid Id { get; set; } // saga id
    public int Revision { get; set; }

    public Guid OrderId { get; set; }

    public string Status { get; set; }

    // AVAILABLE STATES
    // public bool IsUserValidated { get; set; }
    //
    // public bool IsStoreOrderCreated { get; set; }
    //
    // // public bool IsPaymentVerified { get; set; }
    //
    // public bool IsDeliveryCreated { get; set; }
    //
    // public bool IsDelivered { get; set; }
    //
    // public bool IsCancelled { get; set; }
}