namespace RequestFlow.Application.Models.Filters;

/// <summary>
/// Dashboard verisi için filter; sadece kullanıcı bağlamı.
/// </summary>
public record DashboardFilterModel : UserContextFilterModel;
