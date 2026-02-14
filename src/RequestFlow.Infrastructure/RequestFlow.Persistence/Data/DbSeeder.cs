using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RequestFlow.Domain.Entities;
using RequestFlow.Persistence.Data;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Persistence;

public static class DbSeeder
{
    private const string DefaultPassword = "Test123!";

    public static async Task SeedAsync(RequestFlowDbContext context, CancellationToken cancellationToken = default)
    {
        await SeedRequestTypesAsync(context, cancellationToken);
    }

    public static async Task SeedIdentityAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(roleManager, cancellationToken);
        await SeedUsersAsync(userManager, cancellationToken);
    }

    private static async Task SeedRequestTypesAsync(RequestFlowDbContext context, CancellationToken cancellationToken)
    {
        if (await context.RequestTypes.AnyAsync(cancellationToken))
            return;

        var types = new[]
        {
            new RequestType { Name = "Leave Request", Description = "Annual leave, sick leave, etc.", IsActive = true },
            new RequestType { Name = "Equipment Request", Description = "Laptop, monitor, accessories", IsActive = true },
            new RequestType { Name = "Training Request", Description = "Courses, certifications, workshops", IsActive = true },
            new RequestType { Name = "Expense Request", Description = "Travel, meals, other expenses", IsActive = true },
            new RequestType { Name = "Other", Description = "Other types of requests", IsActive = true }
        };

        await context.RequestTypes.AddRangeAsync(types, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, CancellationToken cancellationToken)
    {
        foreach (var roleName in new[] { RoleNames.User, RoleNames.Manager, RoleNames.Admin })
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, CancellationToken cancellationToken)
    {
        // User (User role)
        var userEmail = "user@test.com";
        if (await userManager.FindByEmailAsync(userEmail) == null)
        {
            var user = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                EmailConfirmed = true,
                DisplayName = "Test User"
            };
            await userManager.CreateAsync(user, DefaultPassword);
            await userManager.AddToRoleAsync(user, RoleNames.User);
        }

        // Manager
        var managerEmail = "manager@test.com";
        if (await userManager.FindByEmailAsync(managerEmail) == null)
        {
            var manager = new ApplicationUser
            {
                UserName = managerEmail,
                Email = managerEmail,
                EmailConfirmed = true,
                DisplayName = "Test Manager"
            };
            await userManager.CreateAsync(manager, DefaultPassword);
            await userManager.AddToRoleAsync(manager, RoleNames.Manager);
        }

        // Admin (User, Manager and Admin roles)
        var adminEmail = "admin@test.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                DisplayName = "Test Admin"
            };
            await userManager.CreateAsync(admin, DefaultPassword);
            await userManager.AddToRoleAsync(admin, RoleNames.Admin);
            await userManager.AddToRoleAsync(admin, RoleNames.Manager);
            await userManager.AddToRoleAsync(admin, RoleNames.User);
        }
    }
}
