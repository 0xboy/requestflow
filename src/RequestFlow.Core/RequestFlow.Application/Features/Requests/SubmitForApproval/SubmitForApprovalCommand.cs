using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.SubmitForApproval;

public record SubmitForApprovalCommand(int RequestId, string UserId) : BaseCommand<bool>;
