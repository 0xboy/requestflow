using AutoMapper;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Application.Models.Requests;
using MediatR;

namespace RequestFlow.Application.Features.Requests.Create;

public class CreateRequestHandler(IRequestService requestService, IMapper mapper)
    : IRequestHandler<CreateRequestCommand, int>
{
    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<CreateRequestModel>(request);
        return await requestService.CreateRequestAsync(model, cancellationToken);
    }
}
