using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Models;
using RequestFlow.Persistence.Data;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Controllers;

[Authorize(Roles = RoleNames.Admin)]
public class UsersController(UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var users = userManager.Users.ToList();
        var viewModels = new List<UserListViewModel>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            viewModels.Add(new UserListViewModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                DisplayName = user.DisplayName,
                Roles = string.Join(", ", roles)
            });
        }

        return View(viewModels);
    }
}
