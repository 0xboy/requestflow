namespace RequestFlow.Application.DTOs;

public record DashboardDto
{
    public int TotalRequestCount { get; init; }
    public int PendingApprovalCount { get; init; }
    public IReadOnlyList<RequestListDto> RecentRequests { get; init; } = Array.Empty<RequestListDto>();
    public bool IsManager { get; init; }
}
