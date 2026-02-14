using RequestFlow.Application.DTOs;
using RequestFlow.Application.Features.Common;

namespace RequestFlow.Application.Features.Dashboard;

public record GetDashboardQuery(string UserId, bool IsManager)
    : BaseQuery<DashboardDto>;
