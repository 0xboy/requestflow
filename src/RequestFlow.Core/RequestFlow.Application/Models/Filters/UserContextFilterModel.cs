namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Shared across queries: identity and role (which user, is manager).
/// List, dashboard, detail etc. filter models can use this base.
/// </summary>
public record UserContextFilterModel
{
    /// <summary>Requesting user id</summary>
    public string? UserId { get; init; }

    /// <summary>Whether the user has the Manager role (approval permission)</summary>
    public bool IsManager { get; init; }
}
