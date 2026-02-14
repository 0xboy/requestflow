namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Paging parameters. Other filter models may include these or use this model.
/// </summary>
public record PagingFilterModel
{
    /// <summary>Page number (1-based)</summary>
    public int Page { get; init; } = 1;

    /// <summary>Page size</summary>
    public int PageSize { get; init; } = 10;
}
