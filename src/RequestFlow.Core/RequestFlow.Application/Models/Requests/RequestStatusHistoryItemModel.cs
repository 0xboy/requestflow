using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Models.Requests;

public record RequestStatusHistoryItemModel
{
    public RequestStatus FromStatus { get; init; }
    public RequestStatus ToStatus { get; init; }
    public string? Comment { get; init; }
    public string ChangedByUserId { get; init; } = string.Empty;
    public DateTime ChangedDate { get; init; }
}
