using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Models.Requests;

/// <summary>
/// Application model for creating a new request. Service maps it to entity.
/// </summary>
public record CreateRequestModel
{
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int RequestTypeId { get; init; }
    public Priority Priority { get; init; }
    public string UserId { get; init; } = string.Empty;
}
