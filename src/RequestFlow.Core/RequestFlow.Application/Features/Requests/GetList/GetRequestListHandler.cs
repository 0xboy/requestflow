using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Application.Models.Filters;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetList;

public class GetRequestListHandler(IRequestService requestService, IMapper mapper)
    : IRequestHandler<GetRequestListQuery, RequestListResult>
{
    public async Task<RequestListResult> Handle(GetRequestListQuery request, CancellationToken cancellationToken)
    {
        var filter = mapper.Map<RequestListFilterModel>(request);
        var (items, total) = await requestService.GetListAsync(filter, cancellationToken);

        var dtos = mapper.Map<IReadOnlyList<RequestListDto>>(items);
        return new RequestListResult(dtos.ToList(), total);
    }
}
