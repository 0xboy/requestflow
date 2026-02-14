using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces;
using RequestFlow.Shared.Constants;
using MediatR;

namespace RequestFlow.Application.Features.Dashboard;

public class GetDashboardHandler(IRequestRepository requestRepository)
    : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await requestRepository.GetCountAsync(request.UserId, request.IsManager, cancellationToken: cancellationToken);
        var pendingCount = await requestRepository.GetCountAsync(request.UserId, request.IsManager, RequestStatus.PendingApproval.ToString(), cancellationToken);

        var recentRequests = await requestRepository.GetListAsync(
            request.UserId,
            request.IsManager,
            status: null,
            search: null,
            fromDate: null,
            toDate: null,
            page: 1,
            pageSize: 5,
            cancellationToken);

        var dtos = recentRequests.Select(r => new RequestListDto
        {
            Id = r.Id,
            RequestNo = r.RequestNo,
            Title = r.Title,
            Status = r.Status,
            Priority = r.Priority,
            CreatedDate = r.CreatedDate
        }).ToList();

        return new DashboardDto
        {
            TotalRequestCount = totalCount,
            PendingApprovalCount = pendingCount,
            RecentRequests = dtos,
            IsManager = request.IsManager
        };
    }
}
