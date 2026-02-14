using Microsoft.EntityFrameworkCore;
using RequestFlow.Application.Interfaces.Repositories;
using RequestFlow.Domain.Entities;
using RequestFlow.Persistence;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Persistence.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly RequestFlowDbContext _context;

    public RequestRepository(RequestFlowDbContext context)
    {
        _context = context;
    }

    public async Task<Request?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Requests.FindAsync([id], cancellationToken);
    }

    public async Task<Request?> GetByIdWithTypeAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Requests
            .Include(r => r.RequestType)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Request>> GetListAsync(
        string? userId,
        bool isManager,
        string? status,
        string? search,
        DateTime? fromDate,
        DateTime? toDate,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Requests.AsQueryable();

        if (!isManager && !string.IsNullOrEmpty(userId))
            query = query.Where(r => r.CreatedByUserId == userId);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<RequestStatus>(status, out var statusEnum))
            query = query.Where(r => r.Status == statusEnum);

        if (!string.IsNullOrEmpty(search))
        {
            var term = search.ToLower();
            query = query.Where(r =>
                r.Title.ToLower().Contains(term) ||
                (r.Description != null && r.Description.ToLower().Contains(term)) ||
                r.RequestNo.ToLower().Contains(term));
        }

        if (fromDate.HasValue)
            query = query.Where(r => r.CreatedDate >= fromDate.Value);

        if (toDate.HasValue)
        {
            var endOfDay = toDate.Value.Date.AddDays(1);
            query = query.Where(r => r.CreatedDate < endOfDay);
        }

        return await query
            .OrderByDescending(r => r.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(
        string? userId,
        bool isManager,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Requests.AsQueryable();

        if (!isManager && !string.IsNullOrEmpty(userId))
            query = query.Where(r => r.CreatedByUserId == userId);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<RequestStatus>(status, out var statusEnum))
            query = query.Where(r => r.Status == statusEnum);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Requests.CountAsync(cancellationToken);
    }

    public async Task<Request> AddAsync(Request request, CancellationToken cancellationToken = default)
    {
        await _context.Requests.AddAsync(request, cancellationToken);
        return request;
    }

    public Task UpdateAsync(Request request, CancellationToken cancellationToken = default)
    {
        _context.Requests.Update(request);
        return Task.CompletedTask;
    }

    public async Task AddStatusHistoryAsync(RequestStatusHistory history, CancellationToken cancellationToken = default)
    {
        await _context.RequestStatusHistories.AddAsync(history, cancellationToken);
    }
}
