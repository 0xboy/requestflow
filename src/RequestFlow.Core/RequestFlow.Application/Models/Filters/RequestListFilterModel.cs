namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Request listesi filtreleme ve sayfalama parametreleri.
/// Kullanıcı bağlamı (UserId, IsManager) + liste kriterleri + sayfalama.
/// </summary>
public record RequestListFilterModel : UserContextFilterModel
{
    /// <summary>Talep durumu (Draft, PendingApproval, Approved, Rejected)</summary>
    public string? Status { get; init; }

    /// <summary>Başlık, açıklama veya talep no üzerinde arama</summary>
    public string? Search { get; init; }

    /// <summary>Oluşturulma tarihi - başlangıç</summary>
    public DateTime? FromDate { get; init; }

    /// <summary>Oluşturulma tarihi - bitiş</summary>
    public DateTime? ToDate { get; init; }

    /// <summary>Sayfa numarası (1 tabanlı)</summary>
    public int Page { get; init; } = 1;

    /// <summary>Sayfa başına kayıt sayısı</summary>
    public int PageSize { get; init; } = 10;
}
