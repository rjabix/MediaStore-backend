using Microsoft.AspNetCore.Identity;

namespace UserMService.Contexts;

public class RoleInitializer
{
    /// <summary>
    /// Initializes the roles in the database if they do not exist.
    /// </summary>
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = ["Admin", "User", "Manager"]; // available roles
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}