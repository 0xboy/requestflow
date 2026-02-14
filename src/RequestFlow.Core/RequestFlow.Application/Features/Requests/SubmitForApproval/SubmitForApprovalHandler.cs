using RequestFlow.Application.Interfaces.Services;
using MediatR;

namespace RequestFlow.Application.Features.Requests.SubmitForApproval;

public class SubmitForApprovalHandler(IRequestService requestService)
    : IRequestHandler<SubmitForApprovalCommand, bool>
{
    public async Task<bool> Handle(SubmitForApprovalCommand request, CancellationToken cancellationToken)
        => await requestService.SubmitForApprovalAsync(request.RequestId, request.UserId, cancellationToken);
}
