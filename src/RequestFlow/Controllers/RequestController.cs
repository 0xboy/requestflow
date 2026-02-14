using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Requests.Approve;
using RequestFlow.Application.Features.Requests.Create;
using RequestFlow.Application.Features.Requests.GetDetail;
using RequestFlow.Application.Features.Requests.GetList;
using RequestFlow.Application.Features.Requests.GetTypes;
using RequestFlow.Application.Features.Requests.Reject;
using RequestFlow.Application.Features.Requests.SubmitForApproval;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Extensions;
using RequestFlow.Models;
using RequestFlow.Persistence.Data;

namespace RequestFlow.Controllers;

[Authorize]
public class RequestController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] RequestListFilterModel filter, [FromQuery] bool openCreate = false, CancellationToken cancellationToken = default)
    {
        var filterWithUser = filter with { UserId = this.GetUserId(), IsManager = this.IsManager() };
        var query = mapper.Map<GetRequestListQuery>(filterWithUser);
        var result = await mediator.Send(query, cancellationToken);
        var requests = mapper.Map<List<RequestListViewModel>>(result.Items);
        await SetCreatedByDisplayNamesAsync(requests, cancellationToken);
        var types = await mediator.Send(new GetRequestTypesQuery(), cancellationToken);
        ViewBag.RequestTypes = new SelectList(types, nameof(RequestTypeDto.Id), nameof(RequestTypeDto.Name));
        ViewBag.OpenCreate = openCreate;
        ViewBag.Filter = filter;
        ViewBag.CurrentUserId = this.GetUserId();
        ViewBag.IsManager = this.IsManager();
        return View(requests);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequestCreateViewModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index), new { openCreate = true });

        var cmd = mapper.Map<CreateRequestCommand>(model) with { UserId = this.GetUserId() ?? string.Empty };
        var id = await mediator.Send(cmd, cancellationToken);
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        var isManager = this.IsManager();
        var result = await mediator.Send(new GetRequestDetailQuery(id, userId ?? string.Empty, isManager), cancellationToken);
        if (result == null)
            return NotFound();

        var model = mapper.Map<RequestDetailViewModel>(result);
        if (!string.IsNullOrEmpty(model.CreatedBy))
        {
            var creator = await userManager.FindByIdAsync(model.CreatedBy);
            model.CreatedBy = !string.IsNullOrEmpty(creator?.DisplayName) ? creator.DisplayName : creator?.Email ?? model.CreatedBy;
        }
        foreach (var item in model.StatusHistory)
        {
            if (string.IsNullOrEmpty(item.ChangedByUserId)) continue;
            var user = await userManager.FindByIdAsync(item.ChangedByUserId);
            item.ChangedByDisplayName = !string.IsNullOrEmpty(user?.DisplayName) ? user.DisplayName! : user?.Email ?? item.ChangedByUserId;
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        var result = await mediator.Send(new GetRequestDetailQuery(id, userId ?? string.Empty, this.IsManager()), cancellationToken);
        if (result == null)
            return NotFound();
        if (!result.CanEdit)
            return RedirectToAction(nameof(Details), new { id });

        var types = await mediator.Send(new GetRequestTypesQuery(), cancellationToken);
        ViewBag.RequestTypes = new SelectList(types, nameof(RequestTypeDto.Id), nameof(RequestTypeDto.Name));
        var model = new RequestEditViewModel
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description ?? string.Empty,
            RequestTypeId = result.RequestTypeId,
            Priority = result.Priority.ToString(),
            Status = result.Status.ToString()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RequestEditViewModel model, CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        var result = await mediator.Send(new GetRequestDetailQuery(model.Id, userId ?? string.Empty, this.IsManager()), cancellationToken);
        if (result == null || !result.CanEdit)
            return RedirectToAction(nameof(Details), new { id = model.Id });

        if (!ModelState.IsValid)
        {
            var types = await mediator.Send(new GetRequestTypesQuery(), cancellationToken);
            ViewBag.RequestTypes = new SelectList(types, nameof(RequestTypeDto.Id), nameof(RequestTypeDto.Name));
            return View(model);
        }

        // TODO: Send UpdateRequestCommand via MediatR
        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitForApproval(int id, CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction(nameof(Details), new { id });
        var ok = await mediator.Send(new SubmitForApprovalCommand(id, userId), cancellationToken);
        if (!ok)
            return RedirectToAction(nameof(Details), new { id });
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetRequestDetailQuery(id, this.GetUserId() ?? string.Empty, true), cancellationToken);
        if (result == null || !result.CanApprove)
            return RedirectToAction(nameof(Details), new { id });
        var model = new ApproveRejectViewModel { RequestId = result.Id, RequestNo = result.RequestNo, Title = result.Title };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    [ActionName("Approve")]
    public async Task<IActionResult> ApproveConfirm(ApproveRejectViewModel model, CancellationToken cancellationToken = default)
    {
        var userId = this.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction(nameof(Details), new { id = model.RequestId });
        await mediator.Send(new ApproveRequestCommand(model.RequestId, userId), cancellationToken);
        return model.ReturnTo == "Details"
            ? RedirectToAction(nameof(Details), new { id = model.RequestId })
            : RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetRequestDetailQuery(id, this.GetUserId() ?? string.Empty, true), cancellationToken);
        if (result == null || !result.CanApprove)
            return RedirectToAction(nameof(Details), new { id });
        var vm = new ApproveRejectViewModel { RequestId = result.Id, RequestNo = result.RequestNo, Title = result.Title };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    [ActionName("Reject")]
    public async Task<IActionResult> RejectConfirm(ApproveRejectViewModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(model.RejectionReason))
        {
            TempData["RejectRequestId"] = model.RequestId;
            TempData["RejectRequestNo"] = model.RequestNo;
            TempData["RejectTitle"] = model.Title;
            TempData["RejectError"] = "Rejection reason is required.";
            TempData["RejectReturnTo"] = model.ReturnTo;
            return model.ReturnTo == "Details"
                ? RedirectToAction(nameof(Details), new { id = model.RequestId })
                : RedirectToAction(nameof(Index));
        }
        var userId = this.GetUserId();
        if (string.IsNullOrEmpty(userId))
            return RedirectToAction(nameof(Details), new { id = model.RequestId });
        await mediator.Send(new RejectRequestCommand(model.RequestId, userId, model.RejectionReason!), cancellationToken);
        return model.ReturnTo == "Details"
            ? RedirectToAction(nameof(Details), new { id = model.RequestId })
            : RedirectToAction(nameof(Index));
    }
}
