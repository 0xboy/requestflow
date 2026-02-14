using AutoMapper;
using RequestFlow.Application.Interfaces.Repositories;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Application.Models;
using RequestFlow.Application.Models.Filters;
using RequestFlow.Application.Models.Requests;
using RequestFlow.Application.Models.RequestTypes;
using RequestFlow.Domain.Entities;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Infrastructure.Services;

public class RequestService(
    IRequestRepository requestRepository,
    IRequestTypeRepository requestTypeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestService
{
    public async Task<(IReadOnlyList<RequestListModel> Items, int TotalCount)> GetListAsync(
        RequestListFilterModel filter,
        CancellationToken cancellationToken = default)
    {
        var items = await requestRepository.GetListAsync(
            filter.UserId,
            filter.IsManager,
            filter.Status,
            filter.Search,
            filter.FromDate,
            filter.ToDate,
            filter.Page,
            filter.PageSize,
            cancellationToken);
        var countFilter = new RequestCountFilterModel { UserId = filter.UserId, IsManager = filter.IsManager, Status = filter.Status };
        var total = await GetCountAsync(countFilter, cancellationToken);
        var models = mapper.Map<IReadOnlyList<RequestListModel>>(items);
        return (models, total);
    }

    public async Task<RequestModel?> GetByIdWithTypeAsync(
        RequestDetailFilterModel filter,
        CancellationToken cancellationToken = default)
    {
        var entity = await requestRepository.GetByIdWithTypeAsync(filter.Id, cancellationToken);
        if (entity == null) return null;

        var model = mapper.Map<RequestModel>(entity);
        var canEdit = entity.CreatedByUserId == filter.UserId && entity.Status == RequestStatus.Draft;
        var canApprove = filter.IsManager && entity.Status == RequestStatus.PendingApproval;
        return model with { CanEdit = canEdit, CanApprove = canApprove };
    }

    public Task<int> GetCountAsync(
        RequestCountFilterModel filter,
        CancellationToken cancellationToken = default)
        => requestRepository.GetCountAsync(filter.UserId, filter.IsManager, filter.Status, cancellationToken);

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
        => requestRepository.GetTotalCountAsync(cancellationToken);

    public async Task<IReadOnlyList<RequestTypeModel>> GetActiveTypesAsync(CancellationToken cancellationToken = default)
    {
        var types = await requestTypeRepository.GetActiveTypesAsync(cancellationToken);
        return mapper.Map<IReadOnlyList<RequestTypeModel>>(types);
    }

    public async Task<int> CreateRequestAsync(CreateRequestModel model, CancellationToken cancellationToken = default)
    {
        _ = await requestTypeRepository.GetByIdAsync(model.RequestTypeId, cancellationToken)
            ?? throw new InvalidOperationException($"Request type {model.RequestTypeId} not found.");

        var entity = mapper.Map<Request>(model);
        var nextNo = await requestRepository.GetTotalCountAsync(cancellationToken) + 1;
        entity.RequestNo = $"REQ-{nextNo:D5}";
        entity.Status = RequestStatus.Draft;
        entity.CreatedDate = DateTime.UtcNow;

        var created = await requestRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return created.Id;
    }

    public async Task<DashboardModel> GetDashboardDataAsync(
        DashboardFilterModel filter,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await GetCountAsync(new RequestCountFilterModel { UserId = filter.UserId, IsManager = filter.IsManager, Status = null }, cancellationToken);
        var pendingCount = await GetCountAsync(new RequestCountFilterModel { UserId = filter.UserId, IsManager = filter.IsManager, Status = RequestStatus.PendingApproval.ToString() }, cancellationToken);
        var recentEntities = await requestRepository.GetListAsync(
            filter.UserId, filter.IsManager, null, null, null, null, 1, 5, cancellationToken);
        var recent = mapper.Map<IReadOnlyList<RequestListModel>>(recentEntities);

        var data = new DashboardData(totalCount, pendingCount, recent, filter.IsManager);
        return mapper.Map<DashboardModel>(data);
    }
}
