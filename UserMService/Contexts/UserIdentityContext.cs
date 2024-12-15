using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserMService.Contexts;

public class UserIdentityContext(DbContextOptions<UserIdentityContext> options) : 
    IdentityDbContext<StoreUser>(options)
{
    
}