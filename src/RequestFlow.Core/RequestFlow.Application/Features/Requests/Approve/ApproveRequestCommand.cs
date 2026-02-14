using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.Approve;

public record ApproveRequestCommand(int RequestId, string UserId) : BaseCommand<bool>;
