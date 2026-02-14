using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces;
using RequestFlow.Shared.Constants;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetList;

public class GetRequestListHandler(IRequestRepository requestRepository)
    : IRequestHandler<GetRequestListQuery, RequestListResult>
{
    public async Task<RequestListResult> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
    {
        var statusFilter = !string.IsNullOrEmpty(request.Status) && Enum.TryParse<RequestStatus>(request.Status, out var s) ? s.ToString() : null;

        var items = await requestRepository.GetListAsync(
            request.UserId,
            request.IsManager,
            statusFilter,
            request.Search,
            request.FromDate,
            request.ToDate,
            request.Page,
            request.PageSize,
            cancellationToken);

        var total = await requestRepository.GetCountAsync(
            request.UserId,
            request.IsManager,
            statusFilter,
            cancellationToken);

        var dtos = items.Select(r => new RequestListDto
        {
            Id = r.Id,
            RequestNo = r.RequestNo,
            Title = r.Title,
            Status = r.Status,
            Priority = r.Priority,
            CreatedDate = r.CreatedDate
        }).ToList();

        return new RequestListResult(dtos, total);
    }
}
