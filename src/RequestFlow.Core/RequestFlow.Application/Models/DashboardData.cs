using RequestFlow.Application.Models.Requests;

namespace RequestFlow.Application.Models;

/// <summary>
/// Raw data aggregated for dashboard. Repo + service output fills it;
/// mapped to DashboardModel via mapper (avoids manual new DashboardModel).
/// </summary>
public record DashboardData(
    int TotalRequestCount,
    int PendingApprovalCount,
    int ApprovedCount,
    int RejectedCount,
    IReadOnlyList<RequestListModel> RecentRequests,
    bool IsManager);
