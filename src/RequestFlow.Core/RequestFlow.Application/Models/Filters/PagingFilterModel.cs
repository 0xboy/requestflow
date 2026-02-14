namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Sayfalama parametreleri. Diğer filtre modelleri bu değerleri içerebilir veya bu modeli kullanabilir.
/// </summary>
public record PagingFilterModel
{
    /// <summary>Sayfa numarası (1 tabanlı)</summary>
    public int Page { get; init; } = 1;

    /// <summary>Sayfa başına kayıt sayısı</summary>
    public int PageSize { get; init; } = 10;
}
