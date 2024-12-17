// Program.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using UserMService;
using UserMService.Contexts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<UserIdentityContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserClaimsPrincipalFactory<StoreUser>, CustomUserClaimsPrincipalFactory>(); // add default "User" Role

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<StoreUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserIdentityContext>()
    .AddDefaultTokenProviders();



builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) // seeding, making sure the roles are created
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(roleManager);
}

using (var scope = app.Services.CreateScope()) // adding the admin users
{
    try
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<StoreUser>>();
        var adminsSection = builder.Configuration.GetSection("Admins");
        var admins = adminsSection.Get<Dictionary<string, Admin>>();

        foreach (var admin in admins.Values)
        {
            var user = await userManager.FindByEmailAsync(admin.Email);
            if (user == null)
            {
                var newUser = new StoreUser { UserName = admin.Email, Email = admin.Email };
                await userManager.CreateAsync(newUser, admin.Password);
                await userManager.AddToRoleAsync(newUser, "Admin");
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to add admin profile to db: " + e);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("identity").MapIdentityApi<StoreUser>();

app.Run();

internal class Admin
{
    public string Email { get; set; }
    public string Password { get; set; }
}