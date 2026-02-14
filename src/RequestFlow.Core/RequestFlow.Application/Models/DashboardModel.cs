using RequestFlow.Application.Models.Requests;

namespace RequestFlow.Application.Models;

/// <summary>
/// Application model for dashboard data. Mapped to DTO.
/// </summary>
public record DashboardModel
{
    public int TotalRequestCount { get; init; }
    public int PendingApprovalCount { get; init; }
    public int ApprovedCount { get; init; }
    public int RejectedCount { get; init; }
    public IReadOnlyList<RequestListModel> RecentRequests { get; init; } = Array.Empty<RequestListModel>();
    public bool IsManager { get; init; }
}
