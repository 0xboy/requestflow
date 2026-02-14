namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Talep sayısı sorgusu için filter: kullanıcı bağlamı + isteğe bağlı durum.
/// </summary>
public record RequestCountFilterModel : UserContextFilterModel
{
    /// <summary>Filtrelenecek talep durumu (null = tümü)</summary>
    public string? Status { get; init; }
}
