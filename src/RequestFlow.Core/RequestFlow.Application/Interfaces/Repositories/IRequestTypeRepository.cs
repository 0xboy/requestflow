using RequestFlow.Domain.Entities;

namespace RequestFlow.Application.Interfaces.Repositories;

public interface IRequestTypeRepository
{
    Task<IReadOnlyList<RequestType>> GetActiveTypesAsync(CancellationToken cancellationToken = default);
    Task<RequestType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
