using System.Diagnostics;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Application.Features.Dashboard;
using RequestFlow.Extensions;
using RequestFlow.Models;
using RequestFlow.Persistence.Data;

namespace RequestFlow.Controllers;

public class HomeController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager) : Controller
{
    [Authorize]
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        var isManager = this.IsManager();
        var result = await mediator.Send(new GetDashboardQuery(userId ?? string.Empty, isManager), cancellationToken);
        var model = mapper.Map<DashboardViewModel>(result);
        await SetCreatedByDisplayNamesAsync(model.RecentRequests, cancellationToken);
        return View(model);
    }

    private async Task SetCreatedByDisplayNamesAsync(List<RequestListViewModel> requests, CancellationToken cancellationToken)
    {
        var userIds = requests.Select(r => r.CreatedByUserId).Where(id => !string.IsNullOrEmpty(id)).Distinct().ToList();
        var dict = new Dictionary<string, string>();
        foreach (var id in userIds)
        {
            var user = await userManager.FindByIdAsync(id);
            dict[id] = !string.IsNullOrEmpty(user?.DisplayName) ? user.DisplayName : user?.Email ?? id;
        }
        foreach (var r in requests)
            r.CreatedByDisplayName = dict.GetValueOrDefault(r.CreatedByUserId, r.CreatedByUserId);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
