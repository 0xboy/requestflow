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
        var isCreator = entity.CreatedByUserId == filter.UserId;
        var canEdit = isCreator && (entity.Status == RequestStatus.Draft || entity.Status == RequestStatus.Rejected);
        var canApprove = filter.IsManager && entity.Status == RequestStatus.PendingApproval;
        var canSubmitForApproval = isCreator && entity.Status == RequestStatus.Draft;
        return model with { CanEdit = canEdit, CanApprove = canApprove, CanSubmitForApproval = canSubmitForApproval };
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
        var approvedCount = await GetCountAsync(new RequestCountFilterModel { UserId = filter.UserId, IsManager = filter.IsManager, Status = RequestStatus.Approved.ToString() }, cancellationToken);
        var rejectedCount = await GetCountAsync(new RequestCountFilterModel { UserId = filter.UserId, IsManager = filter.IsManager, Status = RequestStatus.Rejected.ToString() }, cancellationToken);
        var recentEntities = await requestRepository.GetListAsync(
            filter.UserId, filter.IsManager, null, null, null, null, 1, 5, cancellationToken);
        var recent = mapper.Map<IReadOnlyList<RequestListModel>>(recentEntities);

        var data = new DashboardData(totalCount, pendingCount, approvedCount, rejectedCount, recent, filter.IsManager);
        return mapper.Map<DashboardModel>(data);
    }

    public async Task<bool> SubmitForApprovalAsync(int requestId, string userId, CancellationToken cancellationToken = default)
    {
        var entity = await requestRepository.GetByIdAsync(requestId, cancellationToken);
        if (entity == null || entity.Status != RequestStatus.Draft || entity.CreatedByUserId != userId)
            return false;

        var fromStatus = entity.Status;
        entity.Status = RequestStatus.PendingApproval;
        entity.ModifiedDate = DateTime.UtcNow;
        await requestRepository.UpdateAsync(entity, cancellationToken);
        await requestRepository.AddStatusHistoryAsync(new RequestStatusHistory
        {
            RequestId = requestId,
            FromStatus = fromStatus,
            ToStatus = RequestStatus.PendingApproval,
            ChangedByUserId = userId,
            ChangedDate = DateTime.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ApproveRequestAsync(int requestId, string userId, CancellationToken cancellationToken = default)
    {
        var entity = await requestRepository.GetByIdAsync(requestId, cancellationToken);
        if (entity == null || entity.Status != RequestStatus.PendingApproval)
            return false;

        var fromStatus = entity.Status;
        entity.Status = RequestStatus.Approved;
        entity.ModifiedDate = DateTime.UtcNow;
        await requestRepository.UpdateAsync(entity, cancellationToken);
        await requestRepository.AddStatusHistoryAsync(new RequestStatusHistory
        {
            RequestId = requestId,
            FromStatus = fromStatus,
            ToStatus = RequestStatus.Approved,
            ChangedByUserId = userId,
            ChangedDate = DateTime.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RejectRequestAsync(int requestId, string userId, string rejectionReason, CancellationToken cancellationToken = default)
    {
        var entity = await requestRepository.GetByIdAsync(requestId, cancellationToken);
        if (entity == null || entity.Status != RequestStatus.PendingApproval)
            return false;

        var fromStatus = entity.Status;
        entity.Status = RequestStatus.Rejected;
        entity.ModifiedDate = DateTime.UtcNow;
        await requestRepository.UpdateAsync(entity, cancellationToken);
        await requestRepository.AddStatusHistoryAsync(new RequestStatusHistory
        {
            RequestId = requestId,
            FromStatus = fromStatus,
            ToStatus = RequestStatus.Rejected,
            Comment = rejectionReason,
            ChangedByUserId = userId,
            ChangedDate = DateTime.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
