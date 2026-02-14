using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RequestFlow.Controllers;

public class ErrorController() : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public new IActionResult Unauthorized()
    {
        return View();
    }
}