using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestFlow.Models;

namespace RequestFlow.Controllers;

[Authorize]
public class RequestController : Controller
{
    // TODO: Inject IMediator when request features are implemented
    // private readonly IMediator _mediator;

    [HttpGet]
    public async Task<IActionResult> Index(string? status, string? search, DateTime? fromDate, DateTime? toDate, int page = 1, CancellationToken cancellationToken = default)
    {
        // TODO: Send GetRequestListQuery via MediatR
        // var result = await _mediator.Send(new GetRequestListQuery { Status = status, Search = search, FromDate = fromDate, ToDate = toDate, Page = page }, cancellationToken);

        var requests = new List<RequestListViewModel>(); // Placeholder - replace with result
        return View(requests);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new RequestCreateViewModel
        {
            // TODO: Load RequestTypes from GetRequestTypesQuery
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RequestCreateViewModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return View(model);

        // TODO: Send CreateRequestCommand via MediatR
        // var result = await _mediator.Send(new CreateRequestCommand(model.Title, model.Description, model.RequestTypeId, model.Priority), cancellationToken);
        // return RedirectToAction(nameof(Details), new { id = result.Id });

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Send GetRequestDetailQuery via MediatR
        // var result = await _mediator.Send(new GetRequestDetailQuery(id), cancellationToken);
        // if (result == null) return NotFound();

        var model = new RequestDetailViewModel { Id = id, RequestNo = $"REQ-{id:D5}", Title = "Sample", Status = "Draft", CanEdit = true, CanApprove = false };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
    {
        // TODO: Send GetRequestForEditQuery via MediatR
        // var result = await _mediator.Send(new GetRequestForEditQuery(id), cancellationToken);
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
        // await _mediator.Send(new UpdateRequestCommand(model.Id, model.Title, model.Description, model.RequestTypeId, model.Priority), cancellationToken);

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
        // await _mediator.Send(new ApproveRequestCommand(model.RequestId), cancellationToken);

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
        // await _mediator.Send(new RejectRequestCommand(model.RequestId, model.RejectionReason!), cancellationToken);

        return RedirectToAction(nameof(Details), new { id = model.RequestId });
    }
}
