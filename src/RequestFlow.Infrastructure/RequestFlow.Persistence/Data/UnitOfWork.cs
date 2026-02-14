using RequestFlow.Application.Interfaces;

namespace RequestFlow.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly RequestFlowDbContext _context;

    public UnitOfWork(RequestFlowDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
