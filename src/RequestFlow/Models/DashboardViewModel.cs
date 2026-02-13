namespace RequestFlow.Models;

public class DashboardViewModel
{
    public int TotalRequestCount { get; set; }
    public int PendingApprovalCount { get; set; }
    public List<RequestListViewModel> RecentRequests { get; set; } = new();
    public bool IsManager { get; set; }
}
