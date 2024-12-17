// Services/CustomUserClaimsPrincipalFactory.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using UserMService.Contexts;

/// <summary>
/// Used to add the default "User" role to a user if they don't have it
/// </summary>
public class CustomUserClaimsPrincipalFactory(
    UserManager<StoreUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<StoreUser, IdentityRole>(userManager, roleManager, optionsAccessor)
{
    private readonly UserManager<StoreUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(StoreUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (await _userManager.IsInRoleAsync(user, "User")) return identity;
        
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
        await _userManager.AddToRoleAsync(user, "User");

        return identity;
    }
}