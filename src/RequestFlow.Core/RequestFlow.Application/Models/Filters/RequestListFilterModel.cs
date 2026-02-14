namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Request list filtering and paging parameters.
/// User context (UserId, IsManager) + list criteria + paging.
/// </summary>
public record RequestListFilterModel : UserContextFilterModel
{
    /// <summary>Request status (Draft, PendingApproval, Approved, Rejected)</summary>
    public string? Status { get; init; }

    /// <summary>Search on title, description or request number</summary>
    public string? Search { get; init; }

    /// <summary>Created date - from</summary>
    public DateTime? FromDate { get; init; }

    /// <summary>Created date - to</summary>
    public DateTime? ToDate { get; init; }

    /// <summary>Page number (1-based)</summary>
    public int Page { get; init; } = 1;

    /// <summary>Page size</summary>
    public int PageSize { get; init; } = 10;
}
