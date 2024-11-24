using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMService.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<UserIdentityContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
        options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    })
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<UserIdentityContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

var identityGroup = app.MapGroup("/identity/");

identityGroup.MapIdentityApi<IdentityUser>();

app.MapControllers();

// Logout endpoint
identityGroup.MapPost("/logout", async (SignInManager<IdentityUser> signInManager,
        [FromBody] object empty) =>
    {
        if (empty == null) return Results.Unauthorized();
        
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .WithOpenApi()
    .RequireAuthorization();

// app.MapPost("/me", async (ClaimsPrincipal claims, UserIdentityContext context) =>
//     {
//         var userId = claims.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;
//         if (userId == null)
//         {
//             return Results.Unauthorized();
//         }
//
//         Console.WriteLine(userId);
//         var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
//         if (user != null)
//         {
//             return Results.Ok(user);
//         }
//
//         return Results.NotFound();
//     })
//     .WithOpenApi()
//     .RequireAuthorization();

app.Run();