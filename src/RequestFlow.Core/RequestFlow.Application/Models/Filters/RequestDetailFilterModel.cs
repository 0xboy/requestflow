namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Tek talep detayı için filter: kullanıcı bağlamı + talep id.
/// </summary>
public record RequestDetailFilterModel : UserContextFilterModel
{
    /// <summary>Talep id</summary>
    public int Id { get; init; }
}
