using RequestFlow.Application.Features.Common;
using RequestFlow.Shared.Constants;

namespace RequestFlow.Application.Features.Requests.Create;

public record CreateRequestCommand(
    string Title,
    string? Description,
    int RequestTypeId,
    Priority Priority,
    string UserId
) : BaseCommand<int>;
