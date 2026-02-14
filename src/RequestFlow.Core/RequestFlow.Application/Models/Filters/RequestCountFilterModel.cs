namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Filter for request count query: user context + optional status.
/// </summary>
public record RequestCountFilterModel : UserContextFilterModel
{
    /// <summary>Request status to filter by (null = all)</summary>
    public string? Status { get; init; }
}
