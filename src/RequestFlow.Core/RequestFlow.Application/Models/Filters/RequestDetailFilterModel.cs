namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Filter for single request detail: user context + request id.
/// </summary>
public record RequestDetailFilterModel : UserContextFilterModel
{
    /// <summary>Request id</summary>
    public int Id { get; init; }
}
