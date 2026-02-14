using RequestFlow.Application.Models.Requests;

namespace RequestFlow.Application.Models;

/// <summary>
/// Dashboard için toplanan ham veri. Repo + servis çıktıları bunu doldurur,
/// mapper ile DashboardModel'e map edilir (elle new DashboardModel yazmamak için).
/// </summary>
public record DashboardData(
    int TotalRequestCount,
    int PendingApprovalCount,
    IReadOnlyList<RequestListModel> RecentRequests,
    bool IsManager);
