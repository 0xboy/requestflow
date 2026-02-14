using AutoMapper;
using RequestFlow.Application.DTOs;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Application.Models.Filters;
using MediatR;

namespace RequestFlow.Application.Features.Requests.GetDetail;

public class GetRequestDetailHandler(IRequestService requestService, IMapper mapper)
    : IRequestHandler<GetRequestDetailQuery, RequestDto?>
{
    public async Task<RequestDto?> Handle(GetRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var filter = mapper.Map<RequestDetailFilterModel>(request);
        var model = await requestService.GetByIdWithTypeAsync(filter, cancellationToken);
        if (model == null) return null;

        return mapper.Map<RequestDto>(model);
    }
}
