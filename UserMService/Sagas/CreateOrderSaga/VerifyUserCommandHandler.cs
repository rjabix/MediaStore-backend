using Microsoft.AspNetCore.Identity;
using Rebus.Bus;
using Rebus.Handlers;
using UserMService.Contexts;
using UserMService.Sagas.CreateOrderSaga.Models;

namespace UserMService.Sagas.CreateOrderSaga;

public class VerifyUserCommandHandler : IHandleMessages<VerifyUserCommand>
{
    private readonly UserManager<StoreUser> _userManager;
    private readonly IBus _bus;
    
    public VerifyUserCommandHandler(UserManager<StoreUser> userManager, IBus bus)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _bus =  bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task Handle(VerifyUserCommand command)
    {
        var isVerified = await VerifyUserAndAssignOrderId(command);
        var @event = CreateEventFromCommandAndVerificationResult(command, isVerified);
        await _bus.Reply(@event);
    }

    private async Task<bool> VerifyUserAndAssignOrderId(VerifyUserCommand command)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());

        // check
        if (user == null) return false;
        if (!user.EmailConfirmed) return false;
        if (!user.ProductCart_Ids.SequenceEqual(command.ProductIds)) return false;
        if (!user.ProductCartQuantities.SequenceEqual(command.ProductQuantities)) return false;
        
        // then assert that is verified
        user.OrdersGuids.Add(command.OrderId);
        await _userManager.UpdateAsync(user);
        return true;
    }

    private static OrderCreateUserVerifiedEvent CreateEventFromCommandAndVerificationResult(VerifyUserCommand command, bool isVerified)
    {
        return new OrderCreateUserVerifiedEvent(command.OrderId, command.UserId, isVerified);
    }
}