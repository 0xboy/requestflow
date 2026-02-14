using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Requests.Create;
using RequestFlow.Application.Features.Requests.GetDetail;
using RequestFlow.Application.Features.Requests.GetList;
using RequestFlow.Application.Features.Requests.GetTypes;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Extensions;
using RequestFlow.Models;

namespace RequestFlow.Controllers;

[Authorize]
public class RequestController(IMediator mediator, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] RequestListFilterModel filter, CancellationToken cancellationToken = default)
    {
        var filterWithUser = filter with { UserId = this.GetUserId(), IsManager = this.IsManager() };
        var query = mapper.Map<GetRequestListQuery>(filterWithUser);
        var result = await mediator.Send(query, cancellationToken);
        var requests = mapper.Map<List<RequestListViewModel>>(result.Items);
        return View(requests);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        var types = await mediator.Send(new GetRequestTypesQuery(), cancellationToken);
        ViewBag.RequestTypes = new SelectList(types, nameof(RequestTypeDto.Id), nameof(RequestTypeDto.Name));
        return View(new RequestCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequestCreateViewModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return View(model);

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
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Send GetRequestForEditQuery via MediatR
        // var result = await mediator.Send(new GetRequestForEditQuery(id), cancellationToken);
        // if (result == null) return NotFound();

        var model = new RequestEditViewModel { Id = id, Title = "Sample", Status = "Draft" };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RequestEditViewModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return View(model);

        // TODO: Send UpdateRequestCommand via MediatR
        // await mediator.Send(new UpdateRequestCommand(model.Id, model.Title, model.Description, model.RequestTypeId, model.Priority), cancellationToken);

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Approve(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Send GetRequestForApprovalQuery via MediatR
        var model = new ApproveRejectViewModel { RequestId = id, RequestNo = $"REQ-{id:D5}", Title = "Sample" };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager")]
    [ActionName("Approve")]
    public async Task<IActionResult> ApproveConfirm(ApproveRejectViewModel model, CancellationToken cancellationToken = default)
    {
        // TODO: Send ApproveRequestCommand via MediatR
        // await mediator.Send(new ApproveRequestCommand(model.RequestId), cancellationToken);

        return RedirectToAction(nameof(Details), new { id = model.RequestId });
    }

    [HttpGet]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Reject(int id, CancellationToken cancellationToken = default)
    {
        var vm = new ApproveRejectViewModel { RequestId = id, RequestNo = $"REQ-{id:D5}", Title = "Sample" };
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
            ModelState.AddModelError(nameof(model.RejectionReason), "Rejection reason is required.");
            return View(model);
        }

        // TODO: Send RejectRequestCommand via MediatR
        // await mediator.Send(new RejectRequestCommand(model.RequestId, model.RejectionReason!), cancellationToken);

        return RedirectToAction(nameof(Details), new { id = model.RequestId });
    }
}
