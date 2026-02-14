using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Models.Requests;

/// <summary>
/// Application model for a request list item. Mapped to DTO.
/// </summary>
public record RequestListModel
{
    public int Id { get; init; }
    public string RequestNo { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public RequestStatus Status { get; init; }
    public Priority Priority { get; init; }
    public DateTime CreatedDate { get; init; }
    public string CreatedByUserId { get; init; } = string.Empty;
}
