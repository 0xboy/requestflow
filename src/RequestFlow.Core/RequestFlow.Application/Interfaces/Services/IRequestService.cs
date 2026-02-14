using RequestFlow.Application.Models;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Application.Models.Requests;
using RequestFlow.Application.Models.RequestTypes;

namespace RequestFlow.Application.Interfaces.Services;

/// <summary>
/// Request ile ilgili okuma/yazma işlemleri. Model alır/döner; entity Application'a sızmaz.
/// </summary>
public interface IRequestService
{
    Task<(IReadOnlyList<RequestListModel> Items, int TotalCount)> GetListAsync(
        RequestListFilterModel filter,
        CancellationToken cancellationToken = default);

    Task<RequestModel?> GetByIdWithTypeAsync(
        RequestDetailFilterModel filter,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        RequestCountFilterModel filter,
        CancellationToken cancellationToken = default);

    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RequestTypeModel>> GetActiveTypesAsync(CancellationToken cancellationToken = default);

    Task<int> CreateRequestAsync(CreateRequestModel model, CancellationToken cancellationToken = default);

    Task<DashboardModel> GetDashboardDataAsync(
        DashboardFilterModel filter,
        CancellationToken cancellationToken = default);
}
