using RequestFlow.Domain.Entities;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Interfaces.Repositories;

public interface IRequestRepository
{
    Task<Request?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Request?> GetByIdWithTypeAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Request>> GetListAsync(
        string? userId,
        bool isManager,
        string? status,
        string? search,
        DateTime? fromDate,
        DateTime? toDate,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(
        string? userId,
        bool isManager,
        string? status = null,
        CancellationToken cancellationToken = default);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    Task<Request> AddAsync(Request request, CancellationToken cancellationToken = default);
    Task UpdateAsync(Request request, CancellationToken cancellationToken = default);
    Task AddStatusHistoryAsync(RequestStatusHistory history, CancellationToken cancellationToken = default);
}
