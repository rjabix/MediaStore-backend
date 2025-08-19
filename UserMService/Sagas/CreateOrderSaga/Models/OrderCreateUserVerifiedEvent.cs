namespace UserMService.Sagas.CreateOrderSaga.Models;

public record OrderCreateUserVerifiedEvent(Guid OrderId, Guid UserId, bool IsVerified);