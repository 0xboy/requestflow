using RequestFlow.Application.Models.Requests;

namespace RequestFlow.Application.Models;

/// <summary>
/// Dashboard verisi için application modeli. DTO'ya map edilir.
/// </summary>
public record DashboardModel
{
    public int TotalRequestCount { get; init; }
    public int PendingApprovalCount { get; init; }
    public IReadOnlyList<RequestListModel> RecentRequests { get; init; } = Array.Empty<RequestListModel>();
    public bool IsManager { get; init; }
}
