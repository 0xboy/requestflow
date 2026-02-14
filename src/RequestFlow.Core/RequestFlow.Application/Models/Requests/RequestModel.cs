using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Models.Requests;

/// <summary>
/// Application layer model for request detail. Mapped to DTO.
/// </summary>
public record RequestModel
{
    public int Id { get; init; }
    public string RequestNo { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int RequestTypeId { get; init; }
    public string RequestTypeName { get; init; } = string.Empty;
    public Priority Priority { get; init; }
    public RequestStatus Status { get; init; }
    public string CreatedByUserId { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public bool CanEdit { get; init; }
    public bool CanApprove { get; init; }
    public bool CanSubmitForApproval { get; init; }
    public IReadOnlyList<RequestStatusHistoryItemModel> StatusHistory { get; init; } = Array.Empty<RequestStatusHistoryItemModel>();
}
