using RequestFlow.Application.Interfaces.Services;
using MediatR;

namespace RequestFlow.Application.Features.Requests.Reject;

public class RejectRequestHandler(IRequestService requestService)
    : IRequestHandler<RejectRequestCommand, bool>
{
    public async Task<bool> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
        => await requestService.RejectRequestAsync(request.RequestId, request.UserId, request.RejectionReason, cancellationToken);
}
