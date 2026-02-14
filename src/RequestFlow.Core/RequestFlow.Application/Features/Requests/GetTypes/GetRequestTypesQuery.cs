using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.GetTypes;

public record GetRequestTypesQuery : BaseQuery<IReadOnlyList<RequestTypeDto>>;
