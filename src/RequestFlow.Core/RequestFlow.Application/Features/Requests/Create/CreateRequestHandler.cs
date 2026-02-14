using RequestFlow.Application.Interfaces;
using RequestFlow.Domain.Entities;
using RequestFlow.Shared.Constants;
using MediatR;

namespace RequestFlow.Application.Features.Requests.Create;

public class CreateRequestHandler(
    IRequestRepository requestRepository,
    IRequestTypeRepository requestTypeRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRequestCommand, int>
{
    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var requestType = await requestTypeRepository.GetByIdAsync(request.RequestTypeId, cancellationToken)
            ?? throw new InvalidOperationException($"Request type {request.RequestTypeId} not found.");

        var nextNo = await requestRepository.GetTotalCountAsync(cancellationToken) + 1;
        var requestNo = $"REQ-{nextNo:D5}";

        var entity = new Request
        {
            RequestNo = requestNo,
            Title = request.Title,
            Description = request.Description,
            RequestTypeId = request.RequestTypeId,
            Priority = request.Priority,
            Status = RequestStatus.Draft,
            CreatedByUserId = request.UserId,
            CreatedDate = DateTime.UtcNow
        };

        var created = await requestRepository.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return created.Id;
    }
}
