using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetTypes;

public class GetRequestTypesHandler(IRequestTypeRepository requestTypeRepository)
    : IRequestHandler<GetRequestTypesQuery, IReadOnlyList<RequestTypeDto>>
{
    public async Task<IReadOnlyList<RequestTypeDto>> Handle(GetRequestTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await requestTypeRepository.GetActiveTypesAsync(cancellationToken);
        return types.Select(t => new RequestTypeDto { Id = t.Id, Name = t.Name }).ToList();
    }
}
