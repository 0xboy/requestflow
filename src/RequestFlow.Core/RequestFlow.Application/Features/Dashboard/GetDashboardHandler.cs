using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Application.Models.Filters;
using MediatR;

namespace RequestFlow.Application.Features.Dashboard;

public class GetDashboardHandler(IRequestService requestService, IMapper mapper)
    : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var filter = mapper.Map<DashboardFilterModel>(request);
        var model = await requestService.GetDashboardDataAsync(filter, cancellationToken);
        return mapper.Map<DashboardDto>(model);
    }
}
