using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Models;

namespace RequestFlow.Controllers;

public class AccountController : Controller
{
    // TODO: Inject IMediator when authentication is implemented
    // private readonly IMediator _mediator;

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null, CancellationToken cancellationToken = default)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        // TODO: Send LoginCommand via MediatR
        // var result = await _mediator.Send(new LoginCommand(model.Email, model.Password, model.RememberMe), cancellationToken);
        // if (!result.Succeeded) { ModelState.AddModelError(string.Empty, "Invalid login attempt."); return View(model); }
        // await HttpContext.SignInAsync(...);

        return LocalRedirect(returnUrl ?? "/");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
    {
        // TODO: Send LogoutCommand via MediatR (or SignOut)
        // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Login));
    }
}
