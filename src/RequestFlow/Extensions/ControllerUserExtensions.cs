using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RequestFlow.Extensions;

/// <summary>
/// Get current user info in the controller.
/// </summary>
public static class ControllerUserExtensions
{
    public static string? GetUserId(this ControllerBase controller)
        => controller.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public static bool IsManager(this ControllerBase controller)
        => controller.User.IsInRole("Manager");

    public static bool IsAdmin(this ControllerBase controller)
        => controller.User.IsInRole("Admin");
}
