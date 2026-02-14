using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetDetail;

public class GetRequestDetailHandler(IRequestRepository requestRepository)
    : IRequestHandler<GetRequestDetailQuery, RequestDto?>
{
    public async Task<RequestDto?> Handle(GetRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var entity = await requestRepository.GetByIdWithTypeAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var canEdit = entity.CreatedByUserId == request.UserId && entity.Status == Shared.Constants.RequestStatus.Draft;
        var canApprove = request.IsManager && entity.Status == Shared.Constants.RequestStatus.PendingApproval;

        return new RequestDto
        {
            Id = entity.Id,
            RequestNo = entity.RequestNo,
            Title = entity.Title,
            Description = entity.Description,
            RequestTypeId = entity.RequestTypeId,
            RequestTypeName = entity.RequestType.Name,
            Priority = entity.Priority,
            Status = entity.Status,
            CreatedByUserId = entity.CreatedByUserId,
            CreatedDate = entity.CreatedDate,
            CanEdit = canEdit,
            CanApprove = canApprove
        };
    }
}
