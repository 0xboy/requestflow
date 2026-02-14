using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Persistence.Data;

namespace RequestFlow.ViewComponents;

public class CurrentUserViewComponent(UserManager<ApplicationUser> userManager) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return View<string?>(null);

        var user = await userManager.FindByIdAsync(userId);
        var displayName = !string.IsNullOrEmpty(user?.DisplayName)
            ? user.DisplayName
            : user?.Email ?? HttpContext.User.Identity?.Name;
        return View("Default", displayName);
    }
}
