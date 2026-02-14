using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.Reject;

public record RejectRequestCommand(int RequestId, string UserId, string RejectionReason) : BaseCommand<bool>;
