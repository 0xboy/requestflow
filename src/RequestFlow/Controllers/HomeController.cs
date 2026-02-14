using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Application.Features.Dashboard;
using RequestFlow.Extensions;
using RequestFlow.Models;

namespace RequestFlow.Controllers;

public class HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper) : Controller
{
    [Authorize]
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        var isManager = this.IsManager();
        var result = await mediator.Send(new GetDashboardQuery(userId ?? string.Empty, isManager), cancellationToken);
        var model = mapper.Map<DashboardViewModel>(result);
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
