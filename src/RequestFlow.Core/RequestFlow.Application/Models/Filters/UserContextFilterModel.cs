namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Tüm sorgularda ortak: kimlik ve yetki bilgisi (hangi kullanıcı, manager mı).
/// List, dashboard, detail vb. filter modelleri bu tabanı kullanabilir.
/// </summary>
public record UserContextFilterModel
{
    /// <summary>İstek yapan kullanıcı id</summary>
    public string? UserId { get; init; }

    /// <summary>Kullanıcı manager rolünde mi (onay yetkisi var mı)</summary>
    public bool IsManager { get; init; }
}
