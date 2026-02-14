using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Requests.GetList;

public record GetRequestListQuery(
    string UserId,
    bool IsManager,
    string? Status,
    string? Search,
    DateTime? FromDate,
    DateTime? ToDate,
    int Page = 1,
    int PageSize = 10
) : BaseQuery<RequestListResult>;

public record RequestListResult(
    IReadOnlyList<RequestListDto> Items,
    int TotalCount);
