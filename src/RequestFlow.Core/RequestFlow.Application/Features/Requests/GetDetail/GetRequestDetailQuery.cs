using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.GetDetail;

public record GetRequestDetailQuery(int Id, string UserId, bool IsManager)
    : BaseQuery<RequestDto?>;
