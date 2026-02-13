using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Models;

namespace RequestFlow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    // TODO: Inject IMediator when dashboard feature is implemented
    // private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        // TODO: Send GetDashboardQuery via MediatR
        // var result = await _mediator.Send(new GetDashboardQuery(), cancellationToken);

        var model = new DashboardViewModel { TotalRequestCount = 0, PendingApprovalCount = 0, IsManager = false };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
