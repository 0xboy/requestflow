using RequestFlow.Application.Interfaces.Services;
using MediatR;

namespace RequestFlow.Application.Features.Requests.Approve;

public class ApproveRequestHandler(IRequestService requestService)
    : IRequestHandler<ApproveRequestCommand, bool>
{
    public async Task<bool> Handle(ApproveRequestCommand request, CancellationToken cancellationToken)
        => await requestService.ApproveRequestAsync(request.RequestId, request.UserId, cancellationToken);
}
