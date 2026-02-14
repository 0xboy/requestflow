using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces.Services;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetTypes;

public class GetRequestTypesHandler(IRequestService requestService, IMapper mapper)
    : IRequestHandler<GetRequestTypesQuery, IReadOnlyList<RequestTypeDto>>
{
    public async Task<IReadOnlyList<RequestTypeDto>> Handle(GetRequestTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await requestService.GetActiveTypesAsync(cancellationToken);
        return mapper.Map<IReadOnlyList<RequestTypeDto>>(types);
    }
}
