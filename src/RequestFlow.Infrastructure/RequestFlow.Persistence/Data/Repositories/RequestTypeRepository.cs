using Microsoft.EntityFrameworkCore;
using RequestFlow.Application.Interfaces.Repositories;
using RequestFlow.Domain.Entities;
using RequestFlow.Persistence;

namespace RequestFlow.Persistence.Repositories;

public class RequestTypeRepository : IRequestTypeRepository
{
    private readonly RequestFlowDbContext _context;

    public RequestTypeRepository(RequestFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<RequestType>> GetActiveTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.RequestTypes
            .Where(rt => rt.IsActive)
            .OrderBy(rt => rt.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<RequestType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.RequestTypes.FindAsync([id], cancellationToken);
    }
}
